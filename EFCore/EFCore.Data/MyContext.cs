using EFCore.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //申明联合主键
            modelBuilder.Entity<CityCompany>().HasKey(x => new
            {
                x.CityId, x.CompanyId
            });
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<CityCompany> CityCompanies { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=EFCoreDemo;User ID=sa;Password=123456;");
        //}
    }
}