using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using TelephoneBook.PhoneService.Models;

namespace TelephoneBook.PhoneService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasMany(c => c.ContactInfos)
                .WithOne(ci => ci.Contact)
                .HasForeignKey(ci => ci.ContactId);
        }
    }
}
