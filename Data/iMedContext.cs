using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iMed.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace iMed.Data
{
    public class iMedContext : IdentityDbContext<ApplicationUser>
    {
        public iMedContext (DbContextOptions<iMedContext> options)
            : base(options)
        {
        }

        public DbSet<iMed.Models.Doctor> Doctor { get; set; }
        public DbSet<iMed.Models.Patient> Patient { get; set; }
        public DbSet<iMed.Models.Announcement> Announcement { get; set; }
    }
}
