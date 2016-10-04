using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using BizMall.DataAccess.Repositories.Abstract;
using BizMall.ViewModels.Manage;
using BizMall.ViewModels.AdminCompanyGoods;

namespace BizMall.ViewComponents
{
    public class AllCategories : ViewComponent
    {
        private readonly IRepositoryCategory _repositoryCategory;

        public AllCategories(IRepositoryCategory repositoryCategory) {
            _repositoryCategory = repositoryCategory;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Categories = _repositoryCategory.Categories().ToList();
            return View();
        }

        public IViewComponentResult Invoke(CreateEditGoodViewModel cegvm)
        {
            ViewBag.Categories = _repositoryCategory.Categories().ToList();
            //string[] ws = cegvm.Category.Split('/');
            //ViewBag.FW = ws[0];
            return View(cegvm);
        }
    }
}
