using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi1.data;
using WebApi1.Models;
using WebApi1.service;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IConfiguration configuration;

        public AccountController(IAccountService _account,IConfiguration _configuration)
        {
            accountService = _account;
            configuration = _configuration;
        }

        [HttpPost]
        [Route("CreateAccount")]
        public async Task<IActionResult> CreateAccount(SignUpModel signup)
        {

            IdentityResult result = await accountService.CreateAccount(signup);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, result.Errors);
            }




        }


        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole(RoleModel roleModel)
        {
            var result = await accountService.AddRole(roleModel);


            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, result.Errors);
            }

        }


        [HttpGet]
        [Route("GetUsers")]
        public List<ApplicationUser> GetUsers()
        {
            List<ApplicationUser> li = accountService.GetUser();

            return li;


        }


         [HttpGet]
         [Route("UserRoles")]
        public async  Task<List<UserRole>> UserRoles(string UserId)
        {

            List<UserRole> liR = await accountService.GetRoles(UserId);


            return liR;

        }


        [HttpPost]
        //[HttpGet]
        [Route("UpdateUserRole")]
        public async Task<List<UserRole>> UpdateUserRole(List<UserRole> liuserRole)
        {
            
            await accountService.UpdateuserRole(liuserRole);
            List<UserRole> liR = await accountService.GetRoles(liuserRole[0].UserId);

            return liR;


           
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(SignInModel signInModel)
        {
            var result = await accountService.SignIn(signInModel);
            if (result.Succeeded)
            {
                //part one
                //1
                //Claim cc = new Claim("Username",signInModel.Username);
                //OR
                //Claim cc = new Claim("Username", "123);

                //2   
                //List<Claim> li = new List<Claim>();
                //Claim c = new Claim("Username", signInModel.Username);
                //li.Add(c);
                //c = new Claim("Currentdate", DateTime.Now.ToShortDateString());
                //li.Add(c);
                //c = new Claim("UniqueValue", Guid.NewGuid().ToString());
                //li.Add(c);

                //3
                //List<Claim> li = new List<Claim>
                //{
                //    new Claim("Username",signInModel.Username),
                //    new Claim("Currentdate",DateTime.Now.ToShortDateString()),
                //    new Claim("UniqueValue", Guid.NewGuid().ToString())
                //};

                //4
                var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,signInModel.Username),
                    new Claim("UniqueValue",Guid.NewGuid().ToString())
                }; 

                var user = await accountService.getOneUser(signInModel.Username);

                var roles = accountService.getUserRoles(user);

                foreach (var item in roles)
                {
                    authClaim.Add(new Claim(ClaimTypes.Role, item));
                }


                //part two
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                            issuer: configuration["JWT:ValidIssuer"],
                            audience: configuration["JWT:ValidAudience"],
                            expires: DateTime.Now.AddDays(15),
                            claims: authClaim, //hashing
                            //claims: li, //hashing li 
                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );


                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
                //build token
            }
            else
            {
                return Unauthorized();
            }
        }



                                                                                    























    }
}
