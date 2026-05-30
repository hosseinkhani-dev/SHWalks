using Microsoft.EntityFrameworkCore;
using SHWalks.Application.Areas.DTOs;
using SHWalks.Application.Walks;
using SHWalks.Application.Walks.DTOs;
using SHWalks.Domain;
using SHWalks.Infrastructure.Persistence;

namespace SHWalks.Infrastructure.Repositories.Walks
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly SHWalksDbContext _dbContext;

        public SqlWalkRepository(SHWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddAsync(Walk walk)
        {
            _dbContext.Walks.Add(walk);

            await _dbContext.SaveChangesAsync();

            return walk.Id;
        }

        public async Task<List<GetAllWalksDto>> GetAllAsync(
            string? filterOn, string? filterQuery)
        {
            var walks = _dbContext.Walks
                .Select(walk => new GetAllWalksDto
                {
                    Id = walk.Id,
                    Name = walk.Name,
                    Description = walk.Description,
                    ImageUrl = walk.ImageUrl,
                    Lenght = walk.Lenght,
                    Difficulty = walk.Difficulty,
                    AreaId = walk.AreaId
                }).AsQueryable();

            if(!string.IsNullOrWhiteSpace(filterOn) &&
                !string.IsNullOrWhiteSpace(filterQuery))
            {
                if(filterOn.Trim().ToLower() == "name")
                {
                    walks = walks.Where(walk => walk.Name.Contains(filterQuery));
                }
            }

            return await walks.ToListAsync();
        }

        public async Task<GetWalkDto?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Walks
                .Where(walk => walk.Id == id)
                .Select(walk => new GetWalkDto
                {
                    Id = walk.Id,
                    Name = walk.Name,
                    Description = walk.Description,
                    ImageUrl = walk.ImageUrl,
                    Lenght = walk.Lenght,
                    Difficulty = walk.Difficulty,
                    AreaDto = new GetAreaDto
                    {
                        Id = walk.Area!.Id,
                        Name = walk.Area.Name,
                        ImageUrl = walk.Area.ImageUrl
                    }
                }).SingleOrDefaultAsync();
        }

        public async Task<Walk?> UpdateAsync(Guid id, UpdateWalkDto dto)
        {
            if (dto.AreaId != null)
            {
                var isAreaExist = await _dbContext.Areas.AnyAsync(x => x.Id == dto.AreaId);

                if (!isAreaExist)
                    return null;
            }

            var walk = await _dbContext.Walks
                .SingleOrDefaultAsync(walk => walk.Id == id);

            if (walk == null)
                return null;

            walk.Name = !string.IsNullOrWhiteSpace(dto.Name) ? dto.Name : walk.Name;
            walk.Description = !string.IsNullOrWhiteSpace(dto.Description)
                ? dto.Description : walk.Description;
            walk.Lenght = dto.Lenght ?? walk.Lenght;
            walk.ImageUrl = !string.IsNullOrWhiteSpace(dto.ImageUrl)
                ? dto.ImageUrl : walk.ImageUrl;
            walk.Difficulty = dto.Difficulty ?? walk.Difficulty;
            walk.AreaId = dto.AreaId ?? walk.AreaId;

            return walk;
        }
    }
}
