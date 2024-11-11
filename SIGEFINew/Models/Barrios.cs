using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models
{
    public class Barrios
    {
        [Key]
        [Column(Order = 0)]
        public int ID_PROVINCIA { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ID_CANTON { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ID_PARROQUIA { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_BARRIO { get; set; }

        [Required]
        [StringLength(250)]
        public string DESCRIPCION { get; set; }

        public virtual Parroquia Parroquia { get; set; }
    }
}