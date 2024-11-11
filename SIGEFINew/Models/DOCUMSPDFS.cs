using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace SIGEFINew.Models
{
    public class DOCUMSPDFS
    {
        [Key]
        [Column(Order = 0)]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        [Display(Name = "Código")]
        public string CODIGO { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NUM_FILA { get; set; }

        [Required]
        [StringLength(50)]
        public string MODULO { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Path")]
        public string PATH { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Archivo")]
        public string NOM_ARCHIVO { get; set; }

        // PROPIEDADES PRIVADAS
        public string PathRelativo
        {
            get
            {
                return ConfigurationManager.AppSettings["PathArchivos"] +
                                            this.CODIGO.ToString() + "." +
                                            this.MODULO;
            }
        }


        // MÉTODOS PÚBLICOS
        public string PathCompleto
        {
            get
            {
                var _PathAplicacion = HttpContext.Current.Request.PhysicalApplicationPath;
                return Path.Combine(_PathAplicacion, this.PathRelativo);
            }
        }

        public void SubirArchivo(byte[] archivo)
        {
            File.WriteAllBytes(this.PathCompleto, archivo);
        }

        public virtual Periodos Periodos { get; set; }
    }
}