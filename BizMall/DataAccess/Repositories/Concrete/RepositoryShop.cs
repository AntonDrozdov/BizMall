using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

using BizMall.DataAccess.DBContexts;
using BizMall.DataAccess.Repositories.Abstract;
using BizMall.Models.AccountModels;
using BizMall.Models.CompanyModels;

namespace BizMall.DataAccess.Repositories.Concrete
{
    //Сам context наследуем из RepositoryBase
    public class RepositoryCompany : RepositoryBase, IRepository, IRepositoryCompany
    {
        public RepositoryCompany(ApplicationDbContext ctx) : base(ctx)
        {
        }

        public Company CreateDefaultUserCompany(string userId)
        {
            var company = new Company { Title = "Моя компания", ApplicationUserId = userId};
            _ctx.Companies.Add(company);
            _ctx.SaveChanges();
            return company;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            throw new NotImplementedException();
        }

        public Company GetCompany(int shopId)
        {
            throw new NotImplementedException();
        }

        public Company GetUserCompany(ApplicationUser User)
        {
            var company = _ctx.Companies.Where(s => s.ApplicationUserId == User.Id).Include(s => s.Goods).FirstOrDefault();
            return company;
        }
    }
}
