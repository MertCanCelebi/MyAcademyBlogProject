using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Blogy.Business.Services.HuggingServices
{
    public class HuggingFaceToxicityService : IToxicityService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public HuggingFaceToxicityService(IConfiguration config)
        {
            _apiKey = config["HuggingFace:ApiKey"];

            if (string.IsNullOrEmpty(_apiKey))
                throw new Exception("HF API KEY NULL!");

            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);

            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<(bool IsToxic, double Score)> AnalyzeAsync(string text)
        {
            var translated = await TranslateToEnglish(text);
            var score = await GetToxicityScore(translated);
            return (score > 0.50, score);
        }

        private async Task<string> TranslateToEnglish(string text)
        {
            var response = await CallHF(
                "https://router.huggingface.co/hf-inference/models/Helsinki-NLP/opus-mt-tr-en",
                text
            );

            return response[0]
                .GetProperty("translation_text")
                .GetString();
        }

        private async Task<double> GetToxicityScore(string text)
        {
            var response = await CallHF(
                "https://router.huggingface.co/hf-inference/models/unitary/toxic-bert",
                text
            );

            var toxic = response[0]
                .EnumerateArray()
                .First(x => x.GetProperty("label").GetString() == "toxic");

            return toxic.GetProperty("score").GetDouble();
        }

        private async Task<JsonElement> CallHF(string url, string input)
        {
            var body = JsonSerializer.Serialize(new
            {
                inputs = input,
                options = new { wait_for_model = true }
            });

            var response = await _httpClient.PostAsync(
                url,
                new StringContent(body, Encoding.UTF8, "application/json")
            );

            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"HF ERROR ({response.StatusCode}): {responseText}"
                );
            }

            
            var contentType = response.Content.Headers.ContentType?.MediaType;

            if (contentType == null || !contentType.Contains("application/json"))
            {
                throw new Exception(
                    $"HF JSON DIŞI RESPONSE: {responseText}"
                );
            }

            
            var firstChar = responseText.TrimStart()[0];
            if (firstChar != '{' && firstChar != '[')
            {
                throw new Exception(
                    $"HF GEÇERSİZ JSON BAŞLANGICI: {responseText}"
                );
            }

            return JsonDocument.Parse(responseText).RootElement;
        }
    }

}