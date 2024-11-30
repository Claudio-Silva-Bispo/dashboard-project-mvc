using BIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authentication;

[Route("User")] 
public class UserController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    // usar essaa tag para permitir que todos possam fazer cadastrado, mas quem não estiver logado, não vai conseguir acessar nada.
    [AllowAnonymous]
    [HttpGet("Create")]
    [ApiExplorerSettings(IgnoreApi = true)] // Ignora este método no Swagger
    public IActionResult Create()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    [SwaggerOperation(Summary = "Cria um novo usuário", Description = "Cadastra um usuário na aplicação com perfil comum. Demais perfis, precisa ser atualizado pelo adm do BI.")]
    [ProducesResponseType(typeof(User), 201)] 
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([Bind("Id,Nome,Email,Area,Senha")] User usuario)
    {
        if (ModelState.IsValid)
        {
            // Atribuindo o perfil "Comum" por padrão
            usuario.Perfil = "Comum";
            
            _context.Add(usuario);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Usuário cadastrado com sucesso!";
            return RedirectToAction("Mensagem");
        }
        return View(usuario);
    }

    [HttpGet("Message")]
    [ApiExplorerSettings(IgnoreApi = true)] 
    public IActionResult Mensagem()
    {
        return View();
    }

    [HttpGet("Consult")]
    [SwaggerOperation(Summary = "Consulta todos os usuários", Description = "Retorna uma lista de todos os usuários cadastrados.")]
    [ProducesResponseType(typeof(IList<User>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Consult()
    {
        var usuarios = await _context.Usuario.ToListAsync(); 
        return View(usuarios); 
    }

    [HttpGet("Update")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Update()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
        {
            return RedirectToAction("Error");
        }

        var usuario = await _context.Usuario.FindAsync(userId);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    [HttpPost("Update")]
    [SwaggerOperation(Summary = "Atualiza as informações de um usuário", Description = "Atualiza os dados do usuário com base no ID.")]
    [ProducesResponseType(typeof(User), 200)]
    [ProducesResponseType(400)]  
    [ProducesResponseType(404)] 
    public async Task<IActionResult> Update(User usuario)
    {
        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
        {
            return RedirectToAction("Error");
        }

        var usuarioExistente = await _context.Usuario.FindAsync(userId);
        if (usuarioExistente == null)
        {
            return NotFound();
        }

        usuarioExistente.Nome = usuario.Nome;
        usuarioExistente.Email = usuario.Email;
        usuarioExistente.Area = usuario.Area;

        if (!string.IsNullOrEmpty(usuario.Senha))
        {
            usuarioExistente.Senha = usuario.Senha;
        }

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Usuário atualizado com sucesso!";

        return View(usuarioExistente);
    }


    [HttpGet("ConfirmarExcluir/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmarExcluir(int id)
    {
        var usuario = await _context.Usuario.FindAsync(id); 

        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [SwaggerOperation(Summary = "Exclui um usuário", Description = "Exclui permanentemente um usuário do sistema.")]
    [ProducesResponseType(200)] 
    [ProducesResponseType(404)] 
    [ProducesResponseType(400)] 
    public async Task<IActionResult> Excluir(int id)
    {
        var usuario = await _context.Usuario.FindAsync(id); 
        
        if (usuario == null)
        {
            return NotFound(); 
        }

        _context.Usuario.Remove(usuario); 
        await _context.SaveChangesAsync(); 

        // Caso o usuário logado seja o mesmo que está sendo excluído, desloga
        if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value == id.ToString())
        {
            await HttpContext.SignOutAsync(); 
        }

        TempData["SuccessMessage"] = "Usuário excluído com sucesso."; 
        return RedirectToAction("MensagemExclusao", "Usuario"); 
    }

    [HttpGet("MensagemExclusao")]
    [ApiExplorerSettings(IgnoreApi = true)] 
    public IActionResult MensagemExclusao()
    {
        return View(); 
    }

    // Ajustar perfil do usuário pela gestão do BI
    [HttpGet("UpdatePerfil")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> UpdatePerfil()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
        {
            return RedirectToAction("Error");
        }

        var usuario = await _context.Usuario.FindAsync(userId);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    [HttpPost("UpdatePerfil")]
    [SwaggerOperation(Summary = "Atualiza as informações de um usuário pelo administrador", Description = "Atualiza os dados do usuário com base no ID.")]
    [ProducesResponseType(typeof(User), 200)]
    [ProducesResponseType(400)]  
    [ProducesResponseType(404)] 
    public async Task<IActionResult> UpdatePerfil(User usuario)
    {
        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
        {
            return RedirectToAction("Error");
        }

        var usuarioExistente = await _context.Usuario.FindAsync(userId);
        if (usuarioExistente == null)
        {
            return NotFound();
        }

        // Atualiza apenas os campos que não são nulos ou vazios
        if (!string.IsNullOrEmpty(usuario.Nome))
        {
            usuarioExistente.Nome = usuario.Nome;
        }

        if (!string.IsNullOrEmpty(usuario.Email))
        {
            usuarioExistente.Email = usuario.Email;
        }

        if (!string.IsNullOrEmpty(usuario.Area))
        {
            usuarioExistente.Area = usuario.Area;
        }

        if (!string.IsNullOrEmpty(usuario.Perfil))
        {
            usuarioExistente.Perfil = usuario.Perfil;
        }

        // Só altera a senha se o campo de senha estiver preenchido
        if (!string.IsNullOrEmpty(usuario.Senha))
        {
            usuarioExistente.Senha = usuario.Senha;
        }

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Usuário atualizado com sucesso!";

        return View(usuarioExistente);
    }



}
