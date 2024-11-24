
using Microsoft.AspNetCore.Mvc;

namespace BIProject.Controllers
{
    public class LoginHistoryController : Controller
    {
    
        private readonly ApplicationDbContext _context;

        public LoginHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Consult()
        {
            var logs = _context.Login.ToList(); 
            return View(logs); 
        }

    }
    
}