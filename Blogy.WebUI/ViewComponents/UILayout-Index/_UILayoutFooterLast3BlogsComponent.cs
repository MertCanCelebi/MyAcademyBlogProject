using Blogy.Business.Services.BlogServices;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.UILayout_Index
{
    public class _UILayoutFooterLast3BlogsComponent(IBlogService _blogService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _blogService.GetLast3BlogsAsync();
            return View(values);
        }
    }
}
