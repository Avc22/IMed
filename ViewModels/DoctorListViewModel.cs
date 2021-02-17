using iMed.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iMed.ViewModels
{
    public class DoctorListViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [DisplayFormat(NullDisplayText ="unspecified")]
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public string Picture { get; set; }
    }
}
