using Blogy.Business.DTOs.AboutDtos;
using Blogy.Business.Services.AboutServices;
using Blogy.WebUI.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{Roles.Admin}")]
    public class AboutController(IAboutService _aboutService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var values = await _aboutService.GetAllAsync();
            return View(values);
        }
        public async Task<IActionResult> CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto aboutDto)
        {
            if (!ModelState.IsValid)
            {
                return View(aboutDto);
            }
            
            await _aboutService.CreateAsync(aboutDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var about = await _aboutService.GetByIdAsync(id);
            return View(about);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateAboutDto);
            }

            await _aboutService.UpdateAsync(updateAboutDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteAbout(int id)
        {
            await _aboutService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
