using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
  public class Inventory
  {
    public int Id { get; set; }
    public List<Weapon> Weapons { get; set; }
    public List<Food> Foods { get; set; }
    public List<Potion> Potions { get; set; }
    public int Money { get; set; }
    public int CharacterId { get; set; }
    public Character Character { get; set; }
  }
}
