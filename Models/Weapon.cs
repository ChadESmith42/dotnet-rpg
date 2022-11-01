using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
  public class Weapon
  {
    public int Id { get; set; }
    public string Name { get; set; } = "Spoon of Justice";
    public string Type { get; set; } = "Spoon";
    public int Damage { get; set; } = 10;
    public int Durability { get; set; } = 10;
    public bool Repairable { get; set; } = true;
    public int Value { get; set; }
    public bool IsActive { get; set; }
  }
}
