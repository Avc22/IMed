using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMed.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using SQLitePCL;

namespace iMed.Pages.Announcement
{
    public class CreateModel : PageModel
    {
        private readonly iMedContext _context;

        [BindProperty]
        public Models.Announcement an { get; set; } = new Models.Announcement();

        public CreateModel(iMedContext context)
        {
            _context = context;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            var tmp = _context.Announcement.ToList();
            int max = 0;

            foreach(var item in tmp)
            {
                if (item.Number >= max)
                {
                    max = item.Number;
                }
            }

            an.Number = max + 1;
            an.Hour = DateTime.Now.ToString("HH:mm");
            an.Date = DateTime.Now.Date.ToString("dd/MM/yyy");
            an.Id = Guid.NewGuid().ToString();

            await _context.AddAsync(an);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}