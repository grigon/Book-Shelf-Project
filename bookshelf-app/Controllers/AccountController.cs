using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using bookshelf_app.Auth;
using bookshelf.DAL;
using bookshelf.DTO.Create;
using bookshelf.Model.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public async Task<IActionResult> CreateToken(UserLoginDTO loginDto, bool refresh)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user != null)
                {
                    var IsSuccess = false;
                    if (!refresh)
                    {
                        var result = _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                        IsSuccess = (result.Result == SignInResult.Success);
                    }
                    else
                    {
                        IsSuccess = true;
                    }

                    if (IsSuccess)
                    {
                        var claims = new List<Claim> { };
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Count == 0)
                        {
                            claims = new List<Claim>
                            {
                                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                            };
                        }
                        else
                        {
                            claims = new List<Claim>
                                {
                                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                                    new Claim(ClaimTypes.Role, roles.Last())
                                }
                                ;
                        }
                        
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
                        var newRefreshToken = "";
                        if (!refresh)
                        {
                            newRefreshToken =
                                await _userManager.GenerateUserTokenAsync(user, "BookShelf", "RefreshToken");
                            await _userManager.SetAuthenticationTokenAsync(user, "BookShelf", "RefreshToken",
                                newRefreshToken);
                        }
                        
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
                if (User.Identity != null)
                {
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _userManager.RemoveAuthenticationTokenAsync(user, "BookShelf", "RefreshToken");
                    await _tokenManager.DeactivateCurrentAsync();
                    await _signInManager.SignOutAsync();
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to logout");
            }

            return BadRequest();
        }

        // [Authorize]
        // [HttpPost("CancelToken")]
        // public async Task<IActionResult> CancelAccessToken()
        // {
        //     await _tokenManager.DeactivateCurrentAsync();
        //     return NoContent();
        // }
        
        [Authorize]
        [HttpPut("admin")]
        public async Task<IActionResult> Admin()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                return Ok();
            }

            return BadRequest();
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
                        var token = await CreateToken(_mapper.Map<UserLoginDTO>(user), true);
                        return Ok(token);
                    }

                    ;
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
                    var token = await CreateToken(model, false);
                    return Ok(token);
                }
            }

            ModelState.AddModelError("", "Failed to login.");

            return BadRequest();
        }
    }
}