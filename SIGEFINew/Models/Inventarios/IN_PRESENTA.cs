using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_PRESENTA
    {
        [Key]
        [Column(Order = 0)]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Key]
        [Required]
        [Column(Order = 2)]
        [Display(Name = "U. Medida")]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [StringLength(10)]
        public string COD_CAGEN { get; set; }

        [Key]
        [Column(Order = 3)]
        [Display(Name = "Código")]
        public int COD_PRESEN { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Descripción")]
        public string DESCRIP_CAGEN { get; set; }

        [StringLength(50)]
        [Required]
        public string USER_CREA { get; set; }

        [Required]
        public DateTime FECHA_CREA { get; set; }

        [StringLength(50)]
        public string USER_MODIF { get; set; }

        public DateTime FECHA_MODIF { get; set; }

        public virtual IN_CARACGEN IN_CARACGEN { get; set; }

        public virtual ICollection<Models.Inventarios.IN_ITEMS> IN_ITEMS { get; set; }
    }
}