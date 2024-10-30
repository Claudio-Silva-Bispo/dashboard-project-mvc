using DelfosMachine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Route("Cliente")] // Mantendo rota simples como 'Cliente'
public class ClienteController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClienteController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Cliente/Criar
    [HttpGet("Criar")]
    public IActionResult Criar()
    {
        return View();
    }

    // POST: Cliente/Criar
    [HttpPost("Criar")]
    [ValidateAntiForgeryToken] // Protege contra ataques CSRF
    public async Task<IActionResult> Criar([Bind("Id,Nome,Email,Telefone")] Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync(); 
            
            // Armazenar a mensagem de sucesso em TempData
            TempData["SuccessMessage"] = "Cliente cadastrado com sucesso!";
            return RedirectToAction("MensagemSucesso");
        }
        return View(cliente);
    }

    public IActionResult MensagemSucesso()
    {
        return View();
    }

    // GET: Cliente/ConsultarTodos
    [HttpGet("ConsultarTodos")]
    public async Task<IActionResult> ConsultarTodos()
    {
        var clientes = await _context.Clientes.ToListAsync(); 
        return View(clientes); 
    }

    // GET: Cliente/Atualizar/5
    [HttpGet("Atualizar/{id}")]
    public async Task<IActionResult> Atualizar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente); // Retorna o formulário com os dados do cliente
    }

    // POST: Cliente/Atualizar/5
    [HttpPost("Atualizar/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Atualizar(int id, [Bind("Id,Nome,Email,Telefone")] Cliente cliente)
    {
        if (id != cliente.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cliente); // Atualiza os dados do cliente no banco
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExiste(cliente.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(ConsultarTodos)); // Redireciona para a lista de clientes após atualizar
        }
        return View(cliente); // Se houver erro, retorna à view com o modelo
    }

    // GET: Cliente/Deletar/5
    [HttpGet("Deletar/{id}")]
    public async Task<IActionResult> Deletar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);
        if (cliente == null)
        {
            return NotFound();
        }

        return View(cliente); // Retorna a view de confirmação para deletar
    }

    // POST: Cliente/Deletar/5
    [HttpPost("Deletar/{id}"), ActionName("DeletarConfirmado")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletarConfirmado(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente == null){
            return NotFound();
        }

        _context.Clientes.Remove(cliente); 
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(ConsultarTodos)); 
    }

    private bool ClienteExiste(int id)
    {
        return _context.Clientes.Any(e => e.Id == id);
    }

}
