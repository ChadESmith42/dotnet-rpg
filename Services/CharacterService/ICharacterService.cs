using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.DTOs.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<List<GetCharacterDto>> GetAllCharacters();
        Task<GetCharacterDto> GetCharacterById(int id);
        Task<Boolean> AddCharacter(AddCharacterDto character);
        Task<GetCharacterDto> UpdateCharacter(UpdateCharacterDto updated);
        Task<List<GetCharacterDto>> DeleteCharacter(int Id);
    }
}
