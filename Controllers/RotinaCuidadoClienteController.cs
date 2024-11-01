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
            return RedirectToAction("MensagemSucesso");
        }
        return View(rotinaCuidado);
    }

    public IActionResult MensagemSucesso()
    {
        return View();
    }

    // GET: RotinaCuidadoCliente/ConsultarRotina
    [HttpGet("ConsultarRotina")]
    public async Task<IActionResult> ConsultarRotina()
    {
        var rotinas = await _context.RotinaCuidado.ToListAsync(); 
        return View(rotinas); 
    }

    // GET: RotinaCuidadoCliente/Atualizar/5
    [HttpGet("Atualizar/{id}")]
    public async Task<IActionResult> Atualizar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rotinaCuidado = await _context.RotinaCuidado.FindAsync(id);
        if (rotinaCuidado == null)
        {
            return NotFound();
        }
        return View(rotinaCuidado); 
    }

    // POST: RotinaCuidado/Atualizar/5
    [HttpPost("Atualizar/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Atualizar(int id, [Bind("Id,IdCliente,HistoricoMedico,FrequenciaEscovacao,FrequenciaFioDental,FrequenciaEnxaguante,SintomasAtuais,HabitosAlimentares,FrequenciaVisitasDentista,CuidadosEspecificos")] RotinaCuidadoCliente rotinaCuidado)
    {
        if (id != rotinaCuidado.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(rotinaCuidado); 
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RotinaCuidadoClienteExiste(rotinaCuidado.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(ConsultarRotina)); 
        }
        return View(rotinaCuidado);
    }

    // GET: RotinaCuidado/Deletar/5
    [HttpGet("Deletar/{id}")]
    public async Task<IActionResult> Deletar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rotinaCuidado = await _context.RotinaCuidado.FirstOrDefaultAsync(m => m.Id == id);
        if (rotinaCuidado == null)
        {
            return NotFound();
        }

        return View(rotinaCuidado);
    }

    // POST: RotinaCuidado/Deletar/5
    [HttpPost("Deletar/{id}"), ActionName("DeletarConfirmado")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletarConfirmado(int id)
    {
        var rotinaCuidado = await _context.RotinaCuidado.FindAsync(id);

        if (rotinaCuidado == null){
            return NotFound();
        }

        _context.RotinaCuidado.Remove(rotinaCuidado); 
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(ConsultarRotina)); 
    }

    private bool RotinaCuidadoClienteExiste(int id)
    {
        return _context.RotinaCuidado.Any(e => e.Id == id);
    }
}
