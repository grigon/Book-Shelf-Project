using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using bookshelf.DTO.Create;
using bookshelf.DTO.Read;
using bookshelf.Model.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RsaSecurityKey _key;
        private readonly IConfiguration _configuration;

        public AccountController(ILogger<AccountController> logger, SignInManager<User> signInManager,
            UserManager<User> userManager, IMapper mapper, RsaSecurityKey key,
            IConfiguration configuration)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _key = key;
            _configuration = configuration;
        }

        [HttpPost("CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] UserReadDTO readDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(readDto.Email);
                Console.WriteLine(user.Email);
                Console.WriteLine(readDto.Password);
                if (user != null)
                {
                    var result = _signInManager.CheckPasswordSignInAsync(user, readDto.Password, false);
                    Console.WriteLine(result.ToString());    
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
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: creds
                        );

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };
                        return Created("", results);
                    }
                    return BadRequest("Bad User name or password");
                    
                }
            }
            return BadRequest("User not found");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO model)
        {
            var user = _mapper.Map<User>(model);
            if (ModelState.IsValid)
            {
                await _signInManager.SignInAsync(user, false);
            }
            return BadRequest();
        } 
    }
}