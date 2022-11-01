using System.Security.Claims;
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
    private readonly IHttpContextAccessor _httpContext;
    public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContext)
    {
      _mapper = mapper;
      _context = context;
      _httpContext = httpContext;
    }

    private int GetUserId() => int.Parse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    public async Task<Boolean> AddCharacter(AddCharacterDto character)
    {
      Character newCharacter = _mapper.Map<Character>(character);
      newCharacter.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
      _context.Characters.Add(newCharacter);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<List<GetCharacterDto>> DeleteCharacter(int Id)
    {
      Character character = _context.Characters.First(c => c.Id == Id && c.User.Id == GetUserId());
      if (character == null) { return null; }
      _context.Characters.Remove(character);
      await _context.SaveChangesAsync();
      var response = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return response;
    }

    public async Task<List<GetCharacterDto>> GetAllCharacters()
    {
      var characters = await _context.Characters
        .Include(c => c.Inventory)
        .Include(c => c.Skills)
        .Where(c => c.User.Id == GetUserId()).ToListAsync();
      var response = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return response;
    }

    public async Task<GetCharacterDto> GetCharacterById(int id)
    {
      var character = await _context.Characters
        .Include(c => c.Inventory)
        .Include(c => c.Skills)
        .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
      if (character == null) { throw new Exception("Character not found."); }
      return _mapper.Map<GetCharacterDto>(character);
    }

    public async Task<GetCharacterDto> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
      try
      {
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id && c.User.Id == GetUserId());
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

    public async Task<GetCharacterDto> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
      try
      {
        Character character = await _context.Characters
          .Include(c => c.Inventory)
          .Include(c => c.Skills)
          .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.User.Id == GetUserId());
        if (character == null) { return null; }
        var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
        if (skill == null) { return null;}
        character.Skills.Add(skill);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetCharacterDto>(character);

      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
