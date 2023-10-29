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
        public IEnumerable<GodDto> Get()
        {
            return gods;
        }

        [HttpGet("{id}")]
        public ActionResult<GodDto> GetById(Guid id)
        {
            var god = gods.Where(god => god.Id == id).SingleOrDefault();
            if(god is null)
            {
                return NotFound();
            }
            return god;
        }
        
        [HttpPost]
        public ActionResult<GodDto> Post(CreateGodDto createGodDto)
        {
            var god = new GodDto(Guid.NewGuid(), createGodDto.Name, createGodDto.Description, createGodDto.Price, DateTimeOffset.UtcNow);
            gods.Add(god);
            return CreatedAtAction(nameof(GetById), new { id = god.Id }, god);
        }

        [HttpPut]
        public IActionResult Put(Guid id, UpdateGodDto updateGodDto)
        {
            var existingGod = gods.Where(god => god.Id == id).SingleOrDefault();
            if(existingGod is null)
            {
                return NotFound();
            }
            var updatedGod = existingGod with
            {
                Name = updateGodDto.Name,
                Description = updateGodDto.Description,
                Price = updateGodDto.Price
            };

            var index = gods.FindIndex(existingGod => existingGod.Id == id);
            gods[index] = updatedGod;

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var index = gods.FindIndex(existingGod => existingGod.Id == id);
            gods.RemoveAt(index);

            return NoContent();
        }
    }
}