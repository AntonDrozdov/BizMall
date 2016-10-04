using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BizMall.DataAccess.Repositories.Concrete;
using BizMall.Models.CompanyModels;

namespace BizMall.DataAccess.Repositories.Abstract
{
    public interface IRepositoryCategory
    {
        IQueryable<Category> Categories();
    }
}
