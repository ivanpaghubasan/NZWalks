using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();

            var regionsDto = new List<RegionDto>();

            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto
                {
                    RegionId = region.RegionId,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{regionId:Guid}")]
        public IActionResult GetById([FromRoute] Guid regionId)
        {
            //var region = dbContext.Regions.Find(regionId);

            var region = dbContext.Regions.FirstOrDefault(r => r.RegionId == regionId);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                RegionId = region.RegionId,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var region = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            dbContext.Regions.Add(region);
            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                RegionId = region.RegionId,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = region.RegionId }, regionDto);
        }

        [HttpPut]
        [Route("{regiodId:Guid}")]
        public IActionResult Update([FromRoute] Guid regionId, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var region = dbContext.Regions.FirstOrDefault(r => r.RegionId == regionId);
            if (region == null)
                return NotFound();

            region.Code = updateRegionRequestDto.Code;
            region.Name = updateRegionRequestDto.Name;
            region.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                RegionId = region.RegionId,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{regionId:Guid}")]
        public IActionResult Delete([FromRoute] Guid regionId)
        {
            var region = dbContext.Regions.FirstOrDefault(r => r.RegionId == regionId);
            if (region == null)
                return NotFound();

            dbContext.Regions.Remove(region);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
