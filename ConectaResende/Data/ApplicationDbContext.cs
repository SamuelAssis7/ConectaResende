using ConectaResende.Models;
using Microsoft.EntityFrameworkCore;

namespace ConectaResende.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Oportunidade> Oportunidades { get; set; }
        public DbSet<Publicacao> Publicacoes { get; set; }
    }
}
