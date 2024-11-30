using System.Security.Claims;
using BIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BIProject.Controllers
{
    [Route("ConceptTitle")]
    public class ConceptTitleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConceptTitleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Create()
        {
            ViewData["Concepts"] = _context.ConceitoTitulo.ToList();
            return View();
        }

        [Authorize]
        [HttpPost("SaveTitle")]
        [ValidateAntiForgeryToken]
        [SwaggerOperation(Summary = "Cria um título para o processo de conceito", Description = "Cadastra um conceito na aplicação com valor único para alguma área especifíca ou produto.")]
        [ProducesResponseType(typeof(User), 201)] 
        [ProducesResponseType(400)]
        public async Task<IActionResult> SaveTitle(ConceptTitle conceptTitle)
        {

            // Verifica se já existe um título com o mesmo nome
            bool titleExists = await _context.ConceitoTitulo.AnyAsync(c => c.Titulo == conceptTitle.Titulo);

            if (titleExists)
            {
                TempData["ErrorMessage"] = "O título já existe! Por favor, escolha um nome diferente.";
                return View("Create");
            }

            if (ModelState.IsValid)
            {
                _context.Add(conceptTitle);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Título cadastrado com sucesso!";
                return View("Create");
            }
            else
            {
                TempData["ErrorMessage"] = "Erro ao cadastrar o título. Verifique os dados e tente novamente.";
            }

            ViewData["Concepts"] = _context.ConceitoTitulo.ToList();
            return View("Create");
        }

        [Authorize]
        [HttpGet("Consult")]
        public async Task<IActionResult> Consult()
        {
            var conceptTitles = await _context.ConceitoTitulo.ToListAsync();

            if (conceptTitles == null || !conceptTitles.Any())
            {
                return RedirectToAction("Error");
            }

            return View(conceptTitles);
            
        }

        public IActionResult Index()
        {
            var concepts = _context.ConceitoTitulo.ToList();
            return View(concepts);  
        }

        [HttpGet("Update")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Update()
        {

            return View();
        }

        [HttpPost("Update")]
        [SwaggerOperation(Summary = "Atualiza as informações de um título", Description = "Atualiza os dados do título com base no ID.")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(400)]  
        [ProducesResponseType(404)] 
        public async Task<IActionResult> Update(ConceptTitle conceptTitle)
        {
            if (!ModelState.IsValid)
            {
                return View(conceptTitle);
            }

            var ConceptTitleExistente = await _context.ConceitoTitulo.FindAsync(conceptTitle.Id);
            
            if (ConceptTitleExistente == null)
            {
                return NotFound();
            }

            ConceptTitleExistente.Id = conceptTitle.Id;
            ConceptTitleExistente.Titulo = conceptTitle.Titulo;
            
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Título atualizado com sucesso!";

            return View(ConceptTitleExistente);
        }

    }
}
