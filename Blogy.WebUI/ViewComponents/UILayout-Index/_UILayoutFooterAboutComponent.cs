using Blogy.Business.Services.GeminiServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Blogy.WebUI.ViewComponents.UILayout_Index
{
    public class _UILayoutFooterAboutComponent : ViewComponent
    {
        private readonly IGeminiService _geminiService;
        private readonly IMemoryCache _cache;

        public _UILayoutFooterAboutComponent(IGeminiService geminiService, IMemoryCache cache)
        {
            _geminiService = geminiService;
            _cache = cache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!_cache.TryGetValue("FooterAboutText", out string response))
            {
                var prompt = "Bir blog sayfasında footer alanına hakkımızda yazısı yazmanı istiyorum. Çok kısa olsun 20,25 kelime kadar ve sadece metni yaz ekstra bir şey ekleme";

                var apiResponse = await _geminiService.GetGeminiDataAsync(prompt);

                // ❗ Eğer hata döndüyse cache etme
                if (!string.IsNullOrWhiteSpace(apiResponse) && !apiResponse.Contains("error"))
                {
                    response = apiResponse;
                    _cache.Set("FooterAboutText", response, TimeSpan.FromHours(12));
                }
                else
                {
                    response = null; // Hata durumunda default yazı kullanılacak
                }
            }

            // Hata veya null geldiğinde default yazı göster
            ViewBag.response = response ?? "Blog köşemiz, fikirlerimizi paylaşmamızı sağlayan küçük bir alan.";

            return View();
        }

    }
}
