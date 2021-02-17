using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iMed.Models
{
    public class MedicalHistory
    {
        public int Id { get; set; }
        public string Allergies { get; set; }

        [Display(Name = "Current Medication")]
        public string CurrentMedication { get; set; }

        [Display(Name = "Medical Issues")]
        public string MedicalIssues { get; set; }

        [Display(Name = "Family Medical History")]
        public string FamilyMedicalHistory { get; set; }

        [Display(Name = "Past Surgeries")]
        public string PastSurgeries { get; set; }

        [Display(Name = "Blood Type")]
        public string BloodType { get; set; }

        [Display(Name = "Is Smoker")]
        public bool isSmoker { get; set; }
        //ADD PDF UPLOAD/DOWNLOAD
    }
}
