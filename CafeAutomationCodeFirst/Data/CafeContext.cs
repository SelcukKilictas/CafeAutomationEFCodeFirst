using CafeAutomationCodeFirst.Models;
using CafeAutomationCodeFirst.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeAutomationCodeFirst.Data
{
    public class CafeContext : DbContext
    {
        public CafeContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CafeAutomation;Integrated Security=True;");
            }
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Table> Tables { get; set; }

        public override int SaveChanges()
        {
            var entiries = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && x.State == EntityState.Added);

            foreach (var item in entiries)
            {
                ((BaseEntity)item.Entity).CreatedDate = DateTime.Now;
            }

            entiries = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && x.State == EntityState.Modified);

            foreach (var item in entiries)
            {
                ((BaseEntity)item.Entity).UpdatedDate = DateTime.Now;
            }

            //entiries = ChangeTracker.Entries()
            //    .Where(x => x.Entity is BaseEntity && x.State == EntityState.Deleted);

            //foreach (var item in entiries)
            //{
            //    ((BaseEntity)item.Entity).DeletedDate = DateTime.Now;
            //    item.State = EntityState.Modified;
            //}

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(x => x.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .Property(x => x.Price)
                .HasPrecision(10, 2);
        }

    }
}
