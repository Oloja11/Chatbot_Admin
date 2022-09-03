using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Models.ViewModel
{
    public class ChangePasswordVM
    {
        [Display(Name = "Current Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Current Password is required")]
        public string CurrentPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "Password not match")]
        public string NewPasswordConfirm { get; set; }

    }
}
