using Microsoft.AspNetCore.Mvc;

namespace DelfosMachine.Controllers
{
    public class PaginaClienteController : Controller
    {
   
        public IActionResult Index()
        {
            return View();
        }
    }
}
