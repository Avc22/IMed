using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMed.Data;
using iMed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace iMed.Pages.Account
{
    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly iMedContext _context;

        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, iMedContext context)
        {
            this.signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }
        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var patient = _context.Patient.Include(r=> r.Picture).Where(p=>p.Id==id).FirstOrDefault();
            var profilePic = patient.Picture;
            _context.Remove(profilePic);
            _context.Remove(patient);

            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);

            return RedirectToPage("/Patient/Index");
        }

        public async Task<IActionResult> DeleteUserD(string id)
        {
            var doctor = _context.Doctor.Include(r => r.Picture).Where(d => d.Id == id).FirstOrDefault();
            var profilePic = doctor.Picture;
            _context.Remove(profilePic);
            _context.Remove(doctor);

            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);

            return RedirectToPage("/Doctor/Index");
        }
    }
}