using SHWalks.Application.Areas.DTOs;
using SHWalks.Domain;

namespace SHWalks.Application.Areas
{
    public interface IAreaRepository
    {
        Task<List<GetAreaDto>> GetAllAsync();
        Task<GetAreaDto?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<Area?> FindByIdAsync(Guid id);
        Task AddAsync(Area area);
        Task UpdateAsync(UpdateAreaDto dto, Guid id);
        Task<bool> IsExistByIdAsync(Guid areaId);
    }
}
