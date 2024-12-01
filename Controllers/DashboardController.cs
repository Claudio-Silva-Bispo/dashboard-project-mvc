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
        [HttpGet("Dashboard/Index")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SwaggerOperation(Summary = "Cadastra os links dos relatórios no banco", Description = "Cria uma lista de relatórios com links para hospedarmos no sistema sem o usuário ter o acesso público ou privado, apenas consiga visualizar os dados.")]
        [ProducesResponseType(typeof(User), 201)] 
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(Dashboard dashboard)
        {

            if (ModelState.IsValid)
            {
                _context.Add(dashboard);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Relatório cadastrado com sucesso!";
                return RedirectToAction("Create");
            }

            return View(dashboard);
        }

        [Authorize]
        [HttpGet("Consult")]
        public async Task<IActionResult> Consult()
        {
            var dashboards = await _context.Dashboard.ToListAsync();

            if (dashboards == null || !dashboards.Any())
            {
                return RedirectToAction("Error");
            }

            return View(dashboards);
            
        }

        [HttpGet("Update")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Update()
        {

            return View();
        }

        [HttpPost("Update")]
        [SwaggerOperation(Summary = "Atualiza as informações dos relatórios de forma completa", Description = "Atualiza os dados de um relatório com base no ID.")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(400)]  
        [ProducesResponseType(404)] 
        public async Task<IActionResult> Update(Dashboard dashboard)
        {
            if (!ModelState.IsValid)
            {
                return View(dashboard);
            }

            var dashboardExistente = await _context.Dashboard.FindAsync(dashboard.Id);
            
            if (dashboardExistente == null)
            {
                return NotFound();
            }

            dashboardExistente.Id = dashboard.Id;
            dashboardExistente.NomeRelatorio = dashboard.NomeRelatorio;
            dashboardExistente.Solicitante = dashboard.Solicitante;
            dashboardExistente.AnalistaCriador = dashboard.AnalistaCriador;
            dashboardExistente.DataCriacao = dashboard.DataCriacao;
            dashboardExistente.Link = dashboard.Link;
            
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Dados atualizados com sucesso!";
            return View(dashboardExistente);
        }

    }
}
