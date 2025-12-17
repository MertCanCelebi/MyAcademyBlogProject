using Blogy.Business.Services.SocialService;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.UILayout_Index
{
    public class _UILayoutFooterSocialComponent(ISocialService _socialService): ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _socialService.GetAllAsync();
            return View(values);
        }
    }
}
