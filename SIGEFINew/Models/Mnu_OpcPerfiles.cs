using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models
{
    public class Mnu_OpcPerfiles
    {
        [Key]
        [Column(Order = 0)]
        public int COD_MODULO { get; set; }

        [Key]
        [Column(Order = 1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int COD_PERFIL { get; set; }

        [Required]
        [StringLength(80)]
        [Display(Name = "Descripción")]
        public string DESC_PERFIL { get; set; }
    }
}