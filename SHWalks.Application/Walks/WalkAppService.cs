using SHWalks.Application.Areas;
using SHWalks.Application.Walks.DTOs;
using SHWalks.Domain;

namespace SHWalks.Application.Walks
{
    public class WalkAppService : IWalkService
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IAreaRepository _areaRepository;

        public WalkAppService(
            IWalkRepository walkRepository,
            IAreaRepository areaRepository)
        {
            _walkRepository = walkRepository;
            _areaRepository = areaRepository;
        }

        public async Task<Guid?> AddAsync(AddWalkDto dto)
        {
            var isAreaExist = await _areaRepository.IsExistByIdAsync(dto.AreaId);

            if(!isAreaExist)
                return null;

            var walk = new Walk
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Length = dto.Length,
                ImageUrl = dto.ImageUrl,
                Difficulty = dto.Difficulty,
                AreaId = dto.AreaId,
            };

            return await _walkRepository.AddAsync(walk);
        }
    }
}
