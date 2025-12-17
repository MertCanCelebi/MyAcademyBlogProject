using AutoMapper;
using Blogy.Business.DTOs.AboutDtos;
using Blogy.DataAccess.Repositories.AboutRepositories;
using Blogy.Entity.Entities;

namespace Blogy.Business.Services.AboutServices
{
    public class AboutService(IAboutRepository _aboutRepository, IMapper _mapper) : IAboutService
    {
        public async Task CreateAsync(CreateAboutDto dto)
        {
            var entity = _mapper.Map<About>(dto);
            await _aboutRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _aboutRepository.DeleteAsync(id);
        }

        public async Task<List<ResultAboutDto>> GetAllAsync()
        {
            var values = await _aboutRepository.GetAllAsync();
            return _mapper.Map<List<ResultAboutDto>>(values);
        }

        public async Task<UpdateAboutDto> GetByIdAsync(int id)
        {
            var value = await _aboutRepository.GetByIdAsync(id);
            return _mapper.Map<UpdateAboutDto>(value);
        }

        public async Task<ResultAboutDto> GetSingleByIdAsync(int id)
        {
            var value = await _aboutRepository.GetByIdAsync(id);
            return _mapper.Map<ResultAboutDto>(value);
        }

        public async Task UpdateAsync(UpdateAboutDto dto)
        {
            var entity = _mapper.Map<About>(dto);
            await _aboutRepository.UpdateAsync(entity);
        }
    }
}
