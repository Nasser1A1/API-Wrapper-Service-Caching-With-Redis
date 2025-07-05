using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Weather_API_Wrapper_Service.Entities;

namespace Weather_API_Wrapper_Service.Helpers
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration configuration;

        public ApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            this.configuration = configuration;
        }

        private void AddHeaders(HttpRequestMessage request, Dictionary<string, string>? headers)
        {
    
            if (headers == null) return;
            foreach (var header in headers)
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        private async Task<Dictionary<string, object>> HandleResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status {response.StatusCode}: {error}");
            }

            var content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(content);
            using var document = JsonDocument.Parse(content);
            var root = document.RootElement;

            var address = root.GetProperty("address").GetString();

            var days = root.GetProperty("days");
            var firstDay = days[0];

            var condition = firstDay.GetProperty("conditions").GetString();
            var temperature = firstDay.GetProperty("temp").GetDouble();

            // Return as dictionary/map
            return new Dictionary<string, object>
            {
                ["location"] = address!,
                ["condition"] = condition!,
                ["temperature"] = temperature
            };
        }

        public async Task<Dictionary<string, object>?> GetAsync<T>(string url, Dictionary<string, string>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            AddHeaders(request, headers);
        

            var response = await _httpClient.SendAsync(request);

            return await HandleResponse<T>(response);
        }


        public async Task<Dictionary<string, object>?> PostAsync<T>(string url, object data, Dictionary<string, string>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json")
            };

            AddHeaders(request, headers);

            var response = await _httpClient.SendAsync(request);
            return await HandleResponse<Dictionary<string, object>>(response);
        }

        public async Task<Dictionary<string, object>?> PutAsync<T>(string url, object data, Dictionary<string, string>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json")
            };
            AddHeaders(request, headers);

            var response = await _httpClient.SendAsync(request);
            return await HandleResponse<Dictionary<string, object>>(response);
        }

        public async Task<bool> DeleteAsync(string url, Dictionary<string, string>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            AddHeaders(request, headers);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
