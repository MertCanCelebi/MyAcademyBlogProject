using Blogy.Business.Services.BlogServices;
using Blogy.Business.Services.CategoryServices;
using Blogy.Business.Services.CommentServices;
using Blogy.Entity.Entities;
using Blogy.WebUI.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Admin.Controllers
{
    [Area(Roles.Admin)]
    [Authorize(Roles = Roles.Admin)]
    public class DashboardController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly IBlogService _blogService;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(ICategoryService categoryService, ICommentService commentService, IBlogService blogService, UserManager<AppUser> userManager)
        {
            _categoryService = categoryService;
            _commentService = commentService;
            _blogService = blogService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {


            //kategori sayısı
            var categories = await _categoryService.GetAllAsync();
            ViewBag.categoryCount = categories.Count();

            //yorum sayısı
            var comments = await _commentService.GetAllAsync();
            ViewBag.commentsCount = comments.Count();

            //yazar sayısı
            var writers = await _userManager.GetUsersInRoleAsync(Roles.Writer);
            ViewBag.writerCount = writers.Count;

            //son yazı yazan kullanıcı
            var lastBlog = (await _blogService.GetAllAsync())
                    .OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault();

            if (lastBlog != null)
            {
                var lastWriter = await _userManager.FindByIdAsync(lastBlog.WriterId.ToString());
                ViewBag.lastBlogWriter = lastWriter?.UserName;
            }

            //categorilerin blog sayısı chartı
            var categoryBlogCounts = (await _blogService.GetAllAsync())
                                    .GroupBy(x => x.CategoryId)
                                    .Select(g => new
                                    {
                                        CategoryId = g.Key,
                                        Count = g.Count()
                                    })
                                    .ToList();

            var categoriesDict = (await _categoryService.GetAllAsync())
                .ToDictionary(x => x.Id, x => x.CategoryName);

            var categoryNames = new List<string>();
            var blogCounts = new List<int>();

            foreach (var item in categoryBlogCounts)
            {
                if (categoriesDict.ContainsKey(item.CategoryId))
                {
                    categoryNames.Add(categoriesDict[item.CategoryId]);
                    blogCounts.Add(item.Count);
                }
            }

            ViewBag.CategoryNames = categoryNames;
            ViewBag.BlogCounts = blogCounts;


            //yazara göre blog sayısı
            var blogs = await _blogService.GetAllAsync();

            var writerBlogCounts = blogs
                .GroupBy(x => x.WriterId)
                .Select(g => new
                {
                    WriterId = g.Key,
                    BlogCount = g.Count()
                })
                .ToList();

            var writerNames = new List<string>();
            var writerBlogNumbers = new List<int>();

            foreach (var item in writerBlogCounts)
            {
                var user = await _userManager.FindByIdAsync(item.WriterId.ToString());
                if (user != null)
                {
                    writerNames.Add(user.UserName);
                    writerBlogNumbers.Add(item.BlogCount);
                }
            }

            ViewBag.WriterNames = writerNames;
            ViewBag.WriterBlogCounts = writerBlogNumbers;

            return View();
        }
    }
}
