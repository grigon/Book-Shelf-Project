using System;

namespace bookshelf_app.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public DateTimeOffset AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
    }
}