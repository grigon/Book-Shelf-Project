using System;
using bookshelf.Model.Users;
using Microsoft.IdentityModel.JsonWebTokens;

namespace bookshelf_app.Auth
{
    public class AccountServices : IAccountServices
    {
        public void SignUp(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public JsonWebToken RefreshAccessToken(string token)
        {
            return null;
        }

        public JsonWebToken CreateAccessToken()
        {
            throw new NotImplementedException();
        }

        public void RevokeRefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        public JsonWebToken RefreshAccessToken(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}