using System.ComponentModel.DataAnnotations;

namespace LivratiaAt.Models
{
    public class Livros
    {
        
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public int Ano { get; set; }
        [Required]
        public string Isbn { get; set; }
    }
}
