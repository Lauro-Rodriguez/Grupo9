using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_CARGA_INI
    {
        [Key]
        [Column(Order = 0)]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ID_BODEGA { get; set; }

        [StringLength(50)]
        [Key]
        [Column(Order = 3)]
        public string COD_ITEM { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NUM_FILA { get; set; }

        [Required]
        [Display(Name = "Cantidad")]
        public float CANTIDAD { get; set; }

        [Required]
        [Display(Name = "Costo")]
        public float COSTO { get; set; }

        [Required]
        [Display(Name = "Valor Total")]
        public float VALOR_TOTAL { get; set; }

        [Required]
        [Display(Name = "Activar")]
        public bool ACTIVADO { get; set; }

        [StringLength(50)]
        [Required]
        public string USER_CREA { get; set; }

        [Required]
        public DateTime FECHA_CREA { get; set; }

        [StringLength(50)]
        public string USER_MODIF { get; set; }

        public DateTime FECHA_MODIF { get; set; }

        public virtual IN_ITEMS IN_ITEMS { get; set; }
    }
}