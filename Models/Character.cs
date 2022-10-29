namespace dotnet_rpg.Models
{
  public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Bob";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Farmboy;
        public string Description { get; set; } = "A wily character, small but fierce.";
        public int ClassLevel { get; set; } = 1;
        public User? User { get; set; }
        public Weapon Weapon { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
