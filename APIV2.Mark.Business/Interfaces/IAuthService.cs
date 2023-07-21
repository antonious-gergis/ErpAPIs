
using APIV2.Mark.Database.Models;

namespace APIV2.Mark.Business.Interfaces
{
    public interface IAuthService
    {
        Task<UserAccount> Register(UserAccount user, string password);
        Task<UserAccount> Login(string username, string password);
        Task<bool> UserExist(string username);
    }
}
