using ChatbotAdmin.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Models
{
    public class ClubUser
    {
        public int Id { get; set; }
        [Display(Name = "First-Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }
        [Display(Name = "Last-Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$")]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Sex")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sex is required")]
        public string Sex { get; set; }
        [Display(Name = "Date of Birth")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
        public int IsActive { get; set; }
        [Display(Name = "Default Password")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        public string HashPassword { get; set; }

        [Display(Name = "Confirm Password")]
        //[Compare("HashPassword", ErrorMessage = "Password not match")]
        public string HashPassword2 { get; set; }

        public List<string> Role { get; set; }


    }
}
