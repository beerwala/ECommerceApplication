using Domain.Common;
using Domain.Entity;
using Domain.Model;
using Domain.Model.CascadingData;
using Infrasturcture.Presistence.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasturcture.Presistence.Context
{
    public class ApplicationContext: DbContext
    {
        private readonly EncryptionService _encryptionService;
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext, EncryptionService encryptionService
                                    ):base(dbContext)
        {
            _encryptionService = encryptionService;
        }

        //user
        public DbSet<ApplicationUser> users { get; set; }
        public DbSet<Country> country { get; set; }
        public DbSet<State> states { get; set; }

        //product
        public DbSet<Product> products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

         

            modelBuilder.Entity<Country>().HasData(
                    new Country { Id = 1, CountryName = "India" },
                    new Country { Id = 2, CountryName = "USA" },
                    new Country { Id = 3, CountryName = "China" }
                                             );
            modelBuilder.Entity<State>().HasData(
                             new State { Id = 1, StateName = "Delhi", CountryId = 1 },
                                 new State { Id = 2, StateName = "Maharashtra", CountryId = 1 }
                             // Add more states for India as needed
                                             );


            modelBuilder.Entity<State>().HasData(
                             new State { Id = 3, StateName = "California", CountryId = 2 },
                                new State { Id = 4, StateName = "New York", CountryId = 2 }
                                // Add more states for USA as needed
                    );

            modelBuilder.Entity<State>().HasData(
                     new State { Id = 5, StateName = "Beijing", CountryId = 3 },
                     new State { Id = 6, StateName = "Shanghai", CountryId = 3 }
                      // Add more states for China as needed
                             );


        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddAuditInfo();
            HandleProductDelete();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddAuditInfo()
        {
            var entities = ChangeTracker.Entries<IEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            var utcNow = DateTime.UtcNow;
            //var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? appUser;
            //var ipAddress = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedOnUtc = utcNow;
                    entity.Entity.CreatedBy = "sushant";
                    entity.Entity.LastModifiedOnUtc = utcNow;
                    entity.Entity.LastModifiedBy = "--";
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.LastModifiedOnUtc = utcNow;
                    entity.Entity.LastModifiedBy = "Sushant";
                }

                //entity.Entity.IPAddress = ipAddress;
            }
        }
        private void HandleProductDelete()
        {
            var entities = ChangeTracker.Entries()
                         .Where(e => e.State == EntityState.Deleted);

            foreach (var entity in entities)
            {
                //var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? appUser;
                entity.State = EntityState.Modified;
                var item = entity.Entity as Product;
                item.IsDeleted = true;
                item.LastModifiedOnUtc = DateTime.UtcNow;
                item.LastModifiedBy = "sushan";
            }
        }

















        //saving
        public override int SaveChanges()
        {
          //  EncryptUserData();
            return base.SaveChanges();
        }
        //private void EncryptUserData()
        //{
        //    foreach (var entry in ChangeTracker.Entries<ApplicationUser>())
        //    {
        //        if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
        //        {
        //            var user = entry.Entity;
        //            user.EncryptedUsername = _encryptionService.Encrypt(user.Username);
        //            user.EncryptedPassword = _encryptionService.Encrypt(user.Password);
        //        }
        //    }
        //}
     



    }
}
