using Microsoft.AspNetCore.Mvc;
using DelfosMachine.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DelfosMachine.Controllers
{
    public class HorarioPreferenciaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HorarioPreferenciaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CriarHorarioPreferencia()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarHorarioPreferencia(PreferenciaCliente preferenciaCliente)
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


    }
}
