using Blogy.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.UserLayout
{
    public class _UserLayoutSidebarComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            return View();
        }
    }
}
