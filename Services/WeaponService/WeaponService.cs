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
        Weapon weapon = ForgeWeapon(character);
        _context.Weapons.Add(weapon);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetCharacterDto>(character);
      }
      catch (Exception ex)
        {
            throw ex;
        }
    }

    private static Weapon ForgeWeapon(Character character)
    {
      string type = SetType();
      string descriptor = SetDescriptor();
      Weapon weapon = new Weapon { Name = $"{type} of {descriptor}", Type = type };
      weapon.Damage = SetDamage(character);
      weapon.Damage = SetSpecialWeapons(weapon.Name, weapon.Damage);
      weapon.Value = SetValue(weapon.Damage);
      weapon.Repairable = SetRepairable(weapon.Value);
      return weapon;
    }

    private static bool SetRepairable(int value) {
      return value > 40;
    }

    private static int SetDamage(Character character) {
      int damage = 0;
      switch (character.ClassLevel)
      {
        case 0:
          damage = new Random().Next(15);
          break;
        case 1:
          damage = new Random().Next(15, 25);
          break;
        case 2:
          damage = new Random().Next(25, 35);
          break;
        case 3:
          damage = new Random().Next(35, 45);
          break;
        case 4:
          damage = new Random().Next(45, 55);
          break;
        default:
          damage = new Random().Next(25, 75);
          break;
      }
      return damage;
    }

    private static int SetSpecialWeapons(string name, int damage)
    {
      List<string> specialWeapons = new List<string> { "Spoon of Pudding", "Sword of Truth", "Sponge of Reckoning" };
      List<string> lowQualityWeaponDescriptors = new List<string> { "Shame", "Sorrow", "Loss", "Poverty" };
      return specialWeapons.Any(w => w == name) ? 100 :
        lowQualityWeaponDescriptors.Any(d => name.Contains(d)) ? 15 : damage;
    }

    private static int SetValue(int damage)
    {
      var value = damage * 1.5;
      return (int)Math.Round(value, 0);
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
        "Class",
        "Reckoning"
      };

      return descriptors[random.Next(descriptors.Length)];
    }

    public async Task<GetCharacterDto> SellWeapon(ModifyWeaponDto modifyWeapon)
    {
      var character = await _context.Characters.Include(c => c.Inventory).FirstOrDefaultAsync(c => c.Id == modifyWeapon.CharacterId);
      var saleItem = character.Inventory.Weapons.FirstOrDefault(w => w.Id == modifyWeapon.WeaponId);
      saleItem.IsActive = false;
      character.Inventory.Money += saleItem.Value;
      _context.Remove(saleItem);
      await _context.SaveChangesAsync();
      return _mapper.Map<GetCharacterDto>(character);
    }

    public async Task<GetCharacterDto> RepairWeapon(ModifyWeaponDto modifyWeapon)
    {
      var character = await _context.Characters
        .Include(c => c.Inventory)
        .FirstOrDefaultAsync(c => c.Id == modifyWeapon.CharacterId);
      Weapon weapon = character.Inventory.Weapons.FirstOrDefault(w => w.Id == modifyWeapon.WeaponId);
      if (weapon.Repairable) {
        weapon.Durability = weapon.Durability + 25 < 100 ? weapon.Durability + 25 : 100;
      }
      await _context.SaveChangesAsync();
      return _mapper.Map<GetCharacterDto>(character);
    }

    public async Task<GetCharacterDto> StoreWeapon(ModifyWeaponDto modifyWeapon)
    {
      var character = await _context.Characters
        .Include(c => c.Inventory)
        .FirstOrDefaultAsync(c => c.Id == modifyWeapon.CharacterId);

      Weapon weapon = character.Inventory.Weapons.FirstOrDefault(w => w.Id == modifyWeapon.WeaponId);
      weapon.IsActive = false;
      await _context.SaveChangesAsync();
      return _mapper.Map<GetCharacterDto>(character);
    }

    public async Task<GetCharacterDto> RetrieveWeapon(ModifyWeaponDto modifyWeapon)
    {
      var character = await _context.Characters
        .Include(c => c.Inventory)
        .FirstOrDefaultAsync(c => c.Id == modifyWeapon.CharacterId);
      Weapon activeWeapon = character.Inventory.Weapons.FirstOrDefault(w => w.IsActive == true && w.Id != modifyWeapon.WeaponId);
      if (activeWeapon != null) {
        activeWeapon.IsActive = false;
      }
      Weapon getWeapon = character.Inventory.Weapons.FirstOrDefault(w => w.Id == modifyWeapon.WeaponId);
      getWeapon.IsActive = true;
      await _context.SaveChangesAsync();
      return _mapper.Map<GetCharacterDto>(character);
    }

    public async Task<GetCharacterDto> DropWeapon(ModifyWeaponDto modifyWeapon)
    {
      var character = await _context.Characters
        .Include(c => c.Inventory)
        .FirstOrDefaultAsync(c => c.Id == modifyWeapon.CharacterId);
      Weapon weapon = character.Inventory.Weapons.FirstOrDefault(w => w.Id == modifyWeapon.WeaponId);
      if (weapon != null)
      {
        _context.Remove(weapon);
        await _context.SaveChangesAsync();
      }
      return _mapper.Map<GetCharacterDto>(character);
    }
  }
}
