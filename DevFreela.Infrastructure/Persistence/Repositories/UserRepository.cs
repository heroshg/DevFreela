using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _context;

        public UserRepository(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
    }
}
