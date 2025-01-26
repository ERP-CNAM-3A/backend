using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MiddlewareRegistrationService
    {
        private readonly HttpClient _httpClient;
        private const string MiddlewareRegisterUrl = "http://84.247.160.33:5001/register";

        public MiddlewareRegistrationService()
        {
            _httpClient = new HttpClient();
        }

        public async Task RegisterApiAsync()
        {
            try
            {
                var registrationData = new
                {
                    appKey = "PR",
                    url = "http://82.67.2.10:5000"
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(registrationData),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync(MiddlewareRegisterUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("API successfully registered with the middleware.");
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to register API. Status: {response.StatusCode}, Response: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while registering the API: {ex.Message}");
            }
        }
    }
}