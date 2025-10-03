using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WorkItems.Shared;

namespace WorkItems.Client.Api
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ApiClient(string baseUrl, string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        private void AddHeaders(HttpRequestMessage request)
        {
            request.Headers.Add("X-API-Key", _apiKey);
            request.Headers.Add("X-Correlation-Id", Guid.NewGuid().ToString());
        }

        public async Task<WorkItemDto[]> GetWorkItemsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "workitems");
            AddHeaders(request);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WorkItemDto[]>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<WorkItemDto> CreateWorkItemAsync(WorkItemDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "workitems");
            AddHeaders(request);
            request.Content = new StringContent(JsonSerializer.Serialize(dto), System.Text.Encoding.UTF8, "application/json");

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WorkItemDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateWorkItemStatusAsync(Guid id, WorkItemStatus status)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"workitems/{id}/status");
            AddHeaders(request);
            request.Content = new StringContent(JsonSerializer.Serialize(status), System.Text.Encoding.UTF8, "application/json");

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}