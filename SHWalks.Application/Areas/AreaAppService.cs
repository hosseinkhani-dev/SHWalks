using SHWalks.Application.Areas.DTOs;
using SHWalks.Domain;

namespace SHWalks.Application.Areas
{
    public class AreaAppService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;

        public AreaAppService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
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
    }
}
