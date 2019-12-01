using Cinema.Persisted.Entities;
using Cinema.Web.Clients;
using Cinema.Web.Models;
using Cinema.Web.Providers.Interfaces;
using System.Threading.Tasks;

namespace Cinema.Web.Providers.Authentication
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly IAuthenticationClient _authenticationClient;

        public AuthenticationProvider(
            IAuthenticationClient authenticationClient)
        {
            _authenticationClient = authenticationClient;
        }

        public async Task<AuthUser> SignInAsync(SignInModel signInModel)
        {
            var authUser = await _authenticationClient.SignIn<AuthUser>(signInModel);

            return authUser;
        }

        public async Task<AuthUser> SignUpAsync(SignUpModel signUpModel)
        {
            var signUpResult = await _authenticationClient.SignUpAsync<AuthUser>(signUpModel);
            var authUser = await _authenticationClient.SignIn<AuthUser>(new SignInModel() { Username = signUpModel.Username, Password = signUpModel.Password });

            return authUser;
        }

        public async Task GiveAdminRole(string userId)
        {
            await _authenticationClient.GiveAdminRole<AuthUser>(userId);
        }

        public async Task SignOutAsync()
        {
            await _authenticationClient.SignOut<AuthUser>();
        }

        public async Task CreateRole(string name)
        {
            await _authenticationClient.CreateRole<AuthUser>(name);
        }
    }
}
