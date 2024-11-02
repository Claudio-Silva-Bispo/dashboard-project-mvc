using Microsoft.AspNetCore.Mvc;
using DelfosMachine.Models;
using Microsoft.EntityFrameworkCore;

namespace DelfosMachine.Controllers
{
    public class EnderecoPreferenciaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnderecoPreferenciaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(EnderecoPreferencia endereco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(endereco);
                await _context.SaveChangesAsync();

                // TempData["SuccessMessage"] = "Preferência cadastrada com sucesso!";
                // return RedirectToAction("Mensagem");
            }
            return View(endereco);
        }

        // GET: Cliente/ConsultarTodos
        [HttpGet("Consultar")]
        public async Task<IActionResult> Consultar()
        {
            var dados = await _context.EnderecoPreferencia.ToListAsync(); 
            return View(dados); 
        }

        // public IActionResult Mensagem()
        // {
        //     return View();
        // }


    }
}
