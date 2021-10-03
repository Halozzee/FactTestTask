using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Database
{
    public static class DBPathHelper 
    {
        public static string GetDBPath()
        {
            string assemPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string root = System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(assemPath).FullName).FullName).FullName;
            return root + @"\App_data\testdb.mdf";
        }
    }

    public class EFDBContext : DbContext
    {
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<DrinkAvailability> DrinkAvailabilities { get; set; }

        public EFDBContext() { }

        public EFDBContext(DbContextOptions<EFDBContext> ops) : base(ops) 
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@$"Server=(localdb)\MSSQLLocalDB; AttachDbFilename={DBPathHelper.GetDBPath()}; Trusted_Connection=True");
        }
    }

    public class EFDBContextFactory : IDesignTimeDbContextFactory<EFDBContext>
    {
        public EFDBContext CreateDbContext(string[] args)
        {
            var ops = new DbContextOptionsBuilder<EFDBContext>();
            ops.UseSqlServer(@$"Server=(localdb)\\MSSQLLocalDB; AttachDbFilename={DBPathHelper.GetDBPath()}; Trusted_Connection=True");

            return new EFDBContext(ops.Options);
        }
    }
}
