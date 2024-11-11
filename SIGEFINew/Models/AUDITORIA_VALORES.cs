using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models
{
    public class AUDITORIA_VALORES
    {
        [Key]
        [Column(Order = 0)]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(15)]
        public string USUA_LOGIN { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime FECHA { get; set; }

        [Required]
        [StringLength(100)]
        public string ACCION { get; set; }

        [Required]
        [StringLength(100)]
        public string OBJETO { get; set; }

        [Required]
        [StringLength(100)]
        public string TERMINAL { get; set; }

        [Required]
        [StringLength(100)]
        public string CAMPO { get; set; }

        [StringLength(50)]
        public string NEW_VALOR { get; set; }

        [StringLength(50)]
        public string OLD_VALOR { get; set; }
    }
}