using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Http;
using System.IO;

using BizMall.Models.CompanyModels;
using BizMall.Models.CommonModels;
using BizMall.DataAccess.DBContexts;
using BizMall.DataAccess.Repositories.Abstract;


namespace BizMall.DataAccess.Repositories.Concrete
{
    public class RepositoryGood : RepositoryBase, IRepository, IRepositoryGood
    {
        private readonly IRepositoryImage _repositoryImage;

        public RepositoryGood(ApplicationDbContext ctx, IRepositoryImage repositoryImage) : base(ctx)
        {
            _repositoryImage = repositoryImage;
        }

        public Good GetGood(int goodId) {
            return _ctx.Goods.Where(g => g.Id == goodId).Include(g => g.Category).Include(g => g.Category.ParentCategory).Include(g => g.Images).FirstOrDefault();
        }

        public void DeleteGood(int goodId) {
            Good good = _ctx.Goods.Where(g => g.Id == goodId).Include(g => g.Category).Include(g => g.Category.ParentCategory).Include(g => g.Images).FirstOrDefault();
            _ctx.Goods.Remove(good);
            _ctx.SaveChanges();
        }

        public IQueryable<Good> ShopGoods(int ShopId)
        {
            //получаем список ид товаров магазина из объектов RelShopGood поля Goods, что есть связующие объекты между таблицей магазинов и таблицей товаров
            List<int> ShopGoodsIds = new List<int>();
            foreach (RelCompanyGood rsg in _ctx.Companies.Where(s => s.Id == ShopId).FirstOrDefault().Goods)
                ShopGoodsIds.Add(rsg.GoodId);

            //выбираем из таблицы товаров все, ид которых, содержаться в вышеопределенной коллекции необходимых ид
            return _ctx.Goods.Where(g => ShopGoodsIds.Contains(g.Id));
        }

        public IQueryable<Good> ShopGoodsFullInformation(int ShopId)
        {

            //получаем список ид товаров магазина из объектов RelShopGood поля Goods, что есть связующие объекты между таблицей магазинов и таблицей товаров
            List<int> ShopGoodsIds = new List<int>();
            foreach (RelCompanyGood rsg in _ctx.Companies.Where(s => s.Id == ShopId).FirstOrDefault().Goods)
                ShopGoodsIds.Add(rsg.GoodId);

            //выбираем из таблицы товаров все, ид которых, содержаться в вышеопределенной коллекции необходимых ид
            List<Good> Goods = _ctx.Goods
                    .Where(g => ShopGoodsIds.Contains(g.Id))
                    .Include(g => g.Category)
                    .Include(g => g.Category.ParentCategory)
                    .Include(g => g.Images)
                    .ToList();

            return Goods.AsQueryable();
        }

        public Good SaveGood(Good good, Company company, ICollection<IFormFile> newimages)
        {
            if (good.Id != 0)//если изменение позиции
            {
                _ctx.Entry(good).State = EntityState.Modified;
                _ctx.SaveChanges();
            }
            else {          //если новая позиция
                _ctx.Goods.Add(good);
                _ctx.SaveChanges();

                //теперь создаем обхъкт связку товар - магазин
                RelCompanyGood rsg = new RelCompanyGood() { Good = good, GoodId = good.Id, Company = company, CompanyId = company.Id };
                //добавляем объект связку в товар
                good.Companies.Add(rsg);
            }
            //удаляем

            //сначала добавляем картинки в бд и тут же в коллекцию изображений товара
            foreach (IFormFile im in newimages)
            {
                Image newim = new Image {
                    Id = 0,
                    IsMain = true,
                    Description = "",
                    ImageMimeType = im.ContentType,
                    GoodId = good.Id
                };
                
                using (var reader = new StreamReader(im.OpenReadStream()))
                {
                    Stream stream = reader.BaseStream;
                    Byte[] inArray = new Byte[(int)stream.Length];
                    stream.Read(inArray, 0, (int)stream.Length);

                    newim.ImageContent = inArray;
                }
                //*картинки в бд
                _repositoryImage.SaveImage(newim);

                //*тут же в коллекцию изображений товара
                good.Images.Clear();
                good.Images.Add(newim);
            }

            //созраняемся
            _ctx.Entry(good).State = EntityState.Modified;
            _ctx.SaveChanges();

            return good;
        }
    }
}
