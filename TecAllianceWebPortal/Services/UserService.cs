using Microsoft.EntityFrameworkCore;
using TecAllianceWebPortal.Model;
using TecAllianceWebPortal.Services.Interfaces;

namespace TecAllianceWebPortal.Services
{

    public class UserService : IUserService
    {
        public readonly PortalDBContext _context;

        public UserService(PortalDBContext context)
        {
            _context = context;
        }

        public async Task GenerateTestUsers()
        {
            if (!_context.Users.Any())
            {
                var testUsers = new List<User>()
            {
                new User() { Email = "test1@mail.com" },
                new User() { Email = "test2@mail.com" },
                new User() { Email = "test3@mail.com" },
            };

                _context.Users.AddRange(testUsers);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Please enter correct email");
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }
    }
}
