using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Models
{
    public class Accommodation
    {
        public long Id { get; set; }
        public string AccommodationType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string Link { get; set; }

    }
}
