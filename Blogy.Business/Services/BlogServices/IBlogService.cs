using Blogy.Business.DTOs.BlogDtos;

namespace Blogy.Business.Services.BlogServices
{
    public interface IBlogService : IGenericService<ResultBlogDto, UpdateBlogDto, CreateBlogDto>
    {
        Task<List<ResultBlogDto>> GetBloksWithCategoriesAsync();
        Task<List<ResultBlogDto>> GetBlogsByCategoryIdAsync(int categoryId);
        Task<List<ResultBlogDto>> GetBlogsByWriterIdAsync(int writerId);
        Task<List<ResultBlogDto>> GetLast3BlogsAsync();
    }
}
