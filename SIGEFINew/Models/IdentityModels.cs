using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
namespace SIGEFINew.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string NumIdentif { get; set; }
        public string TypeUser { get; set; }
        public bool Active { get; set; }
        public DateTime FechaReg { get; set; }
        //public int ORG_CODIGO { get; set; }
        public int CODIGO_ROL { get; set; }
        public int ID_EMPLEADO { get; set; }
        public int ID_INSTITUCION { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("SIGEFI");

            modelBuilder.Entity<SIGEFINew.Models.Presupuesto.PartIngreso>()
             .HasIndex(p => new { p.ID_INSTITUCION, p.PAIN_CLAVE }).IsUnique(true);

            modelBuilder.Entity<SIGEFINew.Models.Presupuesto.PartEgreso>()
             .HasIndex(p => new { p.ID_INSTITUCION, p.PAEG_CLAVE }).IsUnique(true);

            modelBuilder.Entity<SIGEFINew.Models.Contabilidad.CO_ASIENTOSCONT>()
             .HasIndex(p => new { p.ID_INSTITUCION, p.PERI_CODIGO, p.TIPO_COMPROB, p.NUM_COMPROB }).IsUnique(true);

            modelBuilder.Entity<SIGEFINew.Models.Inventarios.IN_COMPRAS>()
             .HasIndex(p => new { p.COD_PROV, p.NUM_DOC }).IsUnique(true);

            modelBuilder.Entity<SIGEFINew.Models.Nomina.RP_EMPLEADOS>()
             .HasIndex(p => new { p.ID_INSTITUCION, p.NUM_CEDULA }).IsUnique(true);

            modelBuilder.Entity<SIGEFINew.Models.Nomina.RP_EMPLEADOS>()
             .HasIndex(p => new { p.ID_INSTITUCION, p.APELLIDOS, p.NOMBRES }).IsUnique(true);

            modelBuilder.Entity<SIGEFINew.Models.DOCUMSPDFS>()
             .HasIndex(p => new { p.NOM_ARCHIVO }).IsUnique(true);
            
            base.OnModelCreating(modelBuilder);
            // ...
        }

        //Conexión para Postgress
        public ApplicationDbContext()
            : base("NPGSQLDBContext", throwIfV1Schema: false)
        {
        }

        //Conexión para Oracle
        //public ApplicationDbContext()
        //   : base("SIGEFIORA", throwIfV1Schema: false)
        //{
        //}

        //Conexión para SQL Server
        //public ApplicationDbContext()
        //  : base("SIGEFISQL", throwIfV1Schema: false)
        //{
        //}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //Tablas GENERALES
        public System.Data.Entity.DbSet<SIGEFINew.Models.Instituciones> INSTITUCIONES { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Periodos> PERIODOS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Provincias> PROVINCIAS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Cantones> CANTONES { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Parroquia> PARROQUIAS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Mnu_Opciones> MNU_OPCIONES { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Mnu_Roles> MNU_ROLES { get; set; }
        //public System.Data.Entity.DbSet<SIGEFINew.Models.TiposDoc> TiposDoc { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.TiposDoc> TiposDocs { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.TiposSRI> TiposSRI { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.FIRMAS> FIRMAS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nacionalidades> Nacionalidades { get; set; }

        #region PLANIFICACION PRESUPUESTARIA
        //Tablas para PLANIFICACION PRESUPUESTARIA
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PartIngreso> PartIngresoes { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.Programa> Programas { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.SubPrograma> SubProgramas { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.Proyecto> Proyectoes { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.Actividad> Actividads { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.Grupo> Grupoes { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.Item> Items { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PartEgreso> PartEgresoes { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.OrganicoF> OrganicoFs { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.TipoRecursos> TipoRecursos { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_PNBV> POA_PNBV { get; set; }
        #endregion

        #region Contabilidad
        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_PARAMETROS> CO_PARAMETROS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_ASIENTOSCONT> CO_ASIENTOSCONT { get; set; }

        #endregion

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_EAI> POA_EAI { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_OEI> POA_OEI { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_ED_PDOT> POA_ED_PDOT { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_OE_PDOT> POA_OE_PDOT { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_PR_PDOT> POA_PR_PDOT { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_EPG> POA_EPG { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.Catalogos> Catalogos { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_POL_PDOT> POA_POL_PDOT { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_META_PDOT> POA_META_PDOT { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_OE_PLANIF> POA_OE_PLANIF { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_PLAN> POA_PLAN { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_TIPOROL> RP_TIPOROL { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_EMPLEADOS> RP_EMPLEADOS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_RELACIONLABORAL> RP_RELACIONLABORAL { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_CARGOS> RP_CARGOS { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_TIPOPROYECTOS> POA_TIPOPROYECTOS { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_PLANOBJETIVOS> POA_PLANOBJETIVOS { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_FONDOS> PR_FONDOS { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_FONDOXOBJTVO> POA_FONDOXOBJTVO { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_INDICADOROBJ> POA_INDICADOROBJ { get; set; }

        //public System.Data.Entity.DbSet<SIGEFINew.Models.Proveedores> Proveedores { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_OBJTIVORECURSOS> POA_OBJTIVORECURSOS { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.DETALLEINGRESOS> DETALLEINGRESOS { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.TECHOSPRESUP> TECHOSPRESUPs { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.DETALLEGRESOS> DETALLEGRESOS { get; set; }


        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_CUENTASCONT> CO_CUENTASCONT { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Proveedores> Proveedores { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.BANCOS> BANCOS { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_PROGRAMAS> PR_PROGRAMAS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_SUBPROGRAMAS> PR_SUBPROGRAMAS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_PROYECTOS> PR_PROYECTOS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_ACTIVIDADES> PR_ACTIVIDADES { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_GRUPOS> PR_GRUPOS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_ITEMS> PR_ITEMS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_PARTEGRESOS> PR_PARTEGRESOS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_PARTINGRESOS> PR_PARTINGRESOS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_MOVINGRESO> PR_MOVINGRESO { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_DETINGRESO> PR_DETINGRESO { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_DOCUMMOVINGRESO> PR_DOCUMMOVINGRESO { get; set; }
        //public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_MOVGASTO> PR_MOVGASTO { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_DETGASTO> PR_DETGASTO { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_DOCUMMOVGASTO> PR_DOCUMMOVGASTO { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_DOCUMCERTDISP> PR_DOCUMCERTDISP { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_RELACONTAB> PR_RELACONTAB { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_RELACION> CO_RELACION { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_SOLICDISPPRESUP> PR_SOLICDISPPRESUP { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_DETALSOLICDISPPRES> PR_DETALSOLICDISPPRES { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_CLASIFRETEN> CO_CLASIFRETEN { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_RELARETCONT> CO_RELARETCONT { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_CUENBANCOS> CO_CUENBANCOS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_LINEACREDITO> CO_LINEACREDITO { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_DETALLEBANCOS> CO_DETALLEBANCOS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_CERTDISPRESUP> PR_CERTDISPRESUP { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_DETCERTDISP> PR_DETCERTDISP { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Presupuesto.PR_MOVGASTO> PR_MOVGASTO { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_PARAM> IN_PARAM { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_DETCOMPRA> IN_DETCOMPRA { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_COMPRAS> IN_COMPRAS { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_CTASPPAGAR> CO_CTASPPAGAR { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_RETENCIONES> CO_RETENCIONES { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_DETCTASPPAG> CO_DETCTASPPAG { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_BODEGAS> IN_BODEGAS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_USERSXBOD> IN_USERSXBOD { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_CARACGEN> IN_CARACGEN { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_PRESENTA> IN_PRESENTA { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_TIPOSTRAN> IN_TIPOSTRAN { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_ITEMS> IN_ITEMS { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_CLASES> IN_CLASES { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_SUBTIPOS> IN_SUBTIPOS { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_TIPOS> IN_TIPOS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_CARGA_INI> IN_CARGA_INI { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_PRODUCTOS> IN_PRODUCTOS { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_TIPOSEGURO> RP_TIPOSEGURO { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_PORCAPORTACION> RP_PORCAPORTACION { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_ABONOS> RP_ABONOS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_ANTICIPOS> RP_ANTICIPOS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_CUOTASANTICIPO> RP_CUOTASANTICIPO { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_RECEPFONDOS> RP_RECEPFONDOS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_RUBROS> RP_RUBROS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_VARIABLES> RP_VARIABLES { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_ROLESXRUBRO> RP_ROLESXRUBRO { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_TIPOSANTICIPO> RP_TIPOSANTICIPO { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_SETUP> RP_SETUP { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_SECUENCIAS> RP_SECUENCIAS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_HORASEXT> RP_HORASEXT { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_CARGASFAMILIARES> RP_CARGASFAMILIARES { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_DETALLEROL> RP_DETALLEROL { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_OTROSMOV> RP_OTROSMOV { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_TIRENTA> RP_TIRENTA { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Tramites.TR_HISTORIAL> TR_HISTORIAL { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Contratacion.PO_CONTRATOS> PO_CONTRATOS { get; set; }
        public System.Data.Entity.DbSet<Barrios> Barrios { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.SUSTENTOTRIBUT> SUSTENTOTRIBUT { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.FORMAS_PAGO> FORMAS_PAGO { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_FACTURAS> CO_FACTURAS { get; set; }

        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_TIPOSMOV> RP_TIPOSMOV { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_GRUPOCUPACIONAL> RP_GRUPOCUPACIONAL { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Nomina.RP_ACCIONPERSONAL> RP_ACCIONPERSONAL { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_PYHISTORIAL> POA_PYHISTORIAL { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_CPTOEJECUCION> POA_CPTOEJECUCION { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_FASESPROYS> POA_FASESPROYS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.POA.POA_EVALUACION> POA_EVALUACION { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.DOCUMSPDFS> DOCUMSPDFS { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_TRANSACCIONES> IN_TRANSACCIONES { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_DETALLETRANSAC> IN_DETALLETRANSAC { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Inventarios.IN_UNIOPE> IN_UNIOPE { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_DETFACTURA> CO_DETFACTURA { get; set; }
        public System.Data.Entity.DbSet<SIGEFINew.Models.Contabilidad.CO_VALORESXFACTURA> CO_VALORESXFACTURA { get; set; }
    }
}