using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserSkillsRepository : IUserSkillsRepository
    {
        private readonly DevFreelaDbContext _context;
        public UserSkillsRepository(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task AddRange(List<UserSkill> userSkills)
        {
            await _context.UserSkills.AddRangeAsync(userSkills);
            await _context.SaveChangesAsync();
        }
    }
}
