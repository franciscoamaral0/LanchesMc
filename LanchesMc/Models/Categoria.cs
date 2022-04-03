using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMc.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100, ErrorMessage ="O Tamanho máximo é de 100 caracteres.")]
        [Required(ErrorMessage ="Informe a categoria")]
        [Display(Name ="Nome")]

        public string CategoriaNome { get; set; }
        [StringLength(200, ErrorMessage = "O Tamanho máximo é de 200 caracteres.")]
        [Required(ErrorMessage = "Informe a descricao da categoria")]
        [Display(Name = "Descricao")]
        public string Descricao { get; set; }
        public List<Lanche> Lanches { get; set; }

    }
}
