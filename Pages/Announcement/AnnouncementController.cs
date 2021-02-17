using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMed.Data;
using Microsoft.AspNetCore.Mvc;

namespace iMed.Pages.Announcement
{
    public class AnnouncementController : Controller
    {
        private readonly iMedContext _context;

        public AnnouncementController(iMedContext context)
        {
            _context = context;
        }
        public IActionResult Delete(string id)
        {
            var tmp = _context.Announcement.FirstOrDefault(p => p.Id == id);

            _context.Announcement.Remove(tmp);
            _context.SaveChanges();
            return RedirectToPage("/Announcement/Index");
        }
    }
}
