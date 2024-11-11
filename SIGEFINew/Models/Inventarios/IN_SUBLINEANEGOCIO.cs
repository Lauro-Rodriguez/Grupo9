using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_SUBLINEANEGOCIO
    {
        [Key]
        [Column(Order = 0)]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Key]
        [Column(Order = 2)]
        [Display(Name = "Código")]
        public int COD_LINEA { get; set; }

        [Key]
        [Column(Order = 3)]
        [Display(Name = "Código")]
        public int COD_SUBLINEA { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        [StringLength(100)]
        public string DESC_SUBLINEA { get; set; }

        public virtual IN_LINEANEGOCIO IN_LINEANEGOCIO { get; set; }
    }
}