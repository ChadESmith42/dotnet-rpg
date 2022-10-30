using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.DTOs.Fight;
using dotnet_rpg.Services.FightService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService _service;

        public FightController(IFightService service)
        {
            _service = service;
        }

        [HttpPost("Weapon")]
        public async Task<ActionResult<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            try
            {
                var response = await _service.WeaponAttack(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            try
            {
                var response = await _service.SkillAttack(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<FightResultDto>> Fight(FightRequestDto request)
        {
            try
            {
                var response = await _service.Fight(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("High-Score")]
        public async Task<ActionResult<HighScoreDto>> GetHighScores()
        {
            try
            {
                return Ok(await _service.GetHighScore());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


    }
}
