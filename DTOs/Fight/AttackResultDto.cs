using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.DTOs.Fight
{
    public class AttackResultDto
    {
        public string AttackerName { get; set; } = string.Empty;
        public string DefenderName { get; set; } = string.Empty;
        public int AttackerHitPoints { get; set; }
        public int DefenderHitPoints { get; set; }
        public int Damage { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
