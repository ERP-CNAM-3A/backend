using System.Text;
using System.Text.Json;

namespace Infrastructure.Services
{
    /// <summary>
    /// Service responsible for registering the API with a middleware.
    /// </summary>
    public class MiddlewareRegistrationService
    {
        // HTTP client used to make HTTP requests.
        private readonly HttpClient _httpClient;

        // URL of the middleware service where the registration data is sent.
        private const string MiddlewareRegisterUrl = "http://84.247.160.33:5001/register";

        /// <summary>
        /// Constructor initializes the HttpClient instance.
        /// </summary>
        public MiddlewareRegistrationService()
        {
            // Initialize HttpClient instance to be used for API requests.
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Registers the current API with the middleware by sending the registration data.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task RegisterApiAsync()
        {
            try
            {
                // Data that will be sent for the API registration (contains app key and API URL).
                var registrationData = new
                {
                    appKey = "PR",  // Unique app key identifying the API.
                    url = "http://82.67.2.10:5000"  // The URL of the API being registered.
                };

                // Serialize the registration data to JSON format.
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(registrationData),  // JSON serialized object.
                    Encoding.UTF8,  // Set encoding to UTF-8.
                    "application/json"  // Set content type to application/json.
                );

                // Send a POST request to the middleware register URL with the JSON content.
                var response = await _httpClient.PostAsync(MiddlewareRegisterUrl, jsonContent);

                // Check if the response from the middleware is successful (status code 2xx).
                if (response.IsSuccessStatusCode)
                {
                    // Log success message if the registration is successful.
                    Console.WriteLine("API successfully registered with the middleware.");
                }
                else
                {
                    // Log failure message if the registration fails, including response details.
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to register API. Status: {response.StatusCode}, Response: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                // Log any exception that occurs during the registration process.
                Console.WriteLine($"An error occurred while registering the API: {ex.Message}");
            }
        }
    }
}
