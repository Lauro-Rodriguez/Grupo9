using SIGEFINew.Models.Presupuesto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_UNIOPE
    {
        [Key]
        [Column(Order = 0)]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 2)]
        [Display(Name = "Código")]
        public int ID_UNIDOPERATIVA { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Descripción")]
        public string DESCRIPCION { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        public int ORG_CODIGO { get; set; }

        public virtual Periodos Periodos { get; set; }
        public virtual OrganicoF OrganicoF { get; set; }
    }
}