using Blogy.Business.DTOs.Common;

namespace Blogy.Business.DTOs.MessageDtos
{
    public class ResultMessageDto:BaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
