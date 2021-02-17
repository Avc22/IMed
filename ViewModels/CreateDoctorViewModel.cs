using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iMed.ViewModels
{
    public class CreateDoctorViewModel
    {
        public string Id { get; set; }

        [Display(Name ="First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string Sex { get; set; }

        public string Phone { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Passwords do not match")]
        public int ConfirmPassword { get; set; }
    }
}
