using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIGEFINew.Models
{
    public class Provincias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Provincias()
        {
            CANTONES = new HashSet<Cantones>();
        }

        [Key]
        //[Column(TypeName = "numeric")]
        public int ID_PROVINCIA { get; set; }

        [Required]
        [StringLength(50)]
        public string DESCRIPCION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cantones> CANTONES { get; set; }
    }
}