using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi1.Models;

namespace WebApi1.data
{
    public class HRcontext:IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration config;

        public HRcontext(IConfiguration _config)
        {
            config = _config;
        }

        public DbSet<Employee> employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("HRconnecton"));
            base.OnConfiguring(optionsBuilder);
        }



    }
}
