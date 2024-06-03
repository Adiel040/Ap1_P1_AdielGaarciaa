using System.ComponentModel.DataAnnotations;

namespace Ap1_P1_AdielGaarciaa.Models
{
    public class Articulos
    {

        //Articulo id, Descripcion , costo , ganancia , precio

        [Key]
        public int ArticuloId { get; set; }
        [Required(ErrorMessage = "El campo Descripci&oacute;n es obligatorio ")]
        public string? Descripcion { get; set; }
        public decimal costo { get; set; }
        public decimal ganancia { get; set; }
        public decimal precio { get; set; }




    }
}
