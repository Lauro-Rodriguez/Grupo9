using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models
{
    public class Cantones
    {
        [Key]
        [Column(Order = 0)]
        public int ID_PROVINCIA { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ID_CANTON { get; set; }

        [Required]
        [StringLength(50)]
        public string DESCRIPCION { get; set; }

        public virtual Provincias PROVINCIAS { get; set; }
        public virtual ICollection<Parroquia> Parroquias { get; set; }
    }
}