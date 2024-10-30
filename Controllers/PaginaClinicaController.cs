using Microsoft.AspNetCore.Mvc;

namespace DelfosMachine.Controllers
{
    public class PaginaClinicaController : Controller
    {
   
        public IActionResult Index()
        {
            return View();
        }
    }
}
