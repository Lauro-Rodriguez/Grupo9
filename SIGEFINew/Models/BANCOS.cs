using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models
{
    public enum TipoInst
    {
        Banco, Cooperativa, Mutualista
    }

    public enum Condicion
    {
        Estado, Privado
    }
    public class BANCOS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Banco")]
        public int CODIGO_BANCO { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Nombre")]
        public string DESCRIPCION { get; set; }

        [Required]
        [Display(Name = "Cuenta B.C.E.")]
        public int CODIGO_ENVIO { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public TipoInst? TIPO { get; set; }

        [Required]
        [Display(Name = "Tipo Banca")]
        public Condicion? CONDICION { get; set; }

        public virtual ICollection<Proveedores> Proveedores { get; set; }
        public virtual ICollection<Models.Contabilidad.CO_CUENBANCOS> CO_CUENBANCOS { get; set; }

    }
}