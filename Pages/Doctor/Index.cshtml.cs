using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMed.Data;
using iMed.Models;
using iMed.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SQLitePCL;

namespace iMed.Pages.Doctor
{
    public class IndexModel : PageModel
    {
        private readonly iMedContext _context;

        public PaginatedList<DoctorListViewModel> doctors { get; set; }

        public IndexModel(iMedContext context)
        {
            _context = context;
        }
        public async Task OnGet(int? pageIndex)
        {
            var tmp = await _context.Doctor.Include(u => u.User).Include(p=>p.Picture).AsNoTracking().ToListAsync();
            var docList = new List<DoctorListViewModel>();

            foreach (var item in tmp)
            {
                var dct = new DoctorListViewModel
                {
                    Id = item.Id,
                    Email = item.User.Email,
                    FirstName = item.User.FirstName,
                    LastName = item.User.LastName,
                    Specialization=item.Specializaiton,
                    Phone = item.User.PhoneNumber,
                };
                if (item.Picture.ImageData != null)
                {
                    dct.Picture = Convert.ToBase64String(item.Picture.ImageData);
                }
                docList.Add(dct);
            }

            int pageSize = 4;
            doctors = PaginatedList<DoctorListViewModel>.CreateAsync(docList, pageIndex ?? 1, pageSize);
        }
    }
}