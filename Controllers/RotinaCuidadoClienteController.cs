using DelfosMachine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("RotinaCuidadoCliente")] 
public class RotinaCuidadoClienteController : Controller
{
    private readonly ApplicationDbContext _context;

    public RotinaCuidadoClienteController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: RotinaCuidadoCliente/Criar
    [HttpGet("Criar")]
    public IActionResult Criar()
    {
        return View();
    }

    // POST: RotinaCuidadoCliente/Criar
    [HttpPost("Criar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar([Bind("Id,IdCliente,HistoricoMedico,FrequenciaEscovacao,FrequenciaFioDental,FrequenciaEnxaguante,SintomasAtuais,HabitosAlimentares,FrequenciaVisitasDentista,CuidadosEspecificos")] RotinaCuidadoCliente rotinaCuidado)
    {
        if (ModelState.IsValid)
        {
            _context.Add(rotinaCuidado);
            await _context.SaveChangesAsync(); 
            
            // Armazenar a mensagem de sucesso em TempData
            TempData["SuccessMessage"] = "Rotina de cuidado cadastrada com sucesso!";
            return RedirectToAction("Mensagem");
        }
        return View(rotinaCuidado);
    }

    public IActionResult Mensagem()
    {
        return View();
    }

    // GET: RotinaCuidadoCliente/ConsultarRotina
    [HttpGet("Consultar")]
    public async Task<IActionResult> Consultar()
    {
        var rotinas = await _context.RotinaCuidado.ToListAsync(); 
        return View(rotinas); 
    }

}
