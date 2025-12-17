using Blogy.Business.DTOs.Common;

namespace Blogy.Business.DTOs.CategoryDtos
{
    public class ResultCategoryWithBlogCountDto : BaseDto
    {
        public string CategoryName { get; set; } 
        public int BlogCount { get; set; }
    }
}
