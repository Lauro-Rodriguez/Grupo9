using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_LINEANEGOCIO
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

        [Required]
        [Display(Name = "Descripción")]
        [StringLength(100)]
        public string DESC_LINEA { get; set; }

        public virtual Periodos Periodos { get; set; }
        public virtual ICollection<Models.Inventarios.IN_SUBLINEANEGOCIO> IN_SUBLINEANEGOCIO { get; set; }
    }
}