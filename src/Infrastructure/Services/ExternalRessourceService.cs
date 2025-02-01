
using Domain.Entities.Ressources;
using Domain.Repositories;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Infrastructure.Services
{
    public class ExternalRessourceService : IExternalRessourceService
    {
        private readonly HttpClient _httpClient;

        public ExternalRessourceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Ressource>> GetAllRessourcesAsync()
        {
            var jsonRequest = JsonSerializer.Serialize(new
            {
                key = "STO_GET_RESSOURCES",
                @params = (object)null,
                body = (object)null
            });
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://84.247.160.33:5001/action", content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var externalRessources = JsonSerializer.Deserialize<List<ExternalRessourceDTO>>(jsonResponse);

            var ressources = new List<Ressource>();
            foreach (var externalRessource in externalRessources)
            {
                ressources.Add(new Ressource(
                    externalRessource.Id,
                    externalRessource.Nom,
                    externalRessource.Type,
                    externalRessource.GroupId
                ));
            }

            return ressources;
        }

        public async Task<List<Ressource>> GetAvailableRessourcesBetweenAsync(DateTime startDate, DateTime endDate)
        {
            var jsonRequest = JsonSerializer.Serialize(new
            {
                key = "STO_GET_RESSOURCES_AVAILABLE",
                @params = new
                {
                    start_date = startDate.ToString("yyyy-MM-dd"),
                    end_date = endDate.ToString("yyyy-MM-dd")
                },
                body = (object)null
            });
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://84.247.160.33:5001/action", content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var externalRessources = JsonSerializer.Deserialize<List<ExternalRessourceWithAvailabilityDTO>>(jsonResponse);

            var ressources = new List<Ressource>();
            foreach (var externalRessource in externalRessources)
            {
                var availabilityPeriods = new List<AvailabilityPeriod>();
                if (externalRessource.AvailabilityPeriods != null)
                {
                    foreach (var period in externalRessource.AvailabilityPeriods)
                    {
                        availabilityPeriods.Add(new AvailabilityPeriod(period.StartDate, period.EndDate));
                    }
                }

                ressources.Add(new Ressource(
                    externalRessource.ResourceId,
                    externalRessource.ResourceName,
                    externalRessource.ResourceType,
                    externalRessource.ResourceGroupId,
                    availabilityPeriods
                ));
            }

            return ressources;
        }

        public async Task<bool> ReserveRessourceAsync(int ressourceId, DateTime from, DateTime to)
        {
            var jsonRequest = JsonSerializer.Serialize(new
            {
                key = "STO_RESERVER_RESSOURCE",
                @params = (object)null,
                body = new
                {
                    ressource_id = ressourceId,
                    start_date = from.ToString("yyyy-MM-dd"),
                    end_date = to.ToString("yyyy-MM-dd")
                }
            });
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://84.247.160.33:5001/action", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CancelReservationAsync(int ressourceId, DateTime from, DateTime to)
        {
            var jsonRequest = JsonSerializer.Serialize(new
            {
                key = "STO_SUPPRIMER_RESERVATION",
                @params = (object)null,
                body = new
                {
                    ressource_id = ressourceId,
                    start_date = from.ToString("yyyy-MM-dd"),
                    end_date = to.ToString("yyyy-MM-dd")
                }
            });
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://84.247.160.33:5001/action", content);
            return response.IsSuccessStatusCode;
        }

        private class ExternalRessourceDTO
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("nom")]
            public string Nom { get; set; }
            [JsonPropertyName("group_id")]
            public int GroupId { get; set; }
            [JsonPropertyName("type")]
            public string Type { get; set; }
            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }
        }

        private class ExternalRessourceWithAvailabilityDTO
        {
            [JsonPropertyName("resource_id")]
            public int ResourceId { get; set; }
            [JsonPropertyName("resource_name")]
            public string ResourceName { get; set; }
            [JsonPropertyName("resource_type")]
            public string ResourceType { get; set; }
            [JsonPropertyName("resource_groupId")]
            public int ResourceGroupId { get; set; }
            [JsonPropertyName("availability_periods")]
            public List<AvailabilityPeriodDTO> AvailabilityPeriods { get; set; }
        }

        private class AvailabilityPeriodDTO
        {
            [JsonPropertyName("start_date")]
            public DateTime StartDate { get; set; }
            [JsonPropertyName("end_date")]
            public DateTime EndDate { get; set; }
        }
    }
}


