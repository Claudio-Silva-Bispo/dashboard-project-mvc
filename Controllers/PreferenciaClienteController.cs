using Microsoft.AspNetCore.Mvc;
using DelfosMachine.Models;
using Microsoft.EntityFrameworkCore;

namespace DelfosMachine.Controllers
{
    public class PreferenciaClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreferenciaClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar([Bind("Id,IdCliente,IdEndereco,EnderecoPreferencia,TurnoPreferencia,HorarioPreferencia,DiaSemanaPreferencia")] PreferenciaCliente preferenciaCliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preferenciaCliente);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Preferência cadastrada com sucesso!";
                return RedirectToAction("Mensagem");
            }
            return View(preferenciaCliente);
        }

        public IActionResult Mensagem()
        {
            return View();
        }


        [HttpGet("Consultar")]
        public async Task<IActionResult> Consultar()
        {
            var preferencias = await _context.PreferenciasClientes
                .Include(p => p.EnderecoPreferencia)
                .Include(p => p.DiaSemanaPreferencia)
                .Include(p => p.TurnoPreferencia)
                .Include(p => p.HorarioPreferencia)
                .ToListAsync();
            return View(preferencias);
        }

    }
}
