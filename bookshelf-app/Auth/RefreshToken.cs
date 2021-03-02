using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using bookshelf.DAL;
using bookshelf.Model.Users;
using RTools_NTS.Util;

namespace bookshelf_app.Auth
{
    public class RefreshToken
    {
        // public RefreshToken()
        // {
        //     Token = GenerateRefreshToken(),
        //     Expiration = DateTime.UtcNow.AddSeconds(30) // Make this configurable
        // }
        //
        // public DateTime Expiration { get; set; }

        // public RefreshToken GenerateRefreshToken(User user)
        // {
        //     // Create the refresh token
        //     RefreshToken refreshToken = new RefreshToken()
        //     {
        //         Token = GenerateRefreshToken(),
        //         Expiration = DateTime.UtcNow.AddMinutes(35) // Make this configurable
        //     }
        //
        //     // Add it to the list of of refresh tokens for the user
        //     user.RefreshTokens.Add(refreshToken);
        //
        //     // Update the user along with the new refresh token
        //     UserRepository.Update(user);
        //
        //     return refreshToken;
        // }
        //
    
        public class RefreshTokenHandler
        {
            public async Task<string> GenerateRefreshToken()
            {
                var randomNumber = new byte[32];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);
                    return Convert.ToBase64String(randomNumber);
                }
            }
        }
    }

    
}