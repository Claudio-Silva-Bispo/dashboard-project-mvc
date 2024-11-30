using BIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("ConceptDetails/Create")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Create()
        {
            var viewModel = new ConceptDetailsViewModel
            {
                ConceptDetails = new ConceptDetails(),
                ConceptTitles = _context.ConceitoTitulo.ToList() 
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost("ConceptDetails/Create")]
        [ValidateAntiForgeryToken]
        [SwaggerOperation(Summary = "Cria o conceito completo, ligado ao título do produto ou área", Description = "Cadastra o conceito com título, subtítulo e descrição na aplicação.")]
        [ProducesResponseType(typeof(User), 201)] 
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(ConceptDetailsViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                _context.Add(viewModel.ConceptDetails);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Conceito cadastrado com sucesso!";
                return RedirectToAction("Create");
            }

            viewModel.ConceptTitles = _context.ConceitoTitulo.ToList();
            return View(viewModel);
        }
    }
}
