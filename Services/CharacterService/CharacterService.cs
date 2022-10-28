using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.DTOs.Character;

namespace dotnet_rpg.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private static List<Character> characters = new List<Character> {
            new Character(),
            new Character { Id = 1, Name = "Steve"}
    };

    private readonly IMapper _mapper;
    public CharacterService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<List<AddCharacterDto>> AddCharacter(Character character)
    {
      characters.Add(_mapper.Map<Character>(character));
      var response = characters.Select(c => _mapper.Map<AddCharacterDto>(c)).ToList();
      return response;
    }

    public async Task<List<GetCharacterDto>> DeleteCharacter(int Id)
    {
        Character character = characters.First(c => c.Id == Id);
        characters.Remove(character);
        var response = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return response;
    }

    public async Task<List<GetCharacterDto>> GetAllCharacters()
    {
      var response = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return response;
    }

    public async Task<GetCharacterDto> GetCharacterById(int id)
    {
      return _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));
    }

    public async Task<GetCharacterDto> UpdateCharacter(UpdateCharacterDto updatedCharacter){

        try
        {
            Character character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
            _mapper.Map(updatedCharacter, character);
            return _mapper.Map<GetCharacterDto>(character);
        }
        catch (System.Exception)
        {
            throw new Exception("No character found");
        }
    }
  }
}
