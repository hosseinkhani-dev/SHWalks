using SHWalks.Application.Areas.DTOs;

namespace SHWalks.Application.Areas
{
    public interface IAreaService
    {
        Task<Guid> AddAsync(AddAreaDto dto);
        Task DeleteAsync(Guid id);
        Task<List<GetAllAreaWalksDto>> GetAllWalksAsync(Guid id);
    }
}
