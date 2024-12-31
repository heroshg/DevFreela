using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface ISkillRepository
    {
        Task<int> Add(Skill skill);
    }
}
