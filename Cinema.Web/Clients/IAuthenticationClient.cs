using Cinema.Web.Models;
using Refit;
using System.Threading.Tasks;

namespace Cinema.Web.Clients
{
    public interface IAuthenticationClient
    {
        [Post("/api/Auth/SignIn")]
        Task<T> SignIn<T>([Body]SignInModel signInModel);

        [Post("/api/Auth/SignUp")]
        Task<T> SignUpAsync<T>([Body]SignUpModel model);

        [Post("/api/Auth/SignOut")]
        Task<T> SignOut<T>();

        [Put("/api/Auth/GiveAdminRole")]
        Task<T> GiveAdminRole<T>([Query]string userId);

        [Post("/api/Auth/CreateRole")]
        Task<T> CreateRole<T>([Query]string name);
    }
}
