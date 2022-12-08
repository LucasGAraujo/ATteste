using System.ComponentModel.DataAnnotations;

namespace ConsumeApi.Models
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
        public ICollection<Autor> Autor { get; set; }
    }
}

