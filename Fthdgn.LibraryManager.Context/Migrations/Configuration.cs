namespace Fthdgn.LibraryManager.Context.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Fthdgn.LibraryManager.Context.LibraryManagerDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new DefaultValueSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(Fthdgn.LibraryManager.Context.LibraryManagerDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
