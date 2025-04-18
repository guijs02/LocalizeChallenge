using LocalizeApi.Context;
using LocalizeApi.Repository.Interfaces;

namespace LocalizeApi.Repository
{
    public class UserRepository(LocalizeDbContext context) : IUserRepository
    {
        // Implement methods for user management here
    }
}
