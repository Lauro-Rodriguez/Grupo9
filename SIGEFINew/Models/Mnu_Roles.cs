using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SIGEFINew.Models
{
    [Authorize]
    public class Mnu_Roles
    {
        [Key]
        //[Column(TypeName = "numeric")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        [Display(Name = "Institución")]
        [Required]
        public int ID_INSTITUCION { get; set; }

        //[Key]
        //[Column(Order = 1)]
        //[Display(Name = "Periodo")]
        //[Required]
        //public int PERI_CODIGO { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        [Display(Name = "Código Rol")]
        public int CODIGO_ROL { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Nombre del Rol")]
        public string NOMBRE_ROL { get; set; }

        [Required]
        [StringLength(15)]
        public string USUA_CREA { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FECHA_CREA { get; set; }

        [StringLength(15)]
        public string USUA_MODIF { get; set; }

        [Column(TypeName = "date")]
        public DateTime FECHA_MODIF { get; set; }

        public virtual Periodos PERIODOS { get; set; }

    }
}