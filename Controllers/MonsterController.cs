using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonsterController : ControllerBase
    {
        public MonsterController()
        {

        }
        [HttpGet]
        public async Task<ActionResult<Monster>> GetMonster()
        {
            return Ok(new Monster());
        }
    }
}
