using Microsoft.EntityFrameworkCore;
using SHWalks.Application.Areas;
using SHWalks.Application.Areas.DTOs;
using SHWalks.Domain;
using SHWalks.Infrastructure.Persistence;

namespace SHWalks.Infrastructure.Repositories.Areas
{
    public class SqlAreaRepository : IAreaRepository
    {
        private readonly SHWalksDbContext _dbContext;

        public SqlAreaRepository(SHWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetAreaDto>> GetAllAsync()
        {
            return await _dbContext.Areas.Select(area => new GetAreaDto
            {
                Id = area.Id,
                Name = area.Name,
                ImageUrl = area.ImageUrl,
            }).ToListAsync();
        }

        public async Task<GetAreaDto?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Areas
                .Where(area => area.Id == id)
                .Select(area => new GetAreaDto
                {
                    Id = area.Id,
                    Name = area.Name,
                    ImageUrl = area.ImageUrl,
                }).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var area = await _dbContext.Areas
                .FirstOrDefaultAsync(area => area.Id == id);

            if (area is not null)
                _dbContext.Areas.Remove(area!);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Area?> FindByIdAsync(Guid id)
        {
            return await _dbContext.Areas
                .FirstOrDefaultAsync(area => area.Id == id);
        }

        public async Task AddAsync(Area area)
        {
            _dbContext.Add(area);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateAreaDto dto, Guid id)
        {
            var area = await _dbContext.Areas.SingleOrDefaultAsync(
               area => area.Id == id);

            if (area is null)
            {
                throw new Exception();
            }

            area.Name = dto.Name ?? area.Name;

            area.ImageUrl = dto.ImageUrl;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsExistByIdAsync(Guid areaId)
        {
            return await _dbContext.Areas
                .Where(area => area.Id ==  areaId).AnyAsync();
        }
    }
}
