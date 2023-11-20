using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project_3_quiz_api.Data;
using project_3_quiz_api.Models.DBModels;
using project_3_quiz_api.Models.DTO;
using project_3_quiz_api.Repositories.Interfaces;

namespace project_3_quiz_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly ApplicationDbContext _context;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, ApplicationDbContext context)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _context = context;
        }

        //Post: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username

            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                // add role to this user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        // Add new user to db
                        var user = new UserModel
                        {
                            Id = Guid.NewGuid(),
                            Email = registerRequestDto.Username
                        };
                        await _context.Users.AddAsync(user);
                        await _context.SaveChangesAsync();
                        return Ok("The user was registered! You can now login");
                    }
                }
            }
            return BadRequest("Sorry, it did not work this time");
        }
        //Post: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    // get a role for the user
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwttoken = _tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            AccessToken = jwttoken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("username or password was incorrect");
        }
    }
}
