using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.DTOs.Character;
using dotnet_rpg.DTOs.Weapon;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.WeaponService
{
  public class WeaponService : IWeaponService
  {

    private DataContext _context;
    private IHttpContextAccessor _httpContext;
    private IMapper _mapper;
    private int GetUserId() => int.Parse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    public WeaponService(DataContext context, IHttpContextAccessor httpContext, IMapper mapper)
    {
        _context = context;
        _httpContext = httpContext;
        _mapper = mapper;
    }

    public async Task<GetCharacterDto> AddWeapon(AddWeaponDto newWeapon)
    {
        try
        {
            var currentUser = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.User.Id == GetUserId());

            if (character == null) { return null; }
            string type = SetType();
            string descriptor = SetDescriptor();
            Weapon weapon = new Weapon { Character = character, CharacterId = character.Id, Name = $"{type} of {descriptor}", Type = type };
            _context.Weapons.Add(weapon);
            await _context.SaveChangesAsync();
            return _mapper.Map<GetCharacterDto>(character);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private static string SetType()
    {
      var random = new Random();
      string[] types = new string[] {
        "Sword",
        "Dagger",
        "Staff",
        "Mace",
        "Foil",
        "Spoon",
        "Sponge",
        "Hammer",
        "Axe",
        "Halberd",
        "Knife",
        "Lance",
        "Claymore",
        "Pike",
        "Spear",
        "Club",
        "Flail",
        "War Scythe",
        "Baton",
        "Stick",
        "Cutlass",
        "Sabre",
        "Long Sword",
        "Short Sword",
        "Gladius",
        "Swiss Dagger",
        "Hatchet"
        };
      return types[random.Next(types.Length)];
    }

    private static string SetDescriptor()
    {
      var random = new Random();
      string[] descriptors = new string[] {
        "Justice",
        "Bravery",
        "Happiness",
        "Fear",
        "Calm",
        "Belief",
        "Sorrow",
        "Coldness",
        "Clarity",
        "Stupidity",
        "Luxury",
        "Luck",
        "Freedom",
        "Right",
        "Generosity",
        "Goodness",
        "Movement",
        "Sleep",
        "Awareness",
        "Beauty",
        "Love",
        "Pleasure",
        "Wisdom",
        "Appetite",
        "Loneliness",
        "Joy",
        "Hatred",
        "Solitude",
        "Peace",
        "Failure",
        "Talent",
        "Wit",
        "Honesty",
        "Dishonesty",
        "Fiction",
        "Truth",
        "Success",
        "Wealth",
        "Poverty",
        "Riches",
        "Care",
        "Confusion",
        "Brilliance",
        "Slavery",
        "Kindness",
        "Ability",
        "Thought",
        "Loss",
        "Gain",
        "Growth",
        "Anger",
        "Envy",
        "Irritation",
        "Generation",
        "Philosophy",
        "Talent",
        "Timing",
        "Marriage",
        "Divorce",
        "Advantage",
        "Argument",
        "Horror",
        "Pudding",
        "Uncle Dave",
        "Righteousness",
        "Diversity",
        "Adversity",
        "Instigation",
        "Libation",
        "Inebriation",
        "Fornication",
        "Dedication",
        "Class"
      };

      return descriptors[random.Next(descriptors.Length)];
    }
  }
}
