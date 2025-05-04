using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingTime.Shared.Interfaces
{
    public interface ITimeEntryFetcher
    {
        Task<IEnumerable<TimeEntryDto>> FetchTimeEntriesAsync(DateTime from, DateTime to);
    }
}
