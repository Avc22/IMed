using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iMed.Models
{
    public class Doctor
    {
        public string Id { get; set; }
        public string Specializaiton { get; set; }
        public string Sex { get; set; }
        public ApplicationUser User { get; set; }
        public ProfilePicture Picture { get; set; }
    }
}