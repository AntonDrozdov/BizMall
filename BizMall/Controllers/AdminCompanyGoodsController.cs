using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;


using BizMall.DataAccess.Repositories.Abstract;
using BizMall.ViewModels.AdminCompanyGoods;
using BizMall.Models.CompanyModels;
using BizMall.Models.CommonModels;
using BizMall.Utils;

namespace BizMall.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AdminCompanyGoodsController : Controller
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly IRepositoryCompany _repositoryCompany;
        private readonly IRepositoryGood _repositoryGood;
        private readonly IRepositoryImage _repositoryImage;
        

        public AdminCompanyGoodsController(IRepositoryUser repositoryUser, IRepositoryCompany repositoryCompany, IRepositoryGood repositoryGood, IRepositoryImage repositoryImage)
        {
            _repositoryUser = repositoryUser;
            _repositoryCompany = repositoryCompany;
            _repositoryGood = repositoryGood;
            _repositoryImage = repositoryImage;
        }

        public FileContentResult GetGoodMainImage(int GoodId) {
            Image image = _repositoryImage.GetGoodImage(GoodId);
            var fcr = File(image.ImageContent, image.ImageMimeType);
            return fcr;
        }

        public IActionResult Goods(GoodStatus goodsStatus = GoodStatus.Active)
        {
            var currentUser = _repositoryUser.GetCurrentUser(User.Identity.Name);
            
            if (currentUser != null)
            {
                var Company = _repositoryCompany.GetUserCompany(currentUser);
                var Goods = _repositoryGood.ShopGoodsFullInformation(Company.Id).ToList();

                List<GoodViewModel> GoodsVM = new List<GoodViewModel>();
                foreach (var good in Goods) {
                    GoodViewModel gvm = new GoodViewModel
                    {
                        Amount = good.Amount,
                        Category = good.Category,
                        CategoryId = good.CategoryId,
                        Companies = good.Companies,
                        Description = good.Description,
                        Id = good.Id,
                        Images = good.Images,
                        Title = good.Title
                    };
                    if (good.Images.Count != 0)
                        gvm.MainImageInBase64 = FromByteToBase64Converter.GetImageBase64Src(good.Images.ToList()[0]);
                    GoodsVM.Add(gvm);
                }
                ViewBag.GoodsVM = GoodsVM;
            }
            else
            {
                Redirect("/");
            }

            ViewBag.ActiveSubMenu = "Товары/Услуги";
            if(goodsStatus==GoodStatus.Active)
                ViewBag.ActiveGoodsStatusMenu = 1;
            if (goodsStatus == GoodStatus.InActive)
                ViewBag.ActiveGoodsStatusMenu = 0;
            return View();
        }

        [HttpGet]
        public IActionResult EditGood(int goodId)
        {
            CreateEditGoodViewModel cegvm = null;
            if (goodId != 0)
            {
                Good good = _repositoryGood.GetGood(goodId);

                cegvm = new CreateEditGoodViewModel
                {
                    Category = good.Category.Title,
                    CategoryId = good.CategoryId,
                    Description = good.Description,
                    Id = good.Id,
                    Images = good.Images,
                    Title = good.Title
                };
                if (good.Images.Count != 0) {
                    cegvm.MainImageInBase64 = FromByteToBase64Converter.GetImageBase64Src(good.Images.ToList()[0]);
                    foreach (var image in good.Images) 
                        cegvm.ImageViewModels.Add(
                            new ImageViewModel {
                                GoodId = image.GoodId,
                                Id = image.Id,
                                goodImageIds = image.GoodId+"_"+ image.Id,
                                ImageMimeType = image.ImageMimeType,
                                ImageInBase64 = FromByteToBase64Converter.GetImageBase64Src(image)
                            }
                        );
                }
            }
            else 
                cegvm = new CreateEditGoodViewModel();
            
            return View(cegvm);
        }
        [HttpPost]
        public IActionResult EditGood(CreateEditGoodViewModel model, ICollection<IFormFile> newimages)
        {
            if (ModelState.IsValid)
            {
                //тек пользователь
                var currentUser = _repositoryUser.GetCurrentUser(User.Identity.Name);

                //соответсвующий магазин
                Company company = new Company();
                if (currentUser != null)
                    company = _repositoryCompany.GetUserCompany(currentUser);
                else
                    return RedirectToAction("Goods");
                
                _repositoryGood.SaveGood(new Good { Id = model.Id, Title = model.Title, Description = model.Description, CategoryId = Convert.ToInt32(model.CategoryId) }, company, newimages);

                return RedirectToAction("Goods");
            }
            return RedirectToAction("Goods");
        }

        [HttpGet]
        public IActionResult DeleteGood(int goodId)
        {
            if (goodId != 0)
            {
                _repositoryGood.DeleteGood(goodId);
            }
            return RedirectToAction("Goods");
        }

        public IActionResult DeleteGoodImage(string goodImageId) {
            string[] parameteres = goodImageId.Split('_');

            int goodId = Convert.ToInt32(parameteres[0]);
            int imageId = Convert.ToInt32(parameteres[1]);

            _repositoryImage.DeleteImage(imageId);
            return RedirectToAction("EditGood", new { goodId = goodId }); 
        }
    }
}
