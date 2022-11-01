using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Healing { get; set; }
        public bool CausesBadBreath { get; set; }
        public int Value { get; set; }
    }
}
