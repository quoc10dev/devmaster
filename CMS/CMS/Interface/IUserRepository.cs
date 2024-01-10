using CMS.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CMS.Interface
{
    public interface IUserRepository
    {
        Task<Tuple<bool, IdentityUser>> ValidateUser(LogIn login);
        Task Logout();
        Task<Tuple<bool, List<IdentityError>>> CreateNewUser(SignUp register);
        Task<IdentityResult> ChangePass(ChangePass change, string userName);
    }
}
