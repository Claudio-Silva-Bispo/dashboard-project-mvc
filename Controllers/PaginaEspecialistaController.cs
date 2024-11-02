using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DelfosMachine.Controllers
{
    [Authorize]
    public class PaginaEspecialistaController : Controller
    {
   
        public IActionResult Index()
        {
            return View();
        }
    }
}
