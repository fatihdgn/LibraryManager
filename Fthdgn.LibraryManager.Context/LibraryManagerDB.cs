using Fthdgn.LibraryManager.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Context
{
    public class LibraryManagerDB : DbContext
    {
        public LibraryManagerDB(string nameOrConnectionString) : base(nameOrConnectionString) { }
        public LibraryManagerDB() : this(nameof(LibraryManagerDB)) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Media> Media { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Add(new AttributeToColumnAnnotationConvention<DefaultValueAttribute, string>("SqlDefaultValue", (p, attributes) => attributes.FirstOrDefault().Value.ToString()));

            modelBuilder.Entity<Library>().HasOptional(x => x.Image).WithMany();
            modelBuilder.Entity<Library>().HasMany(x => x.Media).WithOptional(x => x.Library);
        }
    }
}
