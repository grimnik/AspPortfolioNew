using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Portfolio.Domain;

namespace Portfolio.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<TagProject> TagProjects { get; set; }
   
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            builder.Entity<TagProject>().HasKey(tp => new { tp.TagId, tp.ProjectId });
            builder.Entity<Tag>().HasData(
            new Tag() { Id = 1, Naam = "C#" },
            new Tag() { Id = 2, Naam = "ASP.NET" },
            new Tag() { Id = 3, Naam = "HTML" },
            new Tag() { Id = 4, Naam = "CSS" },
            new Tag() { Id = 5, Naam = "JavaScript" },
            new Tag() { Id = 6, Naam = "Entity Framework"}
            );
            builder.Entity<Status>().HasData(
                new Status() { Id = 1, Naam = "Toekomsting Project"},
                new Status() { Id = 2, Naam = "Huidig Project"},
                new Status() { Id = 3, Naam = "Afgewerkt Project" }
                );
            base.OnModelCreating(builder);
        }
    }
}
