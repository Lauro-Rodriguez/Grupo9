using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public enum TIPOUSO
    {
        CONSUMO, INVERSION, BIENES
    }
    public class IN_CLASES
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
        public int ID_CLASE { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Descripción")]
        public string DESRIPCION { get; set; }

        [Required]
        [Display(Name = "Tipo Uso")]
        public TIPOUSO? TIPO_USO { get; set; } //0=CONSUMO,1=INVERSION

        public virtual Periodos Periodos { get; set; }
        public virtual ICollection<IN_TIPOS> IN_TIPOS { get; set; }
        public virtual ICollection<IN_ITEMS> IN_ITEMS { get; set; }
    }
}