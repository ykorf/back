// FuncionarioController.cs permanece igual

// ContratoController.cs permanece igual

// AutenticacaoController.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

public class AutenticacaoController : Controller
{
    private readonly SeuDbContext _context;

    public AutenticacaoController(SeuDbContext context)
    {
        _context = context;
    }

    // Página de Login
    public IActionResult Login()
    {
        return View();
    }

    // Ação de Login
    [HttpPost]
    public async Task<IActionResult> Login(string nomeUsuario, string senha)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NomeUsuario == nomeUsuario && u.Senha == senha);

        if (usuario != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NomeUsuario),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
                // Adicione outras claims conforme necessário
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home"); // Redirecione para a página principal após o login
        }

        ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos");
        return View();
    }

    // Ação de Logout
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login");
    }
}
