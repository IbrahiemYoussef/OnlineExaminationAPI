using FinalYearProject.Models;
using FinalYearProject.Models.DTOs;
using FinalYearProject.Models.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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

namespace FinalYearProject.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    

    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly mydbcon _context;
        
        public AuthenticateController(mydbcon context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            
        }
        
        [HttpPost]
        [Route("Login")]
        public async Task<GlobalResponseDTO> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user.LockoutEnd > DateTimeOffset.Now)
            {
                return new GlobalResponseDTO(false, "the account has been disabled", null);
            }
            else
            {
                if (user != null)
                {
                    if (await userManager.CheckPasswordAsync(user, model.Password))
                    {
                        var userRoles = await userManager.GetRolesAsync(user);
                        var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                        foreach (var userRole in userRoles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        }
                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                        var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                        var userdata = _context.ApplicationUsers.Where(x => x.UserName == model.Username).Select(s => new { s.Id, s.firstname, s.lastname, s.UserName, s.Email }).FirstOrDefault();

                        //return Ok(new{token = new JwtSecurityTokenHandler().WriteToken(token),expiration = token.ValidTo});
                        return new GlobalResponseDTO(true, "successed",
                            new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(token),
                                expiration = token.ValidTo,
                                userDetails = userdata,
                                role= await GetUserRole(model.Username)
                            }
                        );
                    }
                    else
                    {
                        return new GlobalResponseDTO(false, "wrong password", null);
                    }
                }

                else
                {
                    return new GlobalResponseDTO(false, "wrong username", null);
                }
            }
            
            
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FacultyId=model.FacultyId,
                firstname=model.firstname,
                lastname=model.lastname
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,  new Response { Status = "error" , Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("RegisterAny")]
        public async Task<IActionResult> RegisterAny([FromBody] RegisterModel model,string choice)
        {
            //admin , student, professor
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FacultyId = model.FacultyId,
                firstname = model.firstname,
                lastname = model.lastname
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.Student))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Student));
            if (!await roleManager.RoleExistsAsync(UserRoles.Professor))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Professor));

            //if (await roleManager.RoleExistsAsync(UserRoles.Student))
            //{
            //    await userManager.AddToRoleAsync(user, UserRoles.Student);
            //}

            if(choice[0].ToString().ToUpper() == "A")
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            else if (choice[0].ToString().ToUpper() == "S")
            {
                await userManager.AddToRoleAsync(user, UserRoles.Student);
            }
            else if (choice[0].ToString().ToUpper() == "P")
            {
                await userManager.AddToRoleAsync(user, UserRoles.Professor);
            }
            else
            {
                return Ok(new GlobalResponseDTO(false, "User creation failed!", null));
            }
            return Ok(new GlobalResponseDTO(true, $"{choice.ToUpper()} created successfully!", null));

        }

        [HttpGet]
        [Route("GetRoleName")]
        public async Task<IList<string>> GetUserRole(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            var rolenames = await userManager.GetRolesAsync(user);
            return rolenames;
        }
        [HttpGet]
        [Route("DisableUsername")]
        public async Task<IActionResult> BanAccs(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                
                user.LockoutEnd = DateTime.Today.AddYears(10);
                
                _context.SaveChanges(); 
                return Ok(new GlobalResponseDTO(true, "The account has been disabled", null));
            }
            else
            {
                return Ok(new GlobalResponseDTO(false, "wrong Id", null));
            }
        }
        [HttpGet]
        [Route("UnDisableUsername")]
        public async Task<IActionResult> UnBanAccs(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                
                user.LockoutEnd = null;

                _context.SaveChanges();
                return Ok(new GlobalResponseDTO(true, "The account has been enabled", null));
            }
            else
            {
                return Ok(new GlobalResponseDTO(false, "wrong Id", null));
            }
        }

    }
}
