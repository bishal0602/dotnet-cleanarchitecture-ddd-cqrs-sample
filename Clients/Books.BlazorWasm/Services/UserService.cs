using Books.BlazorWasm.Auth;
using Books.BlazorWasm.Contracts;
using Books.BlazorWasm.Exceptions;
using Books.BlazorWasm.External.Models.Authentication;
using Books.BlazorWasm.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Books.BlazorWasm.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorageService;

        public UserService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageService = localStorageService;
        }

        public async Task<User?> SendAuthenticateRequestAsync(string email, string password)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("Books.API");
            StringContent content = new(JsonConvert.SerializeObject(new
            {
                email,
                password
            }), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("/api/auth/login", content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(responseContent)
                                             ?? throw new Exception("No response recieved after authentication");
                await _localStorageService.SetItemAsync(AuthUtilities.LocalStorageTokenKey, authResponse.Token);

                ClaimsPrincipal claimsPrincipal = AuthUtilities.CreateClaimsPrincipalFromToken(authResponse.Token);

                User user = AuthUtilities.CreateUserFromClaimsPrincipal(claimsPrincipal);
                return user;
            }
            else
            {
                var problemDetailsContent = await httpResponseMessage.Content.ReadAsStringAsync();
                var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(problemDetailsContent);
                throw new ApiException(problemDetails);
            }
        }

        public async Task<User?> GetUserFromLocalStorageAsync()
        {
            string token = await _localStorageService.GetItemAsync<string>(AuthUtilities.LocalStorageTokenKey);
            if (token is null)
                return null;
            ClaimsPrincipal claimsPrincipal = AuthUtilities.CreateClaimsPrincipalFromToken(token);
            return AuthUtilities.CreateUserFromClaimsPrincipal(claimsPrincipal);
        }

        public async Task RemoveUserFromLocalStorageAsync()
        {
            await _localStorageService.RemoveItemAsync(AuthUtilities.LocalStorageTokenKey);
        }
    }
}
