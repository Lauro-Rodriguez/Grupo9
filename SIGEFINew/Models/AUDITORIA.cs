using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models
{
    public class AUDITORIA
    {
        [Key]
        [Column(Order = 0)]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Key]
        [Column(Order = 2)]
        public int MODULO { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(15)]
        public string USUA_LOGIN { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime FECHA { get; set; }

        [Required]
        [StringLength(100)]
        public string OPCION_MENU { get; set; }

        [Required]
        [StringLength(100)]
        public string ESTACION { get; set; }

        [Required]
        [StringLength(200)]
        public string PROCESO { get; set; }

        [Required]
        [StringLength(200)]
        public string ACCION { get; set; }
    }
}