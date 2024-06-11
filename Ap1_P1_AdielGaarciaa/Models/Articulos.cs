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
        [Required(ErrorMessage = "El campo Costo es obligatorio ")]

        public decimal Costo { get; set; }
        [Required(ErrorMessage = "El campo Ganancia es obligatorio ")]

        public decimal Ganancia { get; set; }
        [Required(ErrorMessage = "El campo Precio es obligatorio ")]

        public decimal Precio { get; set; }




    }
}
