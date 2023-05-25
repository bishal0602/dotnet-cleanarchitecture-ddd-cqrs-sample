using Books.BlazorWasm.Auth;
using Books.BlazorWasm.Contracts;
using Books.BlazorWasm.External.Models.Authentication;
using Books.BlazorWasm.Models;
using Books.BlazorWasm.Services;
using Microsoft.Extensions.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace Books.BlazorWasm.Tests;
public class ClaimsTransformationTests
{

    [Fact]
    public async Task UserClaims_GetsParsed()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5263")
        };
        StringContent content = new StringContent(JsonConvert.SerializeObject(new
        {
            email = "einstein.albert@example.com",
            password = "E=mc2Genius"
        }), Encoding.UTF8, "application/json");
        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("/api/auth/login", content);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            AuthResponse? authResponse = JsonConvert.DeserializeObject<AuthResponse>(responseContent);
            if (authResponse is null)
                throw new Exception("No response recieved after authentication");
            ClaimsPrincipal claimsPrincipal = AuthUtilities.CreateClaimsPrincipalFromToken(authResponse.Token);
            User user = AuthUtilities.CreateUserFromClaimsPrincipal(claimsPrincipal);

            // Assert
            Assert.Equal(user.Email, "einstein.albert@example.com");
            Assert.Equal(user.FirstName, "Albert");
            Assert.Equal(user.LastName, "Einstein");
            Assert.Equal(user.UserName, "aeinstein");
            Assert.Equal(user.UserId.ToString(), "e06f0e3d-6a26-4495-bdbf-763e8dc49400");
        }
    }
}