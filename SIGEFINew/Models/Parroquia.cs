using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models
{
    public class Parroquia
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

        [Required]
        [StringLength(50)]
        public string DESCRIPCION { get; set; }

        public virtual Cantones Cantones { get; set; }
        public virtual ICollection<Barrios> Barrios { get; set; }
        public virtual ICollection<Proveedores> PROVEEDORES { get; set; }
    }
}