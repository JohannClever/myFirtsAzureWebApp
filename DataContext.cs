using Microsoft.EntityFrameworkCore;
using myFirtsAzureWebApp.Models;

namespace myFirtsAzureWebApp
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opciones) : base(opciones)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
