using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using workout_tracker_backend.Dtos;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;

namespace workout_tracker_tests.IntegrationTests;

public class UserControllerIntegrationTests
{
    static private int userId;
    private HttpClient _httpClient;

    public UserControllerIntegrationTests()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient();
    }

    [Fact]
    public async Task POST_register_user()
    {
        var response = await _httpClient.PostAsync("/api/user/register", new StringContent(
            JsonConvert.SerializeObject(new UserCreateDto()
            {
                Name = "Max",
                Email = "maxverstappen@champion.com",
                Password = "RedBull1"
            }),
            Encoding.UTF8,
            "application/json"));
        string content = await response.Content.ReadAsStringAsync();
        System.Console.WriteLine(content);
        userId = Int32.Parse(content);
        userId.Should().BeGreaterThanOrEqualTo(1);
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }


    [Fact]
    public async Task POST_authenticate_user()
    {
        var response = await _httpClient.PostAsync("/api/user/authenticate", new StringContent(
            JsonConvert.SerializeObject(new AuthenticateRequest()
            {
                Email = "sam@boers.family",
                Password = "test123"
            }),
            Encoding.UTF8,
            "application/json"));
        var authenticateResponse = await response.Content.ReadAsAsync<AuthenticateResponse>();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authenticateResponse.Token);
        System.Console.WriteLine($"authenticate: {userId}");
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
        [Fact]
        public async Task GET_get_user_by_id()
        {
            await POST_authenticate_user();
            var response = await _httpClient.GetAsync("/api/user/" + 1);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var user = JsonConvert.DeserializeObject<UserReadDto>(await response.Content.ReadAsStringAsync());
            System.Console.WriteLine($"getuserbyid: {userId}");
            user.Email.Should().Be("sam@boers.family");
        }

        [Fact]
        public async Task GET_gets_all_users()
        {
            await POST_authenticate_user();
            var response = await _httpClient.GetAsync("/api/user");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var users = JsonConvert.DeserializeObject<UserReadDto[]>(await response.Content.ReadAsStringAsync());
            users.Should().HaveCount(3);
        }
}