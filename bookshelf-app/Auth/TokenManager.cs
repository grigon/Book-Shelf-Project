using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.JsonWebTokens;

namespace bookshelf_app.Auth
{
    public class TokenManager : ITokenManager
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<JwtBearerOptions> _jwtOptions;
        private readonly IConfiguration _configuration;

        public TokenManager(IDistributedCache cache, IHttpContextAccessor httpContextAccessor,
            IOptions<JwtBearerOptions> jwtOptions, IConfiguration configuration
            )
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _jwtOptions = jwtOptions;
            _configuration = configuration;
        }


        public async Task<bool> IsCurrentActiveTokenAsync()
            => await IsActiveAsync(GetCurrentTokenAsync());

        public async Task DeactivateCurrentAsync()
            => await DeactivateAsync(GetCurrentTokenAsync());
 
        public async Task<bool> IsActiveAsync(string token)
            => await _cache.GetStringAsync(GetKkey(token)) == null;

        public async Task DeactivateAsync(string token)
            => await _cache.SetStringAsync(GetKkey(token), " ", new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow
                    = TimeSpan.FromSeconds(Convert.ToDouble(_configuration["Tokens:TimeValid"]))
            });

        private string GetCurrentTokenAsync()
        {
            if (_httpContextAccessor
                .HttpContext != null)
            {
                var authorizationHeader = _httpContextAccessor
                    .HttpContext.Request.Headers["authorization"];
 
                return authorizationHeader == StringValues.Empty
                    ? string.Empty
                    : authorizationHeader.Single().Split(" ").Last();
            }
            return "";
        }
        private static string GetKkey(string token)
            => $"tokens: {token} deactivated";


    }
}