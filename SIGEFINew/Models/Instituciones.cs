using SIGEFINew.Models.Activos_Fijos;
using SIGEFINew.Models.Garantias;
using SIGEFINew.Models.Nomina;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
namespace SIGEFINew.Models
{
    [Authorize]
    public class Instituciones
    {
        public Instituciones()
        {
            this.PERIODOS = new HashSet<Periodos>();
        }

        [Key]
        //[Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Código")]
        public int ID_INSTITUCION { get; set; }

        //[Column(TypeName = "numeric")]
        [Display(Name = "Tipo Presupuesto")]
        [Required]
        public int TIPO_PRESUP { get; set; }

        //[Column(TypeName = "numeric")]
        [Display(Name = "Institución")]
        [Required]
        public int COD_INSTITUCION { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string NOM_INSTITUCION { get; set; }

        [Required]
        [StringLength(13)]
        [MinLength(13)]
        [Display(Name = "R.U.C.")]
        public string RUC_INSTITUCION { get; set; }

        [StringLength(100)]
        [Display(Name = "EMail")]
        [EmailAddress]
        public string EMAIL { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Representante")]
        public string TIPO_DOCREPLEGAL { get; set; }

        [Required]
        [StringLength(13)]
        [Display(Name = "Num. Documento")]
        [MinLength(10)]
        public string NUMDOC_REPLEGAL { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre Rep. Legal")]
        public string NOM_REPLEGAL { get; set; }

        [StringLength(13)]
        [Display(Name = "Contador")]
        [MinLength(13)]
        public string RUC_CONTADOR { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre Contador")]
        public string NOM_CONTADOR { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Dirección")]
        public string DIRECCION { get; set; }

        [Required]
        [StringLength(9)]
        [Display(Name = "Teléfono")]
        public string TELEFONO { get; set; }

        [StringLength(9)]
        [Display(Name = "Fax")]
        public string FAX { get; set; }

        //[Column(TypeName = "numeric")]
        public decimal ID_BASE { get; set; }

        [Required]
        [StringLength(30)]
        public string SERVER { get; set; }

        [Required]
        [StringLength(30)]
        public string CATALOGO { get; set; }

        //[Column(TypeName = "image")]
        //[Display(Name = "Logo")]
        //public byte[] LOGO { get; set; }

        //[Column(TypeName = "numeric")]
        public decimal TIPOAMB { get; set; }

        [Required]
        [StringLength(2)]
        public string OBLIGACONTAB { get; set; }

        //[Column(TypeName = "numeric")]
        public decimal NUMRESOL { get; set; }

        [StringLength(150)]
        public string EMAILPASSWORD { get; set; }

        [StringLength(150)]
        public string SMTPHOST { get; set; }

        //[Column(TypeName = "numeric")]
        public decimal SMTPPORT { get; set; }

        [StringLength(250)]
        public string DIRFIRMADIGITAL { get; set; }

        [StringLength(100)]
        public string PASSWFIRMADIGITAL { get; set; }

        [StringLength(250)]
        public string DIRSRI { get; set; }

        //[Column(TypeName = "numeric")]
        [Display(Name = "Provincia")]
        public decimal ID_PROVINCIA { get; set; }

        //[Column(TypeName = "numeric")]
        [Display(Name = "Cantón")]
        public decimal ID_CANTON { get; set; }

        [Column(TypeName = "date")]
        public DateTime FECHA_INICIO { get; set; }

        [Display(Name = "Cont. Esp.")]
        public bool CONTRIB_ESP { get; set; }

        [Display(Name = "Dato Seguro")]
        public bool DATO_SEGURO { get; set; }

        //Tablas Hijas
        public virtual ICollection<Periodos> PERIODOS { get; set; }
        public virtual ICollection<Presupuesto.Programa> Programa { get; set; }
        public virtual ICollection<Nomina.RP_CARGOS> RP_CARGOS { get; set; }
        public virtual ICollection<Nomina.RP_TIPOROL> RP_TIPOROL { get; set; }
        public virtual ICollection<POA.POA_TIPOPROYECTOS> POA_TIPOPROYECTOS { get; set; }
        public virtual ICollection<Nomina.RP_TIPOSANTICIPO> RP_TIPOSANTICIPO { get; set; }
        public virtual ICollection<Nomina.RP_SETUP> RP_SETUP { get; set; }
        public virtual ICollection<Nomina.RP_VARIABLES> RP_VARIABLES { get; set; }
        public virtual ICollection<Nomina.RP_TIRENTA> RP_TIRENTA { get; set; }
        public virtual ICollection<Nomina.RP_TGTOSPERS> RP_TGTOSPERS { get; set; }
        public virtual ICollection<AF_PARAMETROS> AF_PARAMETROS { get; set; }
        public virtual ICollection<AF_TIPOSTRAN> AF_TIPOSTRAN { get; set; }
        public virtual ICollection<AF_TIPOS> AF_TIPOS { get; set; }
        public virtual ICollection<AF_ACTAENT> AF_ACTAENT { get; set; }
        public virtual ICollection<AF_CUENTASCONT> AF_CUENTASCONT { get; set; }
        public virtual ICollection<AF_DEVOLUCIONES> AF_DEVOLUCIONES { get; set; }
        public virtual ICollection<AF_TRASPASOS> AF_TRASPASOS { get; set; }
        public virtual ICollection<AF_MOTIVOSBAJA> AF_MOTIVOSBAJA { get; set; }
        public virtual ICollection<AF_TABLADEPRE> AF_TABLADEPRE { get; set; }
        public virtual ICollection<AF_TIPOSMEF> AF_TIPOSMEF { get; set; }
        public virtual ICollection<GA_CONCEPTOSGARANTIA> GA_CONCEPTOSGARANTIA { get; set; }
        public virtual ICollection<GA_EMISORES> GA_EMISORES { get; set; }
        public virtual ICollection<GA_TIPOSGARANTIA> GA_TIPOSGARANTIA { get; set; }
        public virtual ICollection<RP_GRUPOCUPACIONAL> RP_GRUPOCUPACIONAL { get; set; }
    }
}