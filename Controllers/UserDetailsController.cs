using BIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Route("UserDetails")]
public class UserDetailsController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserDetailsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet("Consult")]
    public async Task<IActionResult> Consult()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (int.TryParse(userIdString, out var userId))
        {
            // Busca o usuário pelo ID
            var user = await _context.Usuario.FirstOrDefaultAsync(c => c.Id == userId);
            if (user == null)
            {
                return RedirectToAction("Error");
            }

        
            var userDetails = new UserDetails
            {
                User = user,
           
            };

            return View(userDetails);
        }

        return RedirectToAction("Error");
    }
}
