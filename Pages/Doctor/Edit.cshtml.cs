using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using iMed.Data;
using iMed.Models;
using iMed.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iMed.Pages.Doctor
{
    public class EditModel : PageModel
    {
        private readonly iMedContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public DoctorEditViewModel Doctor { get; set; } = new DoctorEditViewModel();

        [BindProperty]
        public string pictureBase64 { get; set; }

        [BindProperty]
        public Buffered FileUpload { get; set; }

        public EditModel(iMedContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public void OnGet(string id)
        {
            var userId = "";
            var doctors = new Models.Doctor();
            var user = new ApplicationUser();

            if (id == null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                doctors = _context.Doctor.Include(i => i.User).Include(p => p.Picture).AsNoTracking().FirstOrDefault(i => i.Id ==userId);
                user = _context.Users.Where(u => u.Id == userId).AsNoTracking().SingleOrDefault();
            }
            if (id != null)
            {
                doctors = _context.Doctor.Include(i => i.User).Include(p => p.Picture).Include(u=>u.User).AsNoTracking().FirstOrDefault(i => i.Id == id);
                user = _context.Users.Where(u => u.Id == id).AsNoTracking().SingleOrDefault();
            }

            pictureBase64 = Convert.ToBase64String(doctors.Picture.ImageData, 0, doctors.Picture.ImageData.Length);

            Doctor = new DoctorEditViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Sex = doctors.Sex,
                Specialization = doctors.Specializaiton,
                Id = doctors.Id,
                Picture = doctors.Picture
            };
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = new ApplicationUser();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (id == null)
            {
                user = await _userManager.GetUserAsync(User);
            }
            if (id != null)
            {
                user = await _userManager.FindByIdAsync(id);
            }

            user.FirstName = Doctor.FirstName;
            user.LastName = Doctor.LastName;
            user.Email = Doctor.Email;
            user.PhoneNumber = Doctor.PhoneNumber;
            user.UserName = Doctor.Email;

            await _userManager.UpdateAsync(user);
            var doctorId = new Models.Doctor();
            if (id == null)
            {
                doctorId = await _context.Doctor.Include(i => i.Picture).AsNoTracking().FirstOrDefaultAsync(i => i.Id == user.Id);
            }

            if (id != null)
            {
                doctorId = await _context.Doctor.Include(i => i.Picture).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

            }
                var pic = new ProfilePicture();

            pic.ImageTitle = Doctor.Email;
            pic.Id = doctorId.Picture.Id;
            pic.ImageData = doctorId.Picture.ImageData;

            if (FileUpload.FormFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    FileUpload.FormFile.CopyTo(memoryStream);

                    if (memoryStream.Length < 2097152)
                    {
                        pic.ImageData = memoryStream.ToArray();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                    }
                }
            }

            Models.Doctor dct = new Models.Doctor
            {
                Id = user.Id,
                Sex = Doctor.Sex,
                Specializaiton = Doctor.Specialization,
                Picture = pic
            };
            pictureBase64 = Convert.ToBase64String(dct.Picture.ImageData, 0, dct.Picture.ImageData.Length);
            _context.Update(dct);
            await _context.SaveChangesAsync();

            return Page();
        }
        public class Buffered
        {
            [Display(Name = "File")]
            public IFormFile FormFile { get; set; }
        }
    }
}
