using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Phoenix.Catalog.Service;
using Phoenix.Catalog.Service.Dtos;
using Phoenix.Catalog.Service.Entities;
using Phoenix.Common;

namespace Phoenix.Catalog.Service.Controllers
{
    [ApiController]
    [Route("gods")]
    public class GodsController : ControllerBase
    {
        private readonly IRepository<God>? godsRepository;
        public GodsController(IRepository<God>? godsRepository)
        {
            this.godsRepository = godsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GodDto>>> GetAsync()
        {
            var gods = (await godsRepository.GetAllAsync())
            .Select(god => god.AsDto());
            return Ok(gods);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GodDto>> GetByIdAsync(Guid id)
        {
            var god = await godsRepository.GetAsync(id);
            if (god is null)
            {
                return NotFound();
            }
            return god.AsDto();
        }

        [HttpGet("name")]
        public async Task<ActionResult<IEnumerable<GodDto>>> GetByIdNameNAsync()
        {
            var gods = await godsRepository.GetAsync(g => g.Name.EndsWith("n"));
            if(gods is null)
            {
                return NotFound();
            }
            return Ok(gods);
        }

        [HttpPost]
        public async Task<ActionResult<GodDto>> PostAsync(CreateGodDto createGodDto)
        {
            var god = new God
            {
                Name = createGodDto.Name,
                Description = createGodDto.Description,
                Price = createGodDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await godsRepository.CreateAsync(god);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = god.Id }, god);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateGodDto updateGodDto)
        {
            var existingGod = await godsRepository.GetAsync(id);
            if (existingGod is null)
            {
                return NotFound();
            }

            existingGod.Name = updateGodDto.Name;
            existingGod.Description = updateGodDto.Description;
            existingGod.Price = updateGodDto.Price;

            await godsRepository.UpdateAsync(existingGod);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var gods = await godsRepository.GetAsync(id);

            await godsRepository.RemoveAsync(gods.Id);

            return NoContent();
        }
    }
}