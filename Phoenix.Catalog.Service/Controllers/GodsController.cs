using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Catalog.Service.Dtos;

namespace Phoenix.Catalog.Service.Controllers
{
    [ApiController]
    [Route("gods")]
    public class GodsController : ControllerBase
    {
        private readonly GodsRepository godsRepository = new();
        public static List<GodDto> gods = new()
        {
            new GodDto(Guid.NewGuid(),
                "Zeus", "Figure of Zeus", 20.45M, DateTimeOffset.Now),
            new GodDto(Guid.NewGuid(),
                "Artemis", "Figure of Artemis", 40M, DateTimeOffset.Now),
            new GodDto(Guid.NewGuid(),
                "Aphrodite", "Figure of Aphrodite", 60M, DateTimeOffset.Now),
        };

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
            if(god is null)
            {
                return NotFound();
            }
            return god.AsDto();
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
            return CreatedAtAction(nameof(GetByIdAsync), new { id = god.Id }, god.AsDto());
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Guid id, UpdateGodDto updateGodDto)
        {
            var existingGod = await godsRepository.GetAsync(id);
            if(existingGod is null)
            {
                return NotFound();
            }
            
            existingGod.Name = updateGodDto.Name;
            existingGod.Description = updateGodDto.Description;
            existingGod.Price = updateGodDto.Price;

            await godsRepository.UpdateAsync(existingGod);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var gods = await godsRepository.GetAsync(id);
            if(gods is null)
            {
                return NotFound();
            }

            await godsRepository.RemoveAsync(gods.Id);
            return NoContent();
        }
    }
}