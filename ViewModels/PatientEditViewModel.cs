using iMed.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iMed.ViewModels
{
    public class PatientEditViewModel
    {
        public string Id { get; set; }

        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [DisplayFormat(NullDisplayText = "unspecified")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Birthday (dd/mm/yyyy)")]
        [DisplayFormat(NullDisplayText = "unspecified")]
        public string Birthday { get; set; }

        [DisplayFormat(NullDisplayText = "unspecified")]
        public string Sex { get; set; }

        [Display(Name = "Weigth (kg)")]
        public int Weigth { get; set; }

        [Display(Name = "Heigth (cm)")]
        public int Heigth { get; set; }
        public MedicalHistory MedicalH { get; set; }

        public ProfilePicture Picture { get; set; }
    }
}
