using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iMed.Models
{
    public class Patient
    {
        public string Id { get; set; }
        public string Birthday { get; set; }
        public string Sex { get; set; }
        public int Heigth { get; set; }
        public int Weight { get; set; }
        public ApplicationUser User { get; set; }
        public MedicalHistory MedicalHistory { get; set; }

        public ProfilePicture Picture { get; set; }
        // ADD IMAGE
    }
}
