using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.DTOs.Weapon
{
    public class ModifyWeaponDto
    {
        public int WeaponId { get; set; }
        public int CharacterId { get; set; }
    }
}
