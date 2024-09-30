using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mozzafiato.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        [StringLength(100)]
        [Display(Name = "Endereço")]
        public string Endereco2 { get; set; }
        [Required(ErrorMessage = "Informe o seu CEP")]
        [Display(Name = "CEP")]
        [StringLength(10, MinimumLength = 8)]
        public string Cep { get; set; }
        [StringLength(50)]
        public string Cidade { get; set; }
        [StringLength(50)]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Informe o seu telefone")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+?[1-9]\d{1,14}([-\s]?\d{1,13})*$", ErrorMessage = "Número de telefone inválido.")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Informe o seu telefone")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }
        [ScaffoldColumn(false)]
        [Column(TypeName = "Decimal(18,2)")]
        [Display(Name = "Total do pedido")]
        public decimal PedidoTotal { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Itens do Pedido")]
        public int TotalItensPedido { get; set; }

        [Display(Name = "Data Envio Pedido")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]

        public DateTime PedidoEnviado { get; set; }
        public DateTime? PedidoEntregueEm { get; set; }

        public List<PedidoDetalhe> PedidoItens { get; set; }

    }
}
