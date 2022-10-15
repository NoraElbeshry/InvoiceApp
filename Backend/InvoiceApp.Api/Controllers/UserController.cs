using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using InvoiceApp.Model.DTO;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace InvoiceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        IConfiguration _configuration;
        SignInManager<IdentityUser> SignInManager;
        IHttpContextAccessor HttpContextAccessor;
        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            SignInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<ActionResult> PostApplicationUser(LoginUserDto model)
        {
            var applicationUser = new IdentityUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                if (result.Succeeded)
                {
                    if (await _roleManager.FindByNameAsync("Admin") == null)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        await _userManager.AddToRoleAsync(applicationUser, "Admin");
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/User/Login
        public async Task<ActionResult> Login(LoginUserDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("sub",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:JWT_Secret"])), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                var userData = new { user.Email };
                return Ok(new { token, userData });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }
    }
}
