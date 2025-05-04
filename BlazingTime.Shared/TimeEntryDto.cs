using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingTime.Shared
{
    public class TimeEntryDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public double Hours { get; set; }
    }
}
