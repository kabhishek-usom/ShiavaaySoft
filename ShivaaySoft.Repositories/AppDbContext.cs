using Microsoft.EntityFrameworkCore;
using ShivaaySoft.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaaySoft.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EnquiryTypeEntity> EnquiryTypes { get; set; }
        public DbSet<EnquiryEntity> Enquiries { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=ShivaaySoft;Trusted_Connection=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
