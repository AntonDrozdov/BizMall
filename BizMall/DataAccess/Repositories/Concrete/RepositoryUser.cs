
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BizMall.DataAccess.Repositories.Abstract;
using BizMall.Models.AccountModels;
using BizMall.DataAccess.DBContexts;

namespace BizMall.DataAccess.Repositories.Concrete
{
    public class RepositoryUser: RepositoryBase, IRepository, IRepositoryUser
    {
        public RepositoryUser(ApplicationDbContext ctx) : base(ctx)
        {
        }

        public ApplicationUser GetCurrentUser(string UserEmail)
        {
            return _ctx.Users.Where(u => u.Email == UserEmail).FirstOrDefault();
        }
    }
}
