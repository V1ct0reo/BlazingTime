using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using BlazingTime.Shared.Interfaces;
using BlazingTime.Shared;
using Microsoft.Extensions.Options;
using BlazingTime.Shared.Options;

namespace BlazingTime.Services.DataFetching
{
    public class TogglTrackTimeEntryFetcher : ITimeEntryFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TogglTrackTimeEntryFetcher> _logger;
        private readonly TogglOptions _togglOptions;

        public TogglTrackTimeEntryFetcher(HttpClient httpClient, IOptions<TogglOptions> options, ILogger<TogglTrackTimeEntryFetcher> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _togglOptions = options.Value;

            var auth = Convert.ToBase64String(
                Encoding.ASCII.GetBytes($"{_togglOptions.ApiToken}:api_token"));

            _httpClient.BaseAddress = new Uri(_togglOptions.BaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", auth);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        }

        public async Task<IEnumerable<TimeEntryDto>> FetchTimeEntriesAsync(DateTime startDate, DateTime endDate)
        {

            _logger.LogInformation($"Fetching timeentries between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}");
            _logger.LogDebug($"Workspace: {_togglOptions.WorkspaceID}, token: {_togglOptions.ApiToken}");


            var url = $"{_togglOptions.BaseUrl}/reports/api/v3/workspace/{_togglOptions.WorkspaceID}/search/time_entries";
           
            

            var payload = new
            {
                start_date = startDate.ToString("yyyy-MM-dd"),
                end_date = endDate.ToString("yyyy-MM-dd"),
                page_size = 200
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
                IgnoreReadOnlyProperties = true,
                AllowTrailingCommas = true
            };
            var data = JsonSerializer.Deserialize<List<TimeEntryDto>>(json,jsonOptions);

            if (data is null || data.Count == 0)
                return  new List<TimeEntryDto>() { new() { Id = -1, Description = "Error" } };
            return data ?? Enumerable.Empty<TimeEntryDto>();
        }
    }
}
