using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMed.Data;
using iMed.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iMed.Pages.Patient
{
    public class ViewModel : PageModel
    {
        private iMedContext _context;
        public PatientEditViewModel PatientV { get; set; }

        public string base64 { get; set; } 
        public ViewModel(iMedContext context)
        {
            _context = context;
        }
        public void OnGet(string id)
        {
            var temp = _context.Patient.Include(u=>u.User).Include(m=>m.MedicalHistory).Include(i=>i.Picture).Where(p => p.Id == id).AsNoTracking().FirstOrDefault();

            base64 = Convert.ToBase64String(temp.Picture.ImageData);

            PatientV = new PatientEditViewModel()
            {
                FirstName = temp.User.FirstName,
                LastName = temp.User.LastName,
                Email = temp.User.Email,
                PhoneNumber = temp.User.PhoneNumber,
                Birthday = temp.Birthday,
                Sex = temp.Sex,
                Weigth = temp.Weight,
                Heigth = temp.Heigth,
                MedicalH=temp.MedicalHistory
            };
        }
    }
}