using SHWalks.Application.Walks.DTOs;
using SHWalks.Domain;

namespace SHWalks.Application.Walks;

public interface IWalkRepository
{
    Task<Guid> AddAsync(Walk walk);
    Task DeleteAsync(Walk walk);
    Task<Walk?> FindByIdAsync(Guid id);
    Task<List<GetAllWalksDto>> GetAllAsync(string? filterOn, string? filterQuery);
    Task<GetWalkDto?> GetByIdAsync(Guid id);
    Task<bool> IsExistWithAreaId(Guid id);
    Task<Walk?> UpdateAsync(Guid id, UpdateWalkDto dto);
}
