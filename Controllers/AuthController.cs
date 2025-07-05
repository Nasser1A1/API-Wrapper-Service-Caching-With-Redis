using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Weather_API_Wrapper_Service.DTOs;
using Weather_API_Wrapper_Service.Helpers.WeatherApiWrapper.Helpers;

namespace Weather_API_Wrapper_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtHelper _jwtHelper;

        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            JwtHelper jwtHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
        {
            if(requestDto == null ||  string.IsNullOrEmpty(requestDto.Email) || string.IsNullOrEmpty(requestDto.Password))
            {
                return BadRequest("Invalid registration request.");
            }

            var user = new IdentityUser { UserName = requestDto.Email, Email = requestDto.Email };
            var result = await _userManager.CreateAsync(user, requestDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        {
            if (requestDto == null || string.IsNullOrEmpty(requestDto.Email) || string.IsNullOrEmpty(requestDto.Password))
            {
                return BadRequest("Invalid Login request.");
            }

            var user = await _userManager.FindByEmailAsync(requestDto.Email);
            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            if (!await _userManager.CheckPasswordAsync(user, requestDto.Password))
            {
                return Unauthorized("Invalid password.");
            }
            var token = _jwtHelper.GenerateToken(user);

            var response = new
            {
                UserId = user.Id,
                Token = token,
                Authentication = "Bearer",


            };



            return Ok(response);
        }

    }
}
