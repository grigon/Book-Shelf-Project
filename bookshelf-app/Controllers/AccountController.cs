using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using bookshelf_app.Auth;
using bookshelf_app.Migrations;
using bookshelf.DAL;
using bookshelf.DTO.Create;
using bookshelf.DTO.Read;
using bookshelf.Model.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace bookshelf_app.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RsaSecurityKey _key;
        private readonly IUserRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenManager _tokenManager;

        public AccountController(ILogger<AccountController> logger, SignInManager<User> signInManager,
            UserManager<User> userManager, IMapper mapper, RsaSecurityKey key, IUserRepository<User> userRepository,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ITokenManager tokenManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _key = key;
            _userRepository = userRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _tokenManager = tokenManager;
        }

        [HttpPost("CreateToken")]
        public async Task<IActionResult> CreateToken(UserLoginDTO loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user != null)
                {
                    var result = _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                    if (result.Result == SignInResult.Success)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                        var creds = new SigningCredentials(
                            _key,
                            SecurityAlgorithms.RsaSha256Signature);

                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddSeconds(Convert.ToDouble(_configuration["Tokens:TimeValid"])),
                            signingCredentials: creds
                        );

                        await _userManager.RemoveAuthenticationTokenAsync(user, "BookShelf", "RefreshToken");
                        var newRefreshToken = CreateRefreshToken(user);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            refreshToken = newRefreshToken
                        };
                        return Created("", results);
                    }

                    return BadRequest("Bad User name or password");
                }
            }

            return BadRequest("User not found");
        }


        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to logout");
            }
        }

        [HttpPost("CancelToken")]
        public async Task<IActionResult> CancelAccessToken()
        {
            await _tokenManager.DeactivateCurrentAsync();
            return NoContent();
        }

        [Authorize]
        [HttpGet("RefreshAccessToken")]
        public async Task<IActionResult> RefreshAccessToken([FromBody] string refreshToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var isValid =
                    await _userManager.VerifyUserTokenAsync(user, "BookShelf", "RefreshToken", "RefreshToken");

                if (isValid)
                {
                    if (refreshToken ==
                        _userManager.GetAuthenticationTokenAsync(user, "BookShelf", "RefreshToken").ToString())
                    {
                        var token = await CreateToken(_mapper.Map<UserLoginDTO>(user));
                        return Ok(token);
                    };
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to refresh token");
            }

            return BadRequest();
        }

        [HttpPost("CreateRefreshToken")]
        public async Task<IActionResult> CreateRefreshToken(User user)
        {
            var newRefreshToken =
                await _userManager.GenerateUserTokenAsync(user, "BookShelf", "RefreshToken");
            await _userManager.SetAuthenticationTokenAsync(user, "BookShelf", "RefreshToken",
                newRefreshToken);
            return Ok(newRefreshToken);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user,
                    model.Password,
                    false,
                    // model.RememberMe, 
                    false);
                if (result.Succeeded)
                {
                    var token = await CreateToken(model);
                    return Ok(token);
                }
            }

            ModelState.AddModelError("", "Failed to login.");

            return BadRequest();
        }
    }
}