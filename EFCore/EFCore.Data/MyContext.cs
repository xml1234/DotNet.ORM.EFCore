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
                x.CityId,
                x.CompanyId
            });

            modelBuilder.Entity<CityCompany>().HasOne(x => x.City).WithMany(x => x.CityCompanies)
                .HasForeignKey(x => x.CityId);

            modelBuilder.Entity<CityCompany>().HasOne(x => x.Company).WithMany(x => x.CityCompanies)
                .HasForeignKey(x => x.CompanyId);

            modelBuilder.Entity<Mayor>().HasOne(x => x.City).WithOne(x => x.Mayor).HasForeignKey<Mayor>(x => x.CityId);

            modelBuilder.Entity<City>().HasOne(x => x.Province).WithMany(x => x.Cities)
                .HasForeignKey(x => x.ProvinceId);



            ////添加种子数据
            //modelBuilder.Entity<Province>().HasData(new Province
            //{
            //    Id = 1,
            //    Name = "湖北",
            //    Population = 50_000_000
            //});

        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<CityCompany> CityCompanies { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Mayor> Mayors { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=EFCoreDemo;User ID=sa;Password=123456;");
        //}
    }
}