using SHWalks.Application.Walks.DTOs;

namespace SHWalks.Application.Walks
{
    public interface IWalkService
    {
        Task<Guid?> AddAsync(AddWalkDto dto);
    }
}
