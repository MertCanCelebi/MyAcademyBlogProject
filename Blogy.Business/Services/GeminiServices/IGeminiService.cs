namespace Blogy.Business.Services.GeminiServices
{
    public interface IGeminiService
    {
        Task<string> GetGeminiDataAsync(string prompt);
    }
}
