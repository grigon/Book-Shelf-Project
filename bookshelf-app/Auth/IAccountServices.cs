using Microsoft.IdentityModel.JsonWebTokens;

namespace bookshelf_app.Auth
{
    public interface IAccountServices
    {
        void SignUp(string username, string password);
        JsonWebToken RefreshAccessToken(string token);
        JsonWebToken CreateAccessToken();
        void RevokeRefreshToken(string token);
    }
}