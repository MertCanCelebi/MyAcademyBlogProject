using Blogy.Business.DTOs.Common;

namespace Blogy.Business.DTOs.AboutDtos
{
    public class UpdateAboutDto:BaseDto
    {
        public string? Title1 { get; set; }
        public string? Description1 { get; set; }
        public string? ImageUrl1 { get; set; }
        public string? Title2 { get; set; }
        public string? Description2 { get; set; }
        public string? ImageUrl2 { get; set; }
    }
}
