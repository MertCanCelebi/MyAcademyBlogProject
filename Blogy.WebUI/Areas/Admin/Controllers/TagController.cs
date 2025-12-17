using Blogy.Business.DTOs.TagDtos;
using Blogy.Business.Services.TagServices;
using Blogy.WebUI.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{Roles.Admin}")]
    public class TagController(ITagService _tagService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var values = await _tagService.GetAllAsync();
            return View(values);
        }
        public async Task<IActionResult> CreateTag()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(CreateTagDto tagDto)
        {
            if (!ModelState.IsValid)
            {
                return View(tagDto);
            }

            await _tagService.CreateAsync(tagDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateTag(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            return View(tag);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTag(UpdateTagDto updateTagDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateTagDto);
            }

            await _tagService.UpdateAsync(updateTagDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteTag(int id)
        {
            await _tagService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
