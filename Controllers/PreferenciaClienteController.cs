using Microsoft.AspNetCore.Mvc;
using DelfosMachine.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

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
        public IActionResult Preferencia()
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
                return RedirectToAction("MensagemSucesso");
            }
            return View(preferenciaCliente);
        }

        public IActionResult MensagemSucesso()
        {
            return View();
        }

        // [HttpGet("ConsultarPreferencia")]
        // public async Task<IActionResult> ConsultarPreferencia()
        // {
        //     var preferencias = await _context.PreferenciasClientes.ToListAsync();
        //     return View(preferencias);
        // }

        [HttpGet("ConsultarPreferencia")]
        public async Task<IActionResult> ConsultarPreferencia()
        {
            var preferencias = await _context.PreferenciasClientes
                .Include(p => p.EnderecoPreferencia)
                .Include(p => p.DiaSemanaPreferencia)
                .Include(p => p.TurnoPreferencia)
                .Include(p => p.HorarioPreferencia)
                .ToListAsync();
            return View(preferencias);
        }

        [HttpGet("Atualizar/{id}")]
        public async Task<IActionResult> Atualizar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preferencia = await _context.PreferenciasClientes.FindAsync(id);
            if (preferencia == null)
            {
                return NotFound();
            }
            return View(preferencia);
        }

        [HttpPost("Atualizar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(int id, [Bind("Id,IdCliente,IdEndereco,EnderecoPreferencia,TurnoPreferencia,HorarioPreferencia,DiaSemanaPreferencia")] PreferenciaCliente preferenciaCliente)
        {
            if (id != preferenciaCliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preferenciaCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreferenciaClienteExiste(preferenciaCliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ConsultarPreferencia));
            }
            return View(preferenciaCliente);
        }

        [HttpGet("Deletar/{id}")]
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preferencia = await _context.PreferenciasClientes.FirstOrDefaultAsync(m => m.Id == id);
            if (preferencia == null)
            {
                return NotFound();
            }

            return View(preferencia);
        }

        [HttpPost("Deletar/{id}"), ActionName("DeletarConfirmado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarConfirmado(int id)
        {
            var preferencia = await _context.PreferenciasClientes.FindAsync(id);

            if (preferencia == null)
            {
                return NotFound();
            }

            _context.PreferenciasClientes.Remove(preferencia);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ConsultarPreferencia));
        }

        private bool PreferenciaClienteExiste(int id)
        {
            return _context.PreferenciasClientes.Any(e => e.Id == id);
        }
    }
}
