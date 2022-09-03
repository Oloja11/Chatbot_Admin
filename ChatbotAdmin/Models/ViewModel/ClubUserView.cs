using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Models.ViewModel
{
    public class ClubUserView
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public string ClubCategory { get; set; }
        public int upcomingEventCount { get; set; }
    }
}
