using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.AdminLayout_Index
{
    public class _AdminLayoutScriptComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
