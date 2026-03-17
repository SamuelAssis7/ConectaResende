using ConectaResende.Data;
using ConectaResende.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConectaResende.Controllers
{
    public class OportunidadesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OportunidadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Criar()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(Publicacao publicacao)
        {
            if (!ModelState.IsValid)
            {
                return View(publicacao);
            }

            publicacao.DataPublicacao = DateTime.UtcNow;

            _context.Publicacoes.Add(publicacao);
            _context.SaveChanges();

            TempData["MensagemSucesso"] = "Publicação criada com sucesso!";

            return RedirectToAction("Index", "Home");
        }
    }
}
