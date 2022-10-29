using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
  public class Monster
  {
    public int Id { get; set; }
    public string Name { get; set; } = GetName();
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public string Description { get; set; } = GenerateDescription();
    public bool HasPowerfulNose { get; set; } = false;

    private static string GetName()
    {
      Random rand = new Random();
      string[] name = new string[] {
        "Bob",
        "William",
        "Xavier",
        "Javier",
        "Dino",
        "Julio",
        "Mike",
        "Eloise",
        "Wilfred",
        "Ferdinand",
        "Kellie",
        "Maxine",
        "Lois",
        "Luis",
        "Jimmy",
        "Tim",
        "Roger",
        "Nicholas",
        "Sven"
      };

      string[] descriptor = new string[]
      {
        "Flatulent",
        "Hairy",
        "Obtuse",
        "Persnickety",
        "Annoying",
        "Fierce",
        "Wild",
        "Lonely",
        "Vicious",
        "Dumb",
        "Genius",
        "Shrubber",
        "Gardner",
        "Finger-Painter",
        "Savage",
        "Cunning",
        "Daring",
        "Obnoxious",
        "Sarcastic",
        "Over-bearing",
        "Strong",
        "Weak",
        "Hairless"
      };

      return $"{name[rand.Next(name.Length)]} the {descriptor[rand.Next(descriptor.Length)]}";
    }

    public static string GenerateDescription()
    {
      Random rand = new Random();
      string[] descriptions = new string[]
      {
        "A wiley beast, but not very strong.",
        "His breath is the strongest thing about him.",
        "Small a fury, he's got sharp, pointy teeth.",
        "Looks like an aardvark, without the charm.",
        "Large and covered with orange fur. For some reason, it's wearing Air Jordans.",
        "Large, scaly, and with huge antlers. This is repulsive.",
        "A cute little rabbit, but covered in the blood of it's last victim.",
        "A continuously moving cloud of smoke and debris. This could get messy.",
        "Fangs. It has fangs on it's fangs. Let's just say, there are lots of teeth involved.",
        "A rodent of unusual size. They do exist."
      };

      return descriptions[rand.Next(descriptions.Length)];
    }

  }
}
