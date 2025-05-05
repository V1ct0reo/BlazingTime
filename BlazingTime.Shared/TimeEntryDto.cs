using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazingTime.Shared
{
    public class TimeEntryDto
    {
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        public string Username { get; set; }
        [JsonPropertyName("project_id")]
        public long ProjectId { get; set; }
        [JsonPropertyName("task_id")]
        public long? TaskId { get; set; }
        public string Description { get; set; }
        [JsonPropertyName("tag_ids")]
        public List<long> TagIds { get; set; }
        [JsonPropertyName("row_number")]
        public int RowNumber { get; set; }
        public long Id { get; set; }
        public int Seconds { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public DateTime At { get; set; }
    }
}
