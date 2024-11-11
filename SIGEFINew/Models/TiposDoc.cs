using System.ComponentModel.DataAnnotations;

namespace SIGEFINew.Models
{
    public class TiposDoc
    {
        [Key]
        [StringLength(15)]
        [Display(Name = "Código")]
        public string COD_TIPO_DOC { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Descripción")]
        public string DESCRIPCION { get; set; }

        [StringLength(2)]
        [Display(Name = "Tipo SRI")]
        public string TIPO_SRI { get; set; }

    }
}