using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IUserSkillsRepository
    {
        Task AddRange(List<UserSkill> userSkills);
    }
}
