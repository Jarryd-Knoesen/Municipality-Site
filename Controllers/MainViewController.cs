using Microsoft.AspNetCore.Mvc;

namespace PROG7312_P1_V1.Controllers
{
    public class MainViewController : Controller
    {
        public IActionResult MainView()
        {
            return View();
        }
    }
}
