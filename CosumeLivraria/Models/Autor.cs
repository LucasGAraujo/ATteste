using System.ComponentModel.DataAnnotations;

namespace CosumeLivraria.Models
{
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        public string NomeAutor { get; set; }
        [Required]
        public string SobrenomeAutor { get; set; }
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}
