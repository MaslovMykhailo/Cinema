using Cinema.Persisted.Entities;
using Cinema.Web.Models;
using System.Threading.Tasks;

namespace Cinema.Web.Providers.Interfaces
{
    public interface IAuthenticationProvider
    {
        Task<AuthUser> SignInAsync(SignInModel signInModel);
        Task<AuthUser> SignUpAsync(SignUpModel signUpModel);
        Task GiveAdminRole(string userId);
        Task SignOutAsync();
        Task CreateRole(string name);

    }
}
