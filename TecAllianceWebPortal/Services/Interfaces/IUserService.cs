using TecAllianceWebPortal.Model;

namespace TecAllianceWebPortal.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User?> GetUserByEmail(string email);
        public Task GenerateTestUsers();
    }
}
