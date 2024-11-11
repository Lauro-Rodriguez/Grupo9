using SIGEFINew.Models.Tramites;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIGEFINew.Models
{
    public class Documentos
    {
        [Key]
        public int CODIGO_DOC { get; set; }

        [Required]
        [StringLength(200)]
        public string NOMBRE_DOC { get; set; }

        public virtual ICollection<FIRMAS> FIRMAS { get; set; }
        public virtual ICollection<TR_HISTORIAL> TR_HISTORIAL { get; set; }
        public virtual ICollection<POA.POA_PYHISTORIAL> POA_PYHISTORIAL { get; set; }
    }
}