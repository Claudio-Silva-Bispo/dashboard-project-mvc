using BIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BIProject.Controllers
{
    public class ConceptDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConceptDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize]
        [HttpGet("ConceptDetails")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        [SwaggerOperation(Summary = "Cria o conceito completo, ligado ao título do produto ou área", Description = "Cadastra o conceito com título, subtítulo e descrição na aplicação.")]
        [ProducesResponseType(typeof(User), 201)] 
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(ConceptDetails conceptDetails)
        {
            var conceptTitle = await _context.ConceitoTitulo.ToListAsync();

            ViewData["Concepts"] = conceptTitle;

            if (ModelState.IsValid)
            {
                _context.Add(conceptDetails);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Conceito cadastrado com sucesso!";
            }

            return View(conceptDetails);
        }
    }
}
