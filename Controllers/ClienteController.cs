using DelfosMachine.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Route("Cliente")] 
public class ClienteController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClienteController(ApplicationDbContext context)
    {
        _context = context;
    }

    // usar essaa tag para permitir que todos possam fazer cadastrado, mas quem não estiver logado, não vai conseguir acessar nada.
    [AllowAnonymous]
    // GET: Cliente/Criar
    [HttpGet("Criar")]
    public IActionResult Criar()
    {
        return View();
    }

    [AllowAnonymous]
    // POST: Cliente/Criar
    [HttpPost("Criar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar([Bind("Id,Nome,Email,Telefone,Genero,DataNasc,Senha")] Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Cliente cadastrado com sucesso!";
            return RedirectToAction("Mensagem");
        }
        return View(cliente);
    }

    public IActionResult Mensagem()
    {
        return View();
    }

    // GET: Cliente/ConsultarTodos
    [HttpGet("Consultar")]
    public async Task<IActionResult> Consultar()
    {
        var clientes = await _context.Clientes.ToListAsync(); 
        return View(clientes); 
    }



}
