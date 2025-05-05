

using BlazingTime.Shared;
using BlazingTime.Shared.Interfaces;

namespace BlazingTime.Services.DataFetching
{
    public class MockTimeEntryFetcher : ITimeEntryFetcher
    {
        public Task<IEnumerable<TimeEntryDto>> FetchTimeEntriesAsync(DateTime from, DateTime to)
        {
            var data = new List<TimeEntryDto>()
            {
                new() { Id = 1, Description = "Mock Task A", Start = DateTime.Now.AddHours(-2), Stop = DateTime.Now },
                new() { Id = 2, Description = "Mock Task B", Start = DateTime.Now.AddHours(-1), Stop = DateTime.Now }
            };
            return Task.FromResult<IEnumerable<TimeEntryDto>>(data);
        }
    }
}
