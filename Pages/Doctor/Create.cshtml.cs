using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iMed.Data;
using iMed.Models;
using iMed.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;

namespace iMed.Pages.Doctor
{
    [Authorize(Roles ="Manager")]
    public class CreateModel : PageModel
    {
        private readonly iMedContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly RoleManager<IdentityRole> _roleManager;

        [BindProperty]
        public CreateDoctorViewModel Doctor { get; set; }

        public CreateModel(iMedContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            _roleManager = roleManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = Doctor.Email,
                Email = Doctor.Email,
                FirstName = Doctor.FirstName,
                LastName = Doctor.LastName,
                PhoneNumber = Doctor.Phone
            };

            await _userManager.CreateAsync(user, Doctor.Password);

            var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", "Default-welcomer.png");
            var imageFileStream = System.IO.File.OpenRead(path);

            byte[] imageByteArray;

            using (var memoryStream = new MemoryStream())
            {
                imageFileStream.CopyTo(memoryStream);
                imageByteArray = memoryStream.ToArray();
            }

            Models.Doctor dct = new Models.Doctor
            {
                Id = user.Id,
                Sex = Doctor.Sex,
                Specializaiton = Doctor.Specialization,
                User = user,
                Picture = new ProfilePicture()
                {
                    Id = Guid.NewGuid().ToString(),
                    ImageData = imageByteArray
                }
            };

            var doc = await _userManager.FindByIdAsync(dct.Id);

            await _userManager.AddToRoleAsync(doc, "Doctor");

            _context.Doctor.Add(dct);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Doctor/Index");
        }
    }
}