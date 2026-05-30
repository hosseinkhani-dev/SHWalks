using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SHWalks.Application.Walks;
using SHWalks.Application.Walks.DTOs;

namespace SHWalks.API.Controllers
{
    [Route("api/walks")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IWalkService _walkService;
        private readonly IMapper _mapper;

        public WalksController(
            IWalkRepository walkRepository,
            IWalkService walkService,
            IMapper mapper)
        {
            _walkRepository = walkRepository;
            _walkService = walkService;
            _mapper = mapper;
        }

        [HttpPost]
        [ValidationModelAttribiute]
        public async Task<IActionResult> Add([FromBody] AddWalkDto dto)
        {
            var walkId = await _walkService.AddAsync(dto);

            if (walkId == null)
                return NoContent();

            return Ok(walkId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery)
        {
            var walksDto = await _walkRepository.GetAllAsync(filterOn, filterQuery);

            if (!walksDto.Any())
                return NotFound();

            return Ok(walksDto);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walkDto = await _walkRepository.GetByIdAsync(id);

            if (walkDto == null)
                return NotFound();

            return Ok(walkDto);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(
            [FromQuery] Guid id,
            [FromBody] UpdateWalkDto dto)
        {
            var walk = await _walkRepository.UpdateAsync(id, dto);

            if (walk == null)
                return NotFound();

            var walkDto = _mapper.Map<AddWalkDto>(walk);

            return Ok(walkDto);
        }
    }
}
