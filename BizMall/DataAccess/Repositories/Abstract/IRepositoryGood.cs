using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

using BizMall.Models.CompanyModels;

namespace BizMall.DataAccess.Repositories.Abstract
{
    public interface IRepositoryGood
    {
        Good GetGood(int goodId);
        void DeleteGood(int goodId);
        IQueryable<Good> ShopGoodsFullInformation(int ShopId);
        IQueryable<Good> ShopGoods(int ShopId);
        Good SaveGood(Good good, Company company, ICollection<IFormFile> newimages);
    }
}
