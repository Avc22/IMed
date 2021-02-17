using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iMed.ViewModels
{
    public class PatientsViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DisplayFormat(NullDisplayText = "unspecified")]
        public string Email { get; set; }

        [DisplayFormat(NullDisplayText = "unspecified")]
        public string Birthday { get; set; }

        [DisplayFormat(NullDisplayText = "unspecified")]
        public string Base64 { get; set; }

        [DisplayFormat(NullDisplayText = "unspecified")]
        public string Phone { get; set; }
    }
}
