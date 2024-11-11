using System.ComponentModel.DataAnnotations;

namespace SIGEFINew.Models
{
    public class TiposSRI
    {
        [Key]
        [StringLength(2)]
        [Display(Name = "Código")]
        public string CODIGO { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Código")]
        public string DESCRIPCION { get; set; }
    }
}