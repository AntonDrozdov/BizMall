using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BizMall.Models.CommonModels;

namespace BizMall.DataAccess.Repositories.Abstract
{
    public interface IRepositoryImage
    {
        Image SaveImage(Image item);
        Image GetImage(int ImageId);
        void DeleteImage(int ImageId);
        Image GetGoodImage(int GoodId);
    }
}
