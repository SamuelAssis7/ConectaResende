using ConectaResende.Data;
using ConectaResende.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ConectaResende.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================= REGISTRAR =================

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
                usuario.DataCadastro = DateTime.UtcNow;

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                TempData["ContaCriada"] = "Conta criada com sucesso!";
                return RedirectToAction("Login");
            }

            return View(usuario);
        }

        // ================= LOGIN =================

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);

            if (usuario != null && BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email)
                };

                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(1)
                });


                TempData["LoginSucesso"] = "Bem-vindo " + usuario.Nome + "!";

                // voltar para login para mostrar o SweetAlert
                return RedirectToAction("Login");
            }

            TempData["LoginErro"] = "Email ou senha inválidos.";

            return View();
        }

        // ================= LOGOUT =================

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            TempData.Clear(); // limpa mensagens antigas

            return RedirectToAction("Index", "Home");
        }
    }
}