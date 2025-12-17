using Blogy.Business.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.Default_Index
{
    public class _DefaultBlogsComponent(ICategoryService _categoryService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = (await _categoryService.GetCategoriesWithBlogsAsync())
                .Where(x => x.Blogs != null && x.Blogs.Any())
                .Select(x =>
                {
                    x.Blogs = x.Blogs
                        .OrderByDescending(b => b.CreatedDate)
                        .Take(3)
                        .ToList();

                    return x;
                })
                .ToList();
            return View(values);
        }
    }
}
