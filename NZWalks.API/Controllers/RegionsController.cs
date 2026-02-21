using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(
            NZWalksDbContext dbContext, 
            IRegionRepository regionRepository,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllAsync();

            var regionsDto = mapper.Map<List<RegionDto>>(regions);
               
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{regionId:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid regionId)
        {
            var region = await regionRepository.GetByIdAsync(regionId);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var region = mapper.Map<Region>(addRegionRequestDto);

            await regionRepository.AddAsync(region);

            var regionDto = mapper.Map<RegionDto>(region);

            return CreatedAtAction(nameof(GetById), new { id = region.RegionId }, regionDto);
        }

        [HttpPut]
        [Route("{regiodId:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid regionId, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var region = await regionRepository.UpdateAsync(regionId, updateRegionRequestDto);
            if (region == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{regionId:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid regionId)
        {
           
            var existingRegion = await regionRepository.DeleteAsync(regionId);
            if (existingRegion == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
