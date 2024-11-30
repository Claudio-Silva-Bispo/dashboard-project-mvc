using BIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BIProject.Controllers
{
    [Route("Concept")]
    public class ConceptController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConceptController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var concept = await _context.ConceitoDetalhes.ToListAsync();

            if (concept == null || !concept.Any())
            {
                return RedirectToAction("Error");
            }

            return View(concept);
            
        }


    }
}
