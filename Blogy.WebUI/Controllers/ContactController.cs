using Blogy.Business.DTOs.MessageDtos;
using Blogy.Business.Services.GeminiServices;
using Blogy.Business.Services.MessageServices;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Controllers
{
    public class ContactController(IMessageService _messageService,IGeminiService _geminiService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateMessageDto createMessageDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createMessageDto);
            }
            await _messageService.CreateAsync(createMessageDto);

            var prompt = $@"
                    Aşağıdaki mesaja mesajınız alınmıştır en kısa sürede dönüş yapılacaktır tarzında 
                        bir dönüş yap. Mesaj hangi dilde ise o dilde karşılık ver.

                    Kurallar:
                    - Başlık ekleme
                    - Açıklama veya yorum ekleme
                    - Sadece düz metin gönder

                    Mesaj: {createMessageDto.Content}";

            var apiResponse = await _geminiService.GetGeminiDataAsync(prompt);


            ViewBag.response = apiResponse;
            ModelState.Clear();
            return View();
        }
    }
}
