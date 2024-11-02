using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DelfosMachine.Models;
using System.Security.Claims;

namespace DelfosMachine.Controllers
{
    public class DadosCadastraisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DadosCadastraisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DadosCadastrais/Consultar
        public async Task<IActionResult> Consultar()
        {
            // Recupera o ID do usuário logado para podermos fazer alterações no cadastro
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            if (userId == null)
            {
                return Unauthorized();
            }

            // Converte o userId para int
            int userIdInt;
            if (!int.TryParse(userId, out userIdInt))
            {
                return BadRequest("ID de usuário inválido.");
            }

            // Busca o cliente relacionado ao usuário logado
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == userIdInt);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            // Carrega os dados cadastrais do cliente com suas relações
            var dadosCadastrais = await _context.DadosCadastrais
                // Carregar o cliente
                .Include(d => d.Cliente) 
                // Carrega as preferências
                .Include(d => d.PreferenciaCliente) 
                // Carrega a rotina de cuidado
                .Include(d => d.RotinaCuidadoCliente) 
                .FirstOrDefaultAsync(d => d.IdCliente == cliente.Id);

            if (dadosCadastrais == null)
            {
                return NotFound("Dados cadastrais não encontrados.");
            }

            return View(dadosCadastrais);
        }

        // GET: DadosCadastrais/Atualizar/5
        public async Task<IActionResult> Atualizar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dadosCadastrais = await _context.DadosCadastrais
                .Include(d => d.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (dadosCadastrais == null)
            {
                return NotFound();
            }

            return View(dadosCadastrais);
        }

        // POST: DadosCadastrais/Atualizar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(int id, [Bind("Id,IdCliente,IdPreferenciaCliente,IdRotinaCuidadoCliente,Cliente,PreferenciaCliente,RotinaCuidadoCliente")] DadosCadastrais dadosCadastrais)
        {
            if (id != dadosCadastrais.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dadosCadastrais);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DadosCadastraisExists(dadosCadastrais.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Consultar));
            }
            return View(dadosCadastrais);
        }

        private bool DadosCadastraisExists(int id)
        {
            return _context.DadosCadastrais.Any(e => e.Id == id);
        }
    }
}
