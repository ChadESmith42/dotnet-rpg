using System.Text.Json.Serialization;

namespace dotnet_rpg.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Pirate = 1,
        SwordMaster = 2,
        Giant = 3,
        Brainiac = 4,
        Soldier = 5,
        Thief = 6,
        Huntsman = 7,
        Farmboy = 8,
        Wizard = 9
    }
}
