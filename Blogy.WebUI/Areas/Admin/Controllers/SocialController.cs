using Blogy.Business.DTOs.AboutDtos;
using Blogy.Business.DTOs.SocialDtos;
using Blogy.Business.Services.SocialService;
using Blogy.WebUI.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{Roles.Admin}")]
    public class SocialController(ISocialService _socialService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var values = await _socialService.GetAllAsync();
            return View(values);
        }
        public async Task<IActionResult> CreateSocial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocial(CreateSocialDto socialDto)
        {
            if (!ModelState.IsValid)
            {
                return View(socialDto);
            }

            await _socialService.CreateAsync(socialDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateSocial(int id)
        {
            var social = await _socialService.GetByIdAsync(id);
            return View(social);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSocial(UpdateSocialDto updateSocialDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateSocialDto);
            }

            await _socialService.UpdateAsync(updateSocialDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteSocial(int id)
        {
            await _socialService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
