using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SIGEFINew.Models
{
    public enum TipoIdenti
    {
        RUC, Cédula, Pasaporte, Catastro
    }

    public enum Sexo
    {
        M, F
    }

    public enum TipoProv
    {
        Proveedor, Funcionario, Contratista
    }
    public class Proveedores
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Código")]
        public string COD_PROV { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "N° Identif.")]
        public string NUMCEDRUC_PROV { get; set; }

        [Required]
        [Display(Name = "N°")]
        public TipoIdenti? TIPO_IDENTI { get; set; }

        [Required]
        [Display(Name = "Tipo Proveedor")]
        public TipoProv? TIPO_PROV { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(250)]
        [Display(Name = "Dirección")]
        public string DIRECCION { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Teléfono")]
        public string TELEFONO { get; set; }

        [Display(Name = "Emvio")]
        public int CODIGO_ENVIO { get; set; }

        [Required]
        [Display(Name = "Banco")]
        public int CODIGO_BANCO { get; set; }

        [Required]
        [StringLength(18)]
        [Display(Name = "N°")]
        public string NUM_CUENTA { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public int TIPO_CUENTA { get; set; }

        [Required]
        [Display(Name = "N°")]
        public TipoIdenti? TIPO_IDENTITRAN { get; set; } //TIPO DE IDENTIFICACION PARA SPI

        [Required]
        [StringLength(13)]
        [Display(Name = " ")]
        public string RUCCEDU_TRAN { get; set; } //RUC/CEDULA PARA SPI

        [Required]
        [StringLength(150)]
        [Display(Name = "R. Social")]
        public string RAZONSOCIAL { get; set; } //RAZON SOCIAL

        [StringLength(50)]
        [Display(Name = "C X C")]
        public string CXC_PROVEED { get; set; } //CUENTA POR COBRAR

        [StringLength(50)]
        [Display(Name = "CXC A.R.")]
        public string CXC_ANTICIPREC { get; set; } //CUENTA POR COBRAR

        [StringLength(50)]
        [Display(Name = "C X P")]
        public string CXP_PROVEED { get; set; } //CUENTA POR PAGAR

        [StringLength(50)]
        [Display(Name = "CXP A.E.")]
        public string CXP_ANTICIPENT { get; set; } //CUENTA POR PAGAR

        [Required]
        [Display(Name = "0% Ret.")]
        public bool CONTRIBESPECIAL { get; set; } //CONTRIBUYENTE ESPECIAL - SE USA PARA CONTRIBUYENTE 0 RTENCIONES

        [Required]
        [Display(Name = "Obliga Contab.")]
        public bool OBLIGA_CONTAB { get; set; } //OBLIGADO A LLEVAR CONTABILIDAD

        [Required]
        [Display(Name = "Tipo")]
        public int INSTIT_PUB { get; set; } //Tipo 1=Instit. Pubica,2=Persona Juridica, 3=Persona Natural

        [Required]
        [Display(Name = "Sexo")]
        public Sexo? SEXO { get; set; } //SEXO

        [Required]
        [StringLength(50)]
        [Display(Name = "E.Mail")]
        [EmailAddress]
        public string EMAIL { get; set; }

        [StringLength(10)]
        [Display(Name = "Clasif. Retención")]
        public string CODI_CLASRET { get; set; }

        [Required]
        [Display(Name = "Provincia")]
        public int ID_PROVINCIA { get; set; }

        [Required]
        [Display(Name = "Cantón")]
        public int ID_CANTON { get; set; }

        [Required]
        [Display(Name = "Parroquia")]
        public int ID_PARROQUIA { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Usuario Crea")]
        public string USER_CREA { get; set; }

        [Required]
        [Display(Name = "Fecha Registro")]
        public DateTime FECHA_CREA { get; set; }

        [StringLength(50)]
        [Display(Name = "Usuario Modifica")]
        public string USER_MODIF { get; set; }

        [Display(Name = "Fecha Modifica")]
        public DateTime FECHA_MODIF { get; set; }

        public virtual Parroquia Parroquias { get; set; }
        public virtual ICollection<Nomina.RP_TIPOROL> RP_TIPOROL { get; set; }
        public virtual BANCOS BANCOS { get; set; }
        public virtual ICollection<Nomina.RP_RECEPFONDOS> RP_RECEPFONDOS { get; set; }
        public virtual ICollection<Contabilidad.CO_FACTURAS> CO_FACTURAS { get; set; }
    }
}