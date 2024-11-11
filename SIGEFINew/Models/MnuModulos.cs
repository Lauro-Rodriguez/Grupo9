using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models
{
    public class MnuModulos
    {
        public MnuModulos()
        {
            this.MNU_OPCIONES = new HashSet<Mnu_Opciones>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int COD_MODULO { get; set; }

        [Required]
        [StringLength(50)]
        public string NOM_MODULO { get; set; }

        public virtual ICollection<Mnu_Opciones> MNU_OPCIONES { get; set; }
    }
}