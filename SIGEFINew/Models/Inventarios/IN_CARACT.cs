using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_CARACT
    {

        [Key]
        [Column(Order = 0)]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [StringLength(10)]
        [Key]
        [Column(Order = 2)]
        public string COD_CAGEN { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Descripción")]
        public string DESCRIP_CAGEN { get; set; }

        [StringLength(5)]
        [Required]
        [Display(Name = "Característica")]
        public string CARACT { get; set; }

        [StringLength(50)]
        [Required]
        public string USER_CREA { get; set; }

        [Required]
        public DateTime FECHA_CREA { get; set; }

        [StringLength(50)]
        public string USER_MODIF { get; set; }

        public DateTime FECHA_MODIF { get; set; }

        public virtual Periodos Periodos { get; set; }
        public virtual ICollection<Models.Inventarios.INV_PRESENT> INV_PRESENT { get; set; }
        public virtual ICollection<Models.Inventarios.IN_ITEMS> IN_ITEMS { get; set; }
    }
}