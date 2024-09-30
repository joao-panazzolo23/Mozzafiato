using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Mozzafiato.Models
{
    [Table("CarrinhoCompraItens")]
    public class CarrinhoCompraItem
    {
        
        public int CarrinhoCompraItemId { get; set; }

        public Lanche Lanche { get; set; }
        public int Quantidade { get; set; }
        [StringLength(200)]
        public string CarrinhoCompraId { get; set; }

    }
}
