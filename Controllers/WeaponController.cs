using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.DTOs.Character;
using dotnet_rpg.DTOs.Weapon;
using dotnet_rpg.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpPost]
        public async Task<ActionResult<Weapon>> AddWeapon(AddWeaponDto newWeapon)
        {
            return Ok(await _weaponService.AddWeapon(newWeapon));
        }

        [HttpPost("Sale")]
        public async Task<ActionResult<GetCharacterDto>> SellWeapon(ModifyWeaponDto modifyWeapon)
        {
            try
            {
                return Ok(await _weaponService.SellWeapon(modifyWeapon));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Repair")]
        public async Task<ActionResult<GetCharacterDto>> RepairWeapon(ModifyWeaponDto modifyWeapon)
        {
            try
            {
                return Ok(await _weaponService.RepairWeapon(modifyWeapon));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Store")]
        public async Task<ActionResult<GetCharacterDto>> StoreWeapon(ModifyWeaponDto modifyWeapon)
        {
            try
            {
                return Ok(await _weaponService.StoreWeapon(modifyWeapon));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Retrieve")]
        public async Task<ActionResult<GetCharacterDto>> RetrieveWeapon(ModifyWeaponDto modifyWeapon)
        {
            try
            {
                return Ok(await _weaponService.RetrieveWeapon(modifyWeapon));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Drop")]
        public async Task<ActionResult<GetCharacterDto>> DropWeapon(ModifyWeaponDto modifyWeapon)
        {
            try
            {
                return Ok(await _weaponService.RetrieveWeapon(modifyWeapon));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
