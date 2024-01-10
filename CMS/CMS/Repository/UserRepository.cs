using CMS.DatabaseContext;
using CMS.Interface;
using CMS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;

namespace CMS.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _authDbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(AuthDbContext authDbContext, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _authDbContext = authDbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> ChangePass(ChangePass change, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ChangePasswordAsync(user, change.OldPass, change.NewPass);
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(result.Errors.ToArray());
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Tuple<bool, IdentityUser>> ValidateUser(LogIn login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, login.RememberMe, false);
            if (user != null && result.Succeeded)
            {
                return new Tuple<bool, IdentityUser>(true, user);
            }
            return new Tuple<bool, IdentityUser>(false, null);
        }

        public async Task<Tuple<bool, List<IdentityError>>> CreateNewUser(SignUp register)
        {
            var user = new IdentityUser()
            {
                UserName = register.Username
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                return new Tuple<bool, List<IdentityError>>(true, null);
            }
            return new Tuple<bool, List<IdentityError>>(false, result.Errors.ToList());
        }
    }
}
