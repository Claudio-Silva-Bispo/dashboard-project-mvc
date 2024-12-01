using BIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BIProject.Controllers
{
    [Route("Dashboard")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("Dashboard/Create")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
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

        [Authorize]
        [HttpGet("Consult")]
        public async Task<IActionResult> Consult()
        {
            var conceptTitles = await _context.ConceitoDetalhes.ToListAsync();

            if (conceptTitles == null || !conceptTitles.Any())
            {
                return RedirectToAction("Error");
            }

            return View(conceptTitles);
            
        }

        [HttpGet("Update")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Update()
        {

            return View();
        }

        [HttpPost("Update")]
        [SwaggerOperation(Summary = "Atualiza as informações do conceito completo", Description = "Atualiza os dados do conceito com base no ID.")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(400)]  
        [ProducesResponseType(404)] 
        public async Task<IActionResult> Update(ConceptDetails conceptDetails)
        {
            if (!ModelState.IsValid)
            {
                return View(conceptDetails);
            }

            var ConceptDetailsExistente = await _context.ConceitoDetalhes.FindAsync(conceptDetails.Id);
            
            if (ConceptDetailsExistente == null)
            {
                return NotFound();
            }

            ConceptDetailsExistente.Id = conceptDetails.Id;
            ConceptDetailsExistente.IdTitulo = conceptDetails.IdTitulo;
            ConceptDetailsExistente.Titulo = conceptDetails.Titulo;
            ConceptDetailsExistente.Subtitulo = conceptDetails.Subtitulo;
            ConceptDetailsExistente.Descricao = conceptDetails.Descricao;
            
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Dados atualizados com sucesso!";

            return View(ConceptDetailsExistente);
        }

    }
}
