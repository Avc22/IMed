using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMed.Data;
using iMed.Models;
using iMed.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace iMed.Pages.Doctor
{
    public class ViewModel : PageModel
    {
        private readonly iMedContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DoctorEditViewModel Doctor { get; set; }
        public string base64 { get; set; }

        public ViewModel(iMedContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public void OnGet(string id)
        {
            var doctor = _context.Doctor.Include(u => u.User).Include(p=>p.Picture).Where(temp=>temp.Id == id).AsNoTracking().FirstOrDefault();

            base64 = Convert.ToBase64String(doctor.Picture.ImageData);

            Doctor = new DoctorEditViewModel
            {
                Id = doctor.Id,
                FirstName = doctor.User.FirstName,
                LastName = doctor.User.LastName,
                Email = doctor.User.Email,
                PhoneNumber = doctor.User.PhoneNumber,
                Sex = doctor.Sex,
                Specialization = doctor.Specializaiton
            };
        }
    }
}