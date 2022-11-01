using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class Potion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Healing { get; set; }
        public int Power { get; set; }
        public int Duration { get; set; }
        public int Value { get; set; }
    }
}
