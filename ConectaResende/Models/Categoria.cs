using System.ComponentModel.DataAnnotations;

namespace ConectaResende.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public ICollection<Oportunidade> Oportunidades { get; set; }
    }
}
