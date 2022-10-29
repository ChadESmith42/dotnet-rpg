using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using dotnet_rpg.DTOs.Character;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _service;
        public CharacterController(ICharacterService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetCharacterDto>>> Get()
        {
            try
            {
                return Ok(await _service.GetAllCharacters());
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCharacterDto>> GetById(int id)
        {
            try
            {
                return Ok(await _service.GetCharacterById(id));
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpPost]
        public async Task<ActionResult<List<AddCharacterDto>>> AddCharacter([FromBody] AddCharacterDto character)
        {
            try
            {
                await _service.AddCharacter(character);
                return Ok(await _service.GetAllCharacters());
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetCharacterDto>> UpdateCharacter(int id, [FromBody] UpdateCharacterDto updatedCharacter)
        {
            if (id != updatedCharacter.Id) { return BadRequest(); }

            try
            {
                return Ok(await _service.UpdateCharacter(updatedCharacter));
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            try
            {
                return Ok(await _service.DeleteCharacter(id));
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
