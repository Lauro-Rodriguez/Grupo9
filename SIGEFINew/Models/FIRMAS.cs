using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models
{
    public class FIRMAS
    {
        [Key]
        [Column(Order = 0)]
        //[Display(Name = "Institución")]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(15)]
        public string USUA_FIRMA { get; set; }

        [Key]
        [Column(Order = 3)]
        public int CODIGO_DOC { get; set; }

        [Key]
        [Column(Order = 4)]
        public int NUM_FILA { get; set; }

        [Required]
        [StringLength(50)]
        public string NOMBRE { get; set; }

        [Required]
        [StringLength(50)]
        public string CARGO { get; set; }

        [StringLength(10)]
        public string TIPO { get; set; }

        public virtual Periodos Periodos { get; set; }
        public virtual Documentos Documentos { get; set; }
    }
}