using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BIProject.Models;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;

namespace BIProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Logar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SwaggerOperation(Summary = "Autentica um usuário cadastrado", Description = "Avalia se o usuário possui conta e qual o perfil, os dados estando corretos, realiza o Login deixa visível as demais telas.")]
        [ProducesResponseType(typeof(User), 201)] 
        [ProducesResponseType(400)]
        public async Task<IActionResult> Logar(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Usuario
                    .FirstOrDefaultAsync(c => c.Email == model.Email && c.Senha == model.Senha);

                if (user != null && user.Nome != null && user.Email != null)
                {

                    // Adiciona o registro do login no histórico
                    var loginHistory = new LoginHistory
                    {
                        IdUsuario = user.Id,
                        Email = user.Email,
                        DataHora = DateTime.UtcNow,

                    };

                    _context.Login.Add(loginHistory);
                    await _context.SaveChangesAsync();

                    if (string.IsNullOrEmpty(user.Perfil))
                    {
                        user.Perfil = "Comum";
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Nome),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("Perfil", user.Perfil)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Hero", "Home");
                }
                else
                {
                    return RedirectToAction("MensagemErro");
                }
            }
            return View(model);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult MensagemErro()
        {
            return View();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Logar", "Login");
        }
    }
}
