namespace bookshelf_app.Auth
{
    public class RefreshToken
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }

    }
}