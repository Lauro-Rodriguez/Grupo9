using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models
{
    public class Periodos
    {
        //public Periodos()
        //{
        //    this.MNU_ROLES = new HashSet<Mnu_Roles>();
        //}

        [Key]
        [Column(Order = 0)]
        [Display(Name = "Institución")]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Descripción")]
        public string PERI_DESCRIPCION { get; set; }


        [Display(Name = "Activo")]
        public bool ACTIVO { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FECHA_CREA { get; set; }

        [Display(Name = "Cerrado")]
        public bool CERRADO { get; set; }

        public DateTime? FECHA_APROB { get; set; } //Fecha de aprobación del presupuesto

        [StringLength(50)]
        [MaxLength(50)]
        public string NUMDOC_APROB { get; set; } //Número de documento de aprobación del presupuesto

        public virtual Instituciones INSITUCIONES { get; set; }

        //Tablas Hijos
        public virtual ICollection<Mnu_Roles> MNU_ROLES { get; set; }
        public virtual ICollection<Models.Contabilidad.CO_PARAMETROS> CO_PARAMETROS { get; set; }
        public virtual ICollection<Models.POA.POA_PLAN> POA_PLAN { get; set; }
        public virtual ICollection<Models.Presupuesto.PR_FONDOS> PR_FONDOS { get; set; }
        public virtual ICollection<Models.Presupuesto.PR_RELACONTAB> PR_RELACONTAB { get; set; }
        public virtual ICollection<Models.Contabilidad.CO_RELACION> CO_RELACION { get; set; }
        public virtual ICollection<Presupuesto.PR_SOLICDISPPRESUP> PR_SOLICDISPPRESUP { get; set; }
        public virtual ICollection<FIRMAS> FIRMAS { get; set; }
        public virtual ICollection<Models.Contabilidad.CO_ASIENTOSCONT> CO_ASIENTOSCONT { get; set; }
        public virtual ICollection<Models.Contabilidad.CO_CLASIFRETEN> CO_CLASIFRETEN { get; set; }
        public virtual ICollection<Models.Contabilidad.CO_CUENBANCOS> CO_CUENBANCOS { get; set; }
        public virtual ICollection<Models.Contabilidad.CO_LINEACREDITO> CO_LINEACREDITO { get; set; }
        public virtual ICollection<Models.Inventarios.IN_PARAM> IN_PARAM { get; set; }
        public virtual ICollection<Models.Inventarios.IN_COMPRAS> IN_COMPRAS { get; set; }
        public virtual ICollection<Models.Inventarios.IN_BODEGAS> IN_BODEGAS { get; set; }
        public virtual ICollection<Models.Inventarios.IN_CLASES> IN_CLASES { get; set; }
        public virtual ICollection<Models.Inventarios.IN_LINEANEGOCIO> IN_LINEANEGOCIO { get; set; }
        public virtual ICollection<Models.Inventarios.IN_TIPOSTRAN> IN_TIPOSTRAN { get; set; }
        public virtual ICollection<Models.Nomina.RP_SECUENCIAS> RP_SECUENCIAS { get; set; }
        public virtual ICollection<Models.Facturacion.FC_TIPOSERVICIO> FC_TIPOSERVICIO { get; set; }
        public virtual ICollection<Models.Facturacion.FC_CAJAS> FC_CAJAS { get; set; }
        public virtual ICollection<Models.Facturacion.FC_CIERRES> FC_CIERRES { get; set; }
        public virtual ICollection<Models.Garantias.GA_GARANTIAS> GA_GARANTIAS { get; set; }
        public virtual ICollection<Models.Garantias.GA_PARAMETROS> GA_PARAMETROS { get; set; }
        public virtual ICollection<Models.Garantias.GA_ASIENTO> GA_ASIENTO { get; set; }
        public virtual ICollection<Models.Garantias.GA_EMAILS> GA_EMAILS { get; set; }
        public virtual ICollection<Models.Contratacion.PO_CONTRATOS> PO_CONTRATOS { get; set; }
        public virtual ICollection<Models.Contabilidad.CO_FACTURAS> CO_FACTURAS { get; set; }
        public virtual ICollection<Models.POA.POA_CPTOEJECUCION> POA_CPTOEJECUCION { get; set; }
        public virtual ICollection<Models.DOCUMSPDFS> DOCUMSPDFS { get; set; }
        public virtual ICollection<Models.Inventarios.IN_UNIOPE> IN_UNIOPE { get; set; }
    }
}