namespace Blogy.Business.Services.HuggingServices
{
    public interface IToxicityService
    {
        Task<(bool IsToxic, double Score)> AnalyzeAsync(string text);
    }
}
