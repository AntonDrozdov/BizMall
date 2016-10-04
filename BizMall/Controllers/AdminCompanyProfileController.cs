using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BizMall.Controllers
{
    public class AdminCompanyProfileController : Controller
    {
        
        public IActionResult Profile()
        {
            ViewBag.ActiveSubMenu = "Профиль";
            return View();
        }
    }
}
