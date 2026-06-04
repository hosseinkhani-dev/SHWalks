using SHWalks.Application.Areas.DTOs;
using SHWalks.Application.Walks;
using SHWalks.Domain;

namespace SHWalks.Application.Areas
{
    public class AreaAppService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IWalkRepository _walkRepository;

        public AreaAppService(
            IAreaRepository areaRepository,
            IWalkRepository walkRepository)
        {
            _areaRepository = areaRepository;
            _walkRepository = walkRepository;
        }

        public async Task<Guid> AddAsync(AddAreaDto dto)
        {
            var area = new Area
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                ImageUrl = dto.ImageUrl,
            };

            await _areaRepository.AddAsync(area);

            return area.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var hasWalk = await _walkRepository.IsExistWithAreaId(id);

            if(hasWalk)
            {
                throw new Exception("This area has walk. and cannot be deleted!");
            }

            await _areaRepository.DeleteAsync(id);  

        }

        public async Task<List<GetAllAreaWalksDto>> GetAllWalksAsync(Guid id)
        {
            return await _areaRepository.GetAllWalksAsync(id);
        }
    }
}
