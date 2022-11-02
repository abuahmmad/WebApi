
using WebApi1.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi1.service
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateAccount(SignUpModel signUp);

        Task<SignInResult> SignIn(SignInModel SignInModel);

        Task<IdentityResult> AddRole(RoleModel roleModel);

        List<ApplicationUser> GetUser();

        Task<List<UserRole>> GetRoles(string UserId);

        //List<UserRole> GetRoles(string UserId)

        Task UpdateuserRole(List<UserRole> liUserRole);

        Task Logout();

        List<string> getUserRoles(ApplicationUser obj);

        Task<ApplicationUser> getOneUser(string username);
    }
}
