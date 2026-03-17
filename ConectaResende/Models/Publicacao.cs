using System.ComponentModel.DataAnnotations;

namespace ConectaResende.Models
{
    public class Publicacao
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }

        public string Bairro { get; set; }

        public string Tipo { get; set; }

        public DateTime DataPublicacao { get; set; } = DateTime.UtcNow;
        public string EmailUsuario { get; set; }
    }
}
