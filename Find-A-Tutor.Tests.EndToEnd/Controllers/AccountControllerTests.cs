using Find_A_Tutor.Infrastructure.Commands.Users;
using System.Net;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using Find_A_Tutor.Infrastructure.DTO;
using Newtonsoft.Json;

namespace Find_A_Tutor.Tests.EndToEnd.Controllers
{
    public class AccountControllerTests : ControllerTestsBase
    {
        [Fact]
        public async Task given_valid_non_existing_user_he_should_be_register()
        {
            var command = new Register
            {
                Role = "student",
                FirstName = "test",
                LastName = "testowy",
                Email = "test@test.com",
                Password = "a12345678"
            };
            var payload = GetPayload(command);
            var response = await Client.PostAsync("account/register", payload);
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
        }

        [Fact]
        public async Task given_unique_email_user_should_be_created()
        {
            var command = new Register
            {
                Role = "student",
                FirstName = "test",
                LastName = "testowy",
                Email = "test@test.com",
                Password = "a12345678"
            };
            var payload = GetPayload(command);
            var response = await Client.PostAsync("account/register", payload);
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().Should().BeEquivalentTo($"account/{command.Email}");

            var user = await GetUserAsync(command.Email);
            user.Email.Should().BeEquivalentTo(command.Email);
        }

        private async Task<AccountDto> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"account/{email}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AccountDto>(responseString);
        }
    }
}
