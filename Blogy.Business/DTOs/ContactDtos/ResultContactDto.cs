using Blogy.Business.DTOs.Common;

namespace Blogy.Business.DTOs.ContactDtos
{
    public class ResultContactDto:BaseDto
    {
        public string Location { get; set; }
        public string OpenHours { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
