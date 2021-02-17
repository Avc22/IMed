using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iMed.Data;
using iMed.Models;
using iMed.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace iMed.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly iMedContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public RegisterViewModel registerModel { get; set; }

        public RegisterModel(iMedContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostingEnvironment)
        {
            this.context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _hostingEnvironment = hostingEnvironment;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerModel.Email,
                    Email = registerModel.Email,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                };
                var result = await _userManager.CreateAsync(user, registerModel.Password);

                var medicalHistory = new MedicalHistory();

                var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", "Default-welcomer.png");
                var imageFileStream = System.IO.File.OpenRead(path);

                byte[] imageByteArray;

                using (var memoryStream = new MemoryStream())
                {
                    imageFileStream.CopyTo(memoryStream);
                    imageByteArray = memoryStream.ToArray();
                }
                var patient = new Models.Patient
                {
                    Id = user.Id,
                    User = user,
                    MedicalHistory = medicalHistory,
                    Picture = new ProfilePicture()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ImageData = imageByteArray
                    }
                };

                var pat = await _userManager.FindByIdAsync(patient.Id);
                await _userManager.AddToRoleAsync(pat, "Patient");

                context.Patient.Add(patient);
                await context.SaveChangesAsync();

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("/Patient/Edit");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return Page();
        }
    }
}