using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Models.ViewModel
{
    public class RetrievePasswordVM
    {
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$")]
        public string Email { get; set; }

    }
}
