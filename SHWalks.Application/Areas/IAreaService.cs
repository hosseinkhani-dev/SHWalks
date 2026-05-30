using SHWalks.Application.Areas.DTOs;

namespace SHWalks.Application.Areas
{
    public interface IAreaService
    {
        Task<Guid> AddAsync(AddAreaDto dto);
    }
}
