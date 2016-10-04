using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

using BizMall.Models.AccountModels;
using BizMall.Models.CommonModels;
using BizMall.Models.CompanyModels;

namespace BizMall.DataAccess.DBContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //товары компании
            builder.Entity<RelCompanyGood>().HasKey(r => new { r.CompanyId, r.GoodId });
            builder.Entity<RelCompanyGood>()
                .HasOne(r => r.Company)
                .WithMany(l => l.Goods)
                .HasForeignKey(r => r.CompanyId);
            builder.Entity<RelCompanyGood>()
                .HasOne(r => r.Good)
                .WithMany(l => l.Companies)
                .HasForeignKey(r => r.GoodId);
        }
    }
}
