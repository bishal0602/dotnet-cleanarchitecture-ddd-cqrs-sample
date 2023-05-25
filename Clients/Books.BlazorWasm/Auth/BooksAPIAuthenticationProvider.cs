using Books.BlazorWasm.Contracts;
using Books.BlazorWasm.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Books.BlazorWasm.Auth
{
    public class BooksAPIAuthenticationProvider : AuthenticationStateProvider
    {
        private readonly IUserService _userService;
        public User? CurrentUser { get; private set; } = new();

        public BooksAPIAuthenticationProvider(IUserService userService)
        {
            _userService = userService;
            AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        }

        public async Task LoginAsync(string email, string password)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            User? user = await _userService.SendAuthenticateRequestAsync(email, password);
            CurrentUser = user;
            if (user is not null)
            {
                claimsPrincipal = AuthUtilities.CreateClaimsPrincipalFromUser(user);
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            var user = await _userService.GetUserFromLocalStorageAsync();
            if (user is not null)
            {
                CurrentUser = user;
                claimsPrincipal = AuthUtilities.CreateClaimsPrincipalFromUser(user);
            }
            return new(claimsPrincipal);
        }

        public async Task LogoutAsync()
        {
            await _userService.RemoveUserFromLocalStorageAsync();
            CurrentUser = new();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }

        public async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> state)
        {
            var authenticationState = await state;
            if (authenticationState is not null)
            {
                CurrentUser = AuthUtilities.CreateUserFromClaimsPrincipal(authenticationState.User);
            }
        }

        public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;
    }
}
