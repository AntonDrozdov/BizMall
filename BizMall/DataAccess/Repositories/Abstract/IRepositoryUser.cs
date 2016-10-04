using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BizMall.Models.CompanyModels;
using BizMall.Models.AccountModels;

namespace BizMall.DataAccess.Repositories.Abstract
{
    public interface IRepositoryUser
    {
        ApplicationUser GetCurrentUser(string UserEmail);
    }
}
