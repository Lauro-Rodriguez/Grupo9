using System.ComponentModel.DataAnnotations;

namespace SIGEFINew.Models
{
    public class Nomenclatura_Docs
    {
        [Key]
        public int CODIGO_DOC { get; set; }

        [Required]
        [StringLength(200)]
        public string DESCRIPCION { get; set; }

    }
}