using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models
{
    public class Nacionalidades
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_NACIONALIDAD { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Descripción")]
        public string DESCRIPCION { get; set; }

        public virtual ICollection<Nomina.RP_EMPLEADOS> RP_EMPLEADOS { get; set; }
    }
}