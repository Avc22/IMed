using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace iMed.Pages.Patient
{
    
    public class EditModel : PageModel
    {
        private readonly iMedContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public PatientEditViewModel patientView { get; set; }

        [BindProperty]
        public Buffered FileUpload { get; set; }

        [BindProperty]
        public string pictureBase64 { get; set; }

        public EditModel(iMedContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public void OnGet(string id)
        {
            var userId = "";
            var patient = new Models.Patient();
            var user = new ApplicationUser();

            if (id == null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                patient = _context.Patient.Include(i => i.User).Include(c => c.MedicalHistory).Include(p => p.Picture).AsNoTracking().FirstOrDefault(i => i.Id == userId);
                user = _context.Users.Where(u => u.Id == userId).AsNoTracking().SingleOrDefault();
            }
            if (id != null)
            {
                patient = _context.Patient.Include(i => i.User).Include(c => c.MedicalHistory).Include(p => p.Picture).AsNoTracking().FirstOrDefault(i => i.Id == id);
                user = _context.Users.Where(u => u.Id == id).AsNoTracking().SingleOrDefault();
            }

            pictureBase64 = Convert.ToBase64String(patient.Picture.ImageData, 0, patient.Picture.ImageData.Length);

            patientView = new PatientEditViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Birthday = patient.Birthday,
                Sex = patient.Sex,
                Weigth = patient.Weight,
                Heigth = patient.Heigth,
                MedicalH = patient.MedicalHistory,
                Id = patient.Id,
                Picture = patient.Picture
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

            user.FirstName = patientView.FirstName;
            user.LastName = patientView.LastName;
            user.Email = patientView.Email;
            user.PhoneNumber = patientView.PhoneNumber;
            user.UserName = patientView.Email;

            await _userManager.UpdateAsync(user);

            Models.MedicalHistory medh = new Models.MedicalHistory
            {
                Allergies = patientView.MedicalH.Allergies,
                CurrentMedication = patientView.MedicalH.CurrentMedication,
                MedicalIssues = patientView.MedicalH.MedicalIssues,
                FamilyMedicalHistory = patientView.MedicalH.FamilyMedicalHistory,
                PastSurgeries = patientView.MedicalH.PastSurgeries,
                BloodType = patientView.MedicalH.BloodType,
                isSmoker = patientView.MedicalH.isSmoker
            };

            var patientId = await _context.Patient.Include(i => i.Picture).AsNoTracking().FirstOrDefaultAsync(id => id.Id == user.Id);
            var pic = new ProfilePicture();


            pic.ImageTitle = patientView.Email;
            pic.Id = patientId.Picture.Id;
            pic.ImageData = patientId.Picture.ImageData;

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

            Models.Patient pt = new Models.Patient
            {
                Id = user.Id,
                Birthday = patientView.Birthday,
                Sex = patientView.Sex,
                Weight = patientView.Weigth,
                Heigth = patientView.Heigth,
                MedicalHistory = medh,
                Picture = pic
            };
            pictureBase64 = Convert.ToBase64String(pt.Picture.ImageData, 0, pt.Picture.ImageData.Length);
            _context.Update(pt);
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