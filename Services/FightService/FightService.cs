using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.DTOs.Fight;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        //TODO: add IHttpContextAccessor to get authenticated user;
        //TODO: add Authorization

        public FightService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    public async Task<FightResultDto> Fight(FightRequestDto challenge)
    {
        FightResultDto results = new FightResultDto();
      try
      {
        var characters = await _context.Characters
            .Include(c => c.Weapon)
            .Include(c => c.Skills)
            .Where(c => challenge.CharacterIds.Contains(c.Id)).ToListAsync();

        bool defeated = false;
        while (!defeated)
        {
            foreach(Character attacker in characters)
            {
                var defenders = characters.Where(c => c.Id != attacker.Id).ToList();
                var defender = defenders[new Random().Next(defenders.Count)];
                int damage = 0;
                string attackUsed = string.Empty;
                bool useWeapon = new Random().Next(2) == 0;
                if (useWeapon) {
                    attackUsed = attacker.Weapon.Name;
                    damage = CalculateWeaponDamage(attacker, defender);
                } else {
                    var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
                    attackUsed = skill.Name;
                    damage = CalculateSkillDamage(attacker, defender, skill);
                }
                results.Log.Add($"{attacker.Name} attacked {defender.Name} with {attackUsed} causing {(damage > 0 ? damage : 0)} damage.");
                if (defender.HitPoints <= 0)
                {
                    defeated = true;
                    attacker.Victories++;
                    defender.Defeats++;
                    results.Log.Add($"{defender.Name} has been defeated!");
                    results.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} Hit Points remaining.");
                }
            }
        }
        characters.ForEach(c => {
            c.Fights++;
            c.HitPoints = 100;
        });
        await _context.SaveChangesAsync();
        return results;
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public async Task<AttackResultDto> SkillAttack(SkillAttackDto attack)
    {
      try
      {
        var attacker = await _context.Characters
            .Include(c => c.Skills)
            .FirstOrDefaultAsync(c => c.Id == attack.AttackerId);
        var defender = await _context.Characters
            .Include(c => c.Skills)
            .FirstOrDefaultAsync(c => c.Id == attack.DefenderId);

        var skill = attacker.Skills.FirstOrDefault(s => s.Id == attack.SkillId);

        if (skill == null) { throw new Exception("Attacker doesn't know that skill."); }

        int damage = CalculateSkillDamage(attacker, defender, skill);
        string message = defender.HitPoints <= 0 ? $"{defender.Name} has been defeated!" : $"{defender.Name} lives to fight another day!";

        await _context.SaveChangesAsync();

        AttackResultDto response = new AttackResultDto
        {
          AttackerName = attacker.Name,
          DefenderName = defender.Name,
          AttackerHitPoints = attacker.HitPoints,
          DefenderHitPoints = defender.HitPoints,
          Damage = damage,
          Message = message
        };

        return response;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private static int CalculateSkillDamage(Character attacker, Character defender, Skill skill)
    {
      int damage = skill.Damage + (new Random().Next(attacker.Intelligence));
      damage -= new Random().Next(defender.Defense);
      if (damage > 0) { defender.HitPoints -= damage; }

      return damage;
    }

    public async Task<AttackResultDto> WeaponAttack(WeaponAttackDto attack)
    {
        try
      {
        var attacker = await _context.Characters
            .Include(c => c.Weapon)
            .FirstOrDefaultAsync(c => c.Id == attack.AttackerId);
        var defender = await _context.Characters
            .Include(c => c.Weapon)
            .FirstOrDefaultAsync(c => c.Id == attack.DefenderId);

        int damage = CalculateWeaponDamage(attacker, defender);
        string message = defender.HitPoints <= 0 ? $"{defender.Name} has been defeated!" : $"{defender.Name} lives to fight another day!";

        await _context.SaveChangesAsync();

        AttackResultDto response = new AttackResultDto
        {
          AttackerName = attacker.Name,
          DefenderName = defender.Name,
          AttackerHitPoints = attacker.HitPoints,
          DefenderHitPoints = defender.HitPoints,
          Damage = damage,
          Message = message
        };

        return response;
      }
      catch (Exception ex)
        {
            throw ex;
        }
    }

    private static int CalculateWeaponDamage(Character? attacker, Character? defender)
    {
      int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
      damage -= new Random().Next(defender.Defense);
      if (damage > 0) { defender.HitPoints -= damage; }

      return damage;
    }

    public async Task<List<HighScoreDto>> GetHighScore()
    {
      try
      {
        var characters = await _context.Characters
            .Where(c => c.Fights > 0)
            .OrderByDescending(c => c.Victories)
            .ThenBy(c => c.Defeats)
            .ToListAsync();

        return characters.Select(c => _mapper.Map<HighScoreDto>(c)).ToList();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
