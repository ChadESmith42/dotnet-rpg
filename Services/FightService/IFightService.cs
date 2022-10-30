using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.DTOs.Fight;

namespace dotnet_rpg.Services.FightService
{
    public interface IFightService
    {
        Task<AttackResultDto> WeaponAttack(WeaponAttackDto attack);
        Task<AttackResultDto> SkillAttack(SkillAttackDto attack);
        Task<FightResultDto> Fight(FightRequestDto challenge);
        Task<List<HighScoreDto>> GetHighScore();
    }
}
