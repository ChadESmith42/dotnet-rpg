using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.DTOs.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    public CharacterService(IMapper mapper, DataContext context)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<Boolean> AddCharacter(AddCharacterDto character)
    {
      Character newCharacter = _mapper.Map<Character>(character);
      _context.Characters.Add(newCharacter);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<List<GetCharacterDto>> DeleteCharacter(int Id)
    {
      Character character = _context.Characters.First(c => c.Id == Id);
      _context.Characters.Remove(character);
      await _context.SaveChangesAsync();
      var response = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return response;
    }

    public async Task<List<GetCharacterDto>> GetAllCharacters()
    {
      var characters = await _context.Characters.ToListAsync();
      var response = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return response;
    }

    public async Task<GetCharacterDto> GetCharacterById(int id)
    {
      var character = await _context.Characters.FindAsync(id);
      if (character == null) { throw new Exception("Character not found."); }
      return _mapper.Map<GetCharacterDto>(character);
    }

    public async Task<GetCharacterDto> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {

      try
      {
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
        character.Name = updatedCharacter.Name;
        character.HitPoints = updatedCharacter.HitPoints;
        character.Strength = updatedCharacter.Strength;
        character.Defense = updatedCharacter.Defense;
        character.Intelligence = updatedCharacter.Intelligence;
        character.Class = updatedCharacter.Class;

        await _context.SaveChangesAsync();
        return _mapper.Map<GetCharacterDto>(character);
      }
      catch (System.Exception)
      {
        throw new Exception("No character found");
      }
    }
  }
}
