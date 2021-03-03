using System.Threading.Tasks;

namespace bookshelf_app.Auth
{
    public interface ITokenManager
    {
        Task<bool> IsCurrentActiveTokenAsync();
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string token);
    }
}