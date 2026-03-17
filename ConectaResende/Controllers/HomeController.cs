using ConectaResende.Data;
using ConectaResende.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConectaResende.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(String tipo)
        {
            // 1. Iniciamos a consulta sem executar ainda (AsQueryable)
            var consulta = _context.Publicacoes.AsQueryable();

            // 2. Verificamos se algum filtro foi enviado pela Navbar
            if (!string.IsNullOrEmpty(tipo))
            {
                if (tipo == "acao")
                {
                    // Filtra especificamente pelo nome que está no seu banco de dados
                    consulta = consulta.Where(p => p.Tipo == "Ação Social");
                }
                else
                {
                    // Filtra por "Emprego" ou "Curso"
                    consulta = consulta.Where(p => p.Tipo == tipo);
                }
            }

            // 3. Ordenamos e transformamos em lista para enviar à View
            var publicacoes = consulta
                .OrderByDescending(p => p.DataPublicacao)
                .ToList();

            return View(publicacoes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        public IActionResult CentralAjuda() => View();
        public IActionResult TermosUso() => View();
        public IActionResult Privacidade() => View();

    }
}