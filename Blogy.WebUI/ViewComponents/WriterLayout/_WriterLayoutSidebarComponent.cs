using Blogy.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.WriterLayout
{
    public class _WriterLayoutSidebarComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            return View();
        }
    }
}
