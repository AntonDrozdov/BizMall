using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BizMall.Models.AccountModels;
using BizMall.Models.CompanyModels;

namespace BizMall.DataAccess.Repositories.Abstract
{
    public interface IRepositoryCompany
    {
        IEnumerable<Company> GetAllCompanies();
        Company GetUserCompany(ApplicationUser currentUser);
        Company GetCompany(int companyId);
        Company CreateDefaultUserCompany(string userId);
    }
}
