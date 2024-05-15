using Inventarios.Server.AspNet.Dto_s;
using Inventarios.Server.AspNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inventarios.Server.AspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ResponseDto _response;
        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _response = new();
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var token = GenerateJwtToken(user);
                LoginResponseDto userModel = new();
                userModel.Id = user.Id;
                userModel.Username = user.UserName;
                userModel.Token = token;
                userModel.Photo = null;
                _response.Result = userModel;
                _response.DisplayMessage = "Welcome " +userModel.Username+ " ! ";
                return Ok(_response);
            }
            _response.DisplayMessage = "Invalid Credentials! ";
            return Unauthorized(_response);
        }
        [HttpPost("updateMyPassword")]
        [Authorize]
        public async Task<IActionResult> updateMyPassword(ChangePassword pwdModel)
        {
            try
            {
                string message,message2,message3;
                var result = await _signInManager.PasswordSignInAsync(pwdModel.UserName, pwdModel.OldPassword,isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = new IdentityUser { UserName = pwdModel.UserName, Email = pwdModel.UserName };
                    await _userManager.ChangePasswordAsync(user, pwdModel.OldPassword, pwdModel.NewPassword);
                    _response.DisplayMessage = "New password changed!";
                    return Ok(_response);
                }
                _response.ErrorMessage = "Wrong! , UserName and Password Incorrects!";
                return BadRequest(_response);
               
               
                
               
            }
            catch (Exception e)
            {
                _response.ErrorMessage = e.ToString();
                return BadRequest(_response);
            }
        }
        [HttpPost("register")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                //var userData = await _userManager.FindByEmailAsync(model.Email);
                var token = GenerateJwtToken(user);
                LoginResponseDto userModel = new();
                userModel.Id = user.Id;
                userModel.Username = user.UserName;
                userModel.Token = token;
                userModel.Photo = null;
                _response.Result = userModel;
                _response.DisplayMessage = "Welcome " + userModel.Username + " ! ";
                return Ok(_response);
               
               
            }
            _response.Result= result.Errors;
            _response.DisplayMessage = "Error Details!";
            return BadRequest(_response);
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
