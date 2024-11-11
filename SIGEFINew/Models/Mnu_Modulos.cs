using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models
{
    public class Mnu_Modulos
    {
        //public Mnu_Modulos()
        //{
        //    this.MNU_OPCIONES  = new HashSet<Mnu_Opciones>();
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int COD_MODULO { get; set; }

        [StringLength(50)]
        public string NOM_MODULO { get; set; }

        //public virtual ICollection<Mnu_Opciones> MNU_OPCIONES { get; set; }
    }
}