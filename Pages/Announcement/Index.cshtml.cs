using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMed.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iMed.Pages.Announcement
{
    public class IndexModel : PageModel
    {
        private readonly iMedContext _context;

        public List<Models.Announcement> anc { get; set; } = new List<Models.Announcement>();

        public IndexModel(iMedContext context)
        {
            _context = context;
        }
        
        public void OnGet()
        {
            anc = _context.Announcement.OrderByDescending(p=>p.Number).AsNoTracking().ToList();
        }
    }
}