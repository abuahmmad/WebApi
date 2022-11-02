
using WebApi1.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi1.service
{
    public class AccountService : IAccountService
    {
        /*private readonly */
        UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signinManager;
        private readonly RoleManager<IdentityRole> roleManage;

        //using Microsoft.AspNetCore.Identity; // using Authentiacation.Models;
        public AccountService(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signinManager, RoleManager<IdentityRole> _roleManage)
        {
            userManager = _userManager; // class to execute any process on table aspnetuser
            signinManager = _signinManager; // class execute sign in (compare & match) 
            roleManage = _roleManage;
        }

        //insert
        public async Task<IdentityResult> CreateAccount(SignUpModel signUp)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = signUp.Username;
            user.Email = signUp.Email;
            user.Name = signUp.Name;
            user.BDate = signUp.Bdate;
            //user.PasswordHash = signUp.Password;
            //CreateAsync do insert on Aspnetusers Table
            //var result = await userManager.CreateAsync(user);
            var result = await userManager.CreateAsync(user, signUp.Password);
            return result;
        }

        public async Task<SignInResult> SignIn(SignInModel signIn)
        {
            //var result = await signinManager.PasswordSignInAsync(signIn.Username, signIn.Password,false, false);
            var result = await signinManager.PasswordSignInAsync(signIn.Username, signIn.Password, signIn.Rememberme, false);
            return result;
        }

        //--------------------------------------------------------------------------------
        //insert
        public async Task<IdentityResult> AddRole(RoleModel roleModel)
        {
            IdentityRole role = new IdentityRole();
            role.Name = roleModel.Name;
            var result = await roleManage.CreateAsync(role);

            return result;
        }

        //get info of All user
        public List<ApplicationUser> GetUser()
        {
            //return userManager.Users.ToList();
            List<ApplicationUser> li = userManager.Users.ToList();

            return li;
        }

        //After click on Add Button get his id and take his name and All Data of Table Role
        //public List<UserRole> GetRoles(string UserId)
        public async Task<List<UserRole>> GetRoles(string UserId)
        {
            List<IdentityRole> liRole = roleManage.Roles.ToList();
            List<UserRole> LiuserRole = new List<UserRole>();
            foreach (IdentityRole item in liRole)
            {
                UserRole uRole = new UserRole();
                uRole.RoleId = item.Id;
                uRole.RoleName = item.Name;
                uRole.UserId = UserId;
                uRole.IsSelected = false;
                LiuserRole.Add(uRole);
            }

            var user = await userManager.FindByIdAsync(UserId);
            var Roles = await userManager.GetRolesAsync(user);
            foreach (UserRole uR in LiuserRole)
            {
                //var user = await userManager.FindByIdAsync(uR.UserId);
                //get all role for this user from table aspnetuserrole
                //Roles is list of string
                //var Roles = await userManager.GetRolesAsync(user);
                foreach (string R in Roles)
                {
                    if (R == uR.RoleName)
                    {
                        uR.IsSelected = true;
                    }
                }

            }
            return LiuserRole;


        }

        //After Click Update save authority
        public async Task UpdateuserRole(List<UserRole> liUserRole)
        {

            foreach (UserRole item in liUserRole)
            {
                var user = await userManager.FindByIdAsync(item.UserId);
                //if select add to table
                if (item.IsSelected == true)
                {
                    //if exist in table don't Add else if Added
                    if (await userManager.IsInRoleAsync(user, item.RoleName) == false)
                    {
                        await userManager.AddToRoleAsync(user, item.RoleName);
                    }
                }
                else
                {   //if exist in tabel 
                    if (await userManager.IsInRoleAsync(user, item.RoleName) == true)
                    {   //remove
                        await userManager.RemoveFromRoleAsync(user, item.RoleName);
                    }
                }

            }
        }

        public async Task Logout()
        {
            await signinManager.SignOutAsync();

        }

        //--------------------------------------------------------------------------------
        //new

        public async Task<ApplicationUser> getOneUser(string username)
        {
            var result = await userManager.FindByNameAsync(username);
            return result;
        }

        public List<string> getUserRoles(ApplicationUser obj)
        {
            List<string> lii = userManager.GetRolesAsync(obj).Result.ToList();
            var li = userManager.GetRolesAsync(obj).Result.ToList();
            //var Roles = await userManager.GetRolesAsync(user);
            return li;
        }


    }
}
