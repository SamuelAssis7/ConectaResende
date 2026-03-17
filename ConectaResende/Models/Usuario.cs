using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaResende.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [NotMapped]
        [Required]
        public string Senha { get; set; }   // senha digitada

        public string TipoUsuario { get; set; }

        public string? SenhaHash { get; set; } // senha criptografada

        public DateTime DataCadastro { get; set; }
    }
}
