using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConectaResende.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        public IActionResult Index()
        {
            var nome = User.Identity.Name;
            var email = User.FindFirstValue(ClaimTypes.Email);

            ViewBag.Nome = nome;
            ViewBag.Email = email;

            return View();
        }
    }
}
