using Microsoft.AspNetCore.Mvc;

namespace Les08.Areas.Admins.Controllers

    
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
