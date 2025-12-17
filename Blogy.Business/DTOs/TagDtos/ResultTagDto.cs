using Blogy.Business.DTOs.Common;
using Blogy.Entity.Entities;

namespace Blogy.Business.DTOs.TagDtos
{
    public class ResultTagDto: BaseDto
    {
        public string Name { get; set; }
        public virtual IList<BlogTag> BlogTags { get; set; }
    }
}
