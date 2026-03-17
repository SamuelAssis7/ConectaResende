using ConectaResende.Data;
using ConectaResende.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConectaResende.Controllers
{
    public class PublicacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PublicacoesController(ApplicationDbContext context)
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
            var emailUsuario = User.FindFirstValue(ClaimTypes.Email)
                   ?? User.Identity.Name;
            publicacao.EmailUsuario = emailUsuario;


            publicacao.EmailUsuario = emailUsuario;

            // remover validação do campo automático
            ModelState.Remove("EmailUsuario");

            if (ModelState.IsValid)
            {
                publicacao.DataPublicacao = DateTime.UtcNow;

                _context.Publicacoes.Add(publicacao);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Publicação criada com sucesso!";

                return RedirectToAction("Index", "Home");
            }

            return View(publicacao);
        }

        public IActionResult Detalhes(int id)
        {
            var publicacao = _context.Publicacoes
                .FirstOrDefault(p => p.Id == id);

            if (publicacao == null)
            {
                return NotFound();
            }

            return View(publicacao);
        }

        public IActionResult Minhas()
        {
            var emailUsuario = User.FindFirstValue(ClaimTypes.Email);

            var minhasPublicacoes = _context.Publicacoes
                .Where(p => p.EmailUsuario == emailUsuario)
                .OrderByDescending(p => p.DataPublicacao)
                .ToList();

            return View(minhasPublicacoes);
        }

        [Authorize]
        public IActionResult Editar(int id)
        {
            var publicacao = _context.Publicacoes.FirstOrDefault(p => p.Id == id);

            if (publicacao == null)
                return NotFound();

            return View(publicacao);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Publicacao publicacao)
        {
            if (ModelState.IsValid)
            {
                publicacao.DataPublicacao = DateTime.UtcNow;

                _context.Publicacoes.Update(publicacao);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Publicação atualizada com sucesso!";
                return RedirectToAction("Minhas");
            }

            return View(publicacao);
        }

        [Authorize]
        public IActionResult Excluir(int id)
        {
            var publicacao = _context.Publicacoes.FirstOrDefault(p => p.Id == id);

            if (publicacao == null)
                return NotFound();

            return View(publicacao);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ExcluirConfirmado(int id)
        {
            var publicacao = _context.Publicacoes.Find(id);

            if (publicacao != null)
            {
                _context.Publicacoes.Remove(publicacao);
                _context.SaveChanges();
            }

            TempData["MensagemSucesso"] = "Publicação excluída com sucesso!";

            return RedirectToAction("Minhas");
        }

        public IActionResult Index(string tipo)
        {
            var publicacoes = _context.Publicacoes
        .OrderByDescending(p => p.DataPublicacao)
        .AsQueryable();

            if (!string.IsNullOrEmpty(tipo))
            {
                if (tipo == "acao")
                {
                    publicacoes = publicacoes
                        .Where(p => p.Tipo == "Ação Social");
                }
                else
                {
                    publicacoes = publicacoes
                        .Where(p => p.Tipo == tipo);
                }
            }

            return View(publicacoes.ToList());
        }

    }
}
