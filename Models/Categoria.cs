using System.ComponentModel.DataAnnotations;

namespace Mozzafiato.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        [StringLength(100, ErrorMessage = "O tamanho máximo de 100 caracteres foi excedido.")]
        [Required(ErrorMessage = "Informe o nome da categoria.")]
        [Display(Name = "Nome")] 
        public string CategoriaName { get; set; }
        [StringLength(200, ErrorMessage = "O tamanho máximo de 200 caracteres foi excedido.")]
        [Required(ErrorMessage = "Informe a descrição da categoria.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public List<Lanche> Lanches { get; set; }
    }
}
