using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHWalks.Application.Areas;
using SHWalks.Application.Areas.DTOs;

namespace SHWalks.API.Controllers
{
    [Route("api/areas")]
    [ApiController]
    [ValidationModel]
    public class AreasController : ControllerBase
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IAreaService _areaService;

        public AreasController(
            IAreaRepository areaRepository,
            IAreaService areaService)
        {
            _areaRepository = areaRepository;
            _areaService = areaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await _areaRepository.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _areaRepository.GetByIdAsync(id);

            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Add([FromBody] AddAreaDto dto)
        {
            var areaId = await _areaService.AddAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = areaId }, dto);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAreaDto dto)
        {
            await _areaRepository.UpdateAsync(dto, id);
           
            return Ok(dto);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _areaRepository.DeleteAsync(id);

            return Ok();
        }
    }
}
