using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.DTOs.Character;
using dotnet_rpg.DTOs.Weapon;

namespace dotnet_rpg.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<GetCharacterDto> AddWeapon(AddWeaponDto newWeapon);
        Task<GetCharacterDto> SellWeapon(ModifyWeaponDto modifyWeapon);
        Task<GetCharacterDto> RepairWeapon(ModifyWeaponDto modifyWeapon);
        Task<GetCharacterDto> StoreWeapon(ModifyWeaponDto modifyWeapon);
        Task<GetCharacterDto> RetrieveWeapon(ModifyWeaponDto modifyWeapon);
        Task<GetCharacterDto> DropWeapon(ModifyWeaponDto modifyWeapon);
    }
}
