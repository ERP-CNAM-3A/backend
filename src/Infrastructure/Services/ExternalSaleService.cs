using Domain.Entities.Ressources;
using Domain.Entities.Sales;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class ExternalSaleService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor for ExternalSaleService. Initializes the HttpClient for making API requests.
        /// </summary>
        /// <param name="httpClient">The HTTP client used for making requests to the external sales API.</param>
        public ExternalSaleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Retrieves external sales data from an external API.
        /// </summary>
        /// <returns>A list of <see cref="Sale"/> entities based on the fetched external sales data.</returns>
        /// <exception cref="Exception">Thrown if the API request fails or returns an error status code.</exception>
        public async Task<List<Sale>> GetExternalSalesAsync()
        {
            var jsonRequest = JsonSerializer.Serialize(new
            {
                key = "VA_SALES_BACKLOG",
                @params = (object)null,
                body = (object)null
            });
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://84.247.160.33:5001/action", content);

            // Check if the response is successful (status code 2xx).
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch external sales. Status code: {response.StatusCode}");
            }

            // Read the response content as a string.
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserialize the response content into a list of ExternalSaleDTO objects.
            var externalSales = JsonSerializer.Deserialize<List<ExternalSaleDTO>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case when mapping properties
            });

            // Map the external sales data to a list of Sale entities and return them.
            return externalSales.Select(s => new Sale
            {
                Id = GenerateGuidFromTypeAndIdType(s.Type, s.IdType), // Generate a unique GUID for each sale.
                Type = s.Type,
                Client = s.Client,
                Product = s.Product,
                Amount = s.Amount,
                Status = s.Status,
                Probability = s.Probability,
                Date = s.Date
            }).ToList();
        }

        /// <summary>
        /// Generates a unique GUID based on the sale type and ID type.
        /// </summary>
        /// <param name="type">The type of the sale (e.g., "Opportunity", "Quote").</param>
        /// <param name="idType">The ID type (e.g., a numeric value). Used to further differentiate the GUID.</param>
        /// <returns>A GUID representing the unique combination of type and ID type.</returns>
        private Guid GenerateGuidFromTypeAndIdType(string type, int idType)
        {
            // Combine 'Type' and 'IdType' into a single string to ensure uniqueness.
            string uniqueString = $"{type}-{idType}";

            // Use MD5 hashing to generate a hash of the unique string, and create a GUID from it.
            return Guid.Parse(BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(uniqueString)))
                .Replace("-", "")  // Remove hyphens from the MD5 hash.
                .Substring(0, 32)); // Take the first 32 characters to form the GUID.
        }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for External Sale.
    /// This class is used to deserialize the external sales data from the API response.
    /// </summary>
    public class ExternalSaleDTO
    {
        public string Type { get; set; }
        public int IdType { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Probability { get; set; }
    }
}
