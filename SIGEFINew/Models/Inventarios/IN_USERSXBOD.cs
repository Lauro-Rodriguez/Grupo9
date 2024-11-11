using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_USERSXBOD
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
        public int ID_BODEGA { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string USUA_LOGIN { get; set; }

        [Display(Name = "Responsable")]
        public bool RESP { get; set; }

        [Display(Name = "Desde")]
        public DateTime FECHA_DESDE { get; set; }

        [Display(Name = "Hasta")]
        public DateTime FECHA_HASTA { get; set; }

        public virtual IN_BODEGAS IN_BODEGAS { get; set; }
    }
}

