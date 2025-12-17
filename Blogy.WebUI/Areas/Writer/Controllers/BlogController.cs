using Azure;
using Blogy.Business.DTOs.BlogDtos;
using Blogy.Business.Services.BlogServices;
using Blogy.Business.Services.CategoryServices;
using Blogy.Business.Services.GeminiServices;
using Blogy.Entity.Entities;
using Blogy.WebUI.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blogy.WebUI.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Authorize(Roles = $"{Roles.Writer}")]
    public class BlogController(IBlogService _blogService,ICategoryService _categoryService, UserManager<AppUser> _userManager , IGeminiService _geminiService) : Controller
    {
        private async Task GetCategoriesAsync()
        {
            var categories = await _categoryService.GetAllAsync();

            ViewBag.categories = (from category in categories
                                  select new SelectListItem
                                  {
                                      Text = category.CategoryName,
                                      Value = category.Id.ToString()
                                  }).ToList();
        } 

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writerId = user.Id;
            var values = await _blogService.GetBlogsByWriterIdAsync(writerId);
            return View(values);
        }
        public async Task<IActionResult> CreateBlog()
        {
            await GetCategoriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogDto blogDto)
        {
            if (!ModelState.IsValid)
            {
                await GetCategoriesAsync();
                return View(blogDto);
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            blogDto.WriterId = user.Id;
            await _blogService.CreateAsync(blogDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateBlog(int id)
        {
            await GetCategoriesAsync();
            var blog = await _blogService.GetByIdAsync(id);
            return View(blog);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog(UpdateBlogDto updateBlogDto)
        {
            if (!ModelState.IsValid)
            {
                await GetCategoriesAsync();
                return View(updateBlogDto);
            }

            var user = await _userManager.GetUserAsync(User);
            updateBlogDto.WriterId = user.Id;

            await _blogService.UpdateAsync(updateBlogDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _blogService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlogByAI(string cumle)
        {
            if (string.IsNullOrWhiteSpace(cumle))
            {
                ModelState.AddModelError("", "Lütfen bir cümle giriniz.");
                return View();
            }
            var prompt = $@"
                    Aşağıdaki cümleyi temel alarak yaklaşık 1000 kelimelik,
                    akıcı, blog formatında bir Türkçe makale yaz.

                    Kurallar:
                    - Başlık ekleme
                    - Açıklama veya yorum ekleme
                    - Sadece düz metin gönder

                    Cümle: {cumle}"; 

            var apiResponse = await _geminiService.GetGeminiDataAsync(prompt);


            ViewBag.response = apiResponse;

            await GetCategoriesAsync();

            return View("CreateBlog", new CreateBlogDto
            {
                Description = apiResponse
            });

        }
    }
}
