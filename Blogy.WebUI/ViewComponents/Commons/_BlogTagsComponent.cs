using Blogy.Business.Services.CategoryServices;
using Blogy.Business.Services.TagServices;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.Commons
{
    public class _BlogTagsComponent(ITagService _tagService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _tagService.GetAllAsync();
            return View(values);
        }
    }
}
