using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Models
{
    public class ApplicationConfig
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public bool SmtpStartEnable { get; set; }


    }
}
