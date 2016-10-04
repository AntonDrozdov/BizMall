using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BizMall.DataAccess.DBContexts;
using BizMall.DataAccess.Repositories.Abstract;
using BizMall.Models.CommonModels;
using Microsoft.Data.Entity;

namespace BizMall.DataAccess.Repositories.Concrete
{
    public class RepositoryImage : RepositoryBase, IRepository, IRepositoryImage  
    {
        public RepositoryImage(ApplicationDbContext ctx) : base(ctx)
        {
        }

        public Image SaveImage(Image item)
        {
            _ctx.Images.Add(item);
            _ctx.SaveChanges();
            return item; 
        }

        public void DeleteImage(int imageId) {
            Image image = _ctx.Images.Where(i => i.Id == imageId).FirstOrDefault();
            _ctx.Images.Remove(image);
            _ctx.SaveChanges();
        }

        public Image GetImage(int ImageId) {
            return _ctx.Images.Where(i => i.Id == ImageId).SingleOrDefault();
        }
        public Image GetGoodImage(int GoodId)
        {
            return _ctx.Images.Where(i => i.GoodId == GoodId).SingleOrDefault();
        }
    }
}
