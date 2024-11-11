using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models
{
    public class Mnu_Opciones
    {
        [Key]
        [Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int COD_MODULO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string COD_MENU { get; set; }

        [StringLength(80)]
        [Display(Name = "Descripción")]
        public string DESC_MENU { get; set; }
        public int MAIN_NIVEL { get; set; }

        [StringLength(10)]
        public string NOM_MENU { get; set; }
        public int SUB_NIVEL { get; set; }

        [StringLength(30)]
        public string EVENTO { get; set; }

        [StringLength(30)]
        public string CONTROLADOR { get; set; }

        public virtual Mnu_Modulos MNU_MODULOS { get; set; }
    }
}