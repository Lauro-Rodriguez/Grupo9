using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_TIPOS
    {
        [Key]
        [Column(Order = 0)]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ID_CLASE { get; set; }

        [Key]
        [Column(Order = 3)]
        [Display(Name = "Código")]
        public int ID_TIPO { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Descripción")]
        public string NOM_TIPO { get; set; }

        public virtual IN_CLASES IN_CLASES { get; set; }
        public virtual ICollection<IN_SUBTIPOS> IN_SUBTIPOS { get; set; }
        public virtual ICollection<IN_ITEMS> IN_ITEMS { get; set; }

    }
}