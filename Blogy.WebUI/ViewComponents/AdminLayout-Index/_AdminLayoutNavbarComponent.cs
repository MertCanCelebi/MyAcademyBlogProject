using Blogy.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.AdminLayout_Index
{
    public class _AdminLayoutNavbarComponent(UserManager<AppUser> _userManager) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.image = string.IsNullOrEmpty(user?.ImageUrl)
                ? "/skydash-v.01/images/faces/face28.jpg"
                : user.ImageUrl;
            return View();
        }
    }
}
