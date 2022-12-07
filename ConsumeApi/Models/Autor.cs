using System.ComponentModel.DataAnnotations;

namespace ConsumeApi.Models
{
    public class Autor
    {

        public int Id { get; set; }
        [Required]
        public string NomeAutor { get; set; } = null!;
        [Required]
        public string SobrenomeAutor { get; set; }= null!;
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}
