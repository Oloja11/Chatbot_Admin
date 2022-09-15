using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Models
{
    public class Course
    {
        public long Id { get; set; }        
        public string Department { get; set; }
        public string Title { get; set; }
        public string Level { get; set; }
        public string Duration { get; set; }

    }
}
