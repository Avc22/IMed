using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMed.Data;
using iMed.Models;
using iMed.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iMed.Pages.Patient
{
    public class IndexModel : PageModel
    {
        private readonly iMedContext _context;
       
        public IndexModel(iMedContext context)
        {
            _context = context;
        }

        public PaginatedList<PatientsViewModel> patients { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            var tmp = await _context.Patient.Include(u => u.User).Include(p=>p.Picture).AsNoTracking().ToListAsync();
            var patList = new List<PatientsViewModel>();

            foreach (var item in tmp)
            {
                var temp = new PatientsViewModel 
                { 
                    Id=item.Id,
                    FirstName = item.User.FirstName , 
                    LastName = item.User.LastName, 
                    Phone = item.User.PhoneNumber,
                    Email = item.User.Email, 
                };

                if (item.Picture.ImageData != null)
                {
                    temp.Base64 = Convert.ToBase64String(item.Picture.ImageData);
                }
                patList.Add(temp);
            }
            int pageSize = 4;
            patients = PaginatedList<PatientsViewModel>.CreateAsync(patList, pageIndex ?? 1, pageSize);
        }
    }
}