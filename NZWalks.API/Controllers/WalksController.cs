using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            var newWalk = await walkRepository.CreateAsync(walkDomainModel);

            var walkDto = mapper.Map<WalkDto>(newWalk);

            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksModel = await walkRepository.GetAll();

            return Ok(mapper.Map<List<WalkDto>>(walksModel));
        }

        [HttpGet]
        [Route("{walkId:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid walkId)
        {
            var walkDomainModel = await walkRepository.GetById(walkId);
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut]
        [Route("{walkId:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid walkId, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            walkDomainModel = await walkRepository.UpdateAsync(walkId, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        [HttpDelete]
        [Route("{walkId:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid walkId)
        {
            var deletedWalk = await walkRepository.DeleteAsync(walkId);
            if (deletedWalk == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
