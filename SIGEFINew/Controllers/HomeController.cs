using SIGEFINew.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SIGEFINew.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            Session["IdDirec"] = 0;
            int SolDisp = 0;
            try
            {
                int IdInst = Convert.ToInt32(Session["IdInst"]);
                int Pericod = Convert.ToInt32(Session["IdPeriodo"]);
                int IdSec = Convert.ToInt32(Session["OrgCodigo"]);
                int IdEmp = Convert.ToInt32(Session["IdEmpleado"]);
                string Usuario = Session["Usuario"].ToString();


                var NomSec = db.OrganicoFs.Where(f => f.ID_INSTITUCION == IdInst && f.ORG_CODIGO == IdSec).FirstOrDefault();
                ViewBag.Seccion = NomSec.ORG_NOMBRE;

                SolDisp = db.PR_SOLICDISPPRESUP.Where(f => f.ID_INSTITUCION == IdInst &&
                f.ORG_CODIGO == IdSec && f.PERI_CODIGO == Pericod && f.ANULADO == false && f.CODIGO_DOC == 100).Count();
            }
            catch
            {
                //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                //return RedirectToAction("Login", "Account");
            }
            ViewBag.TotSolFDisp = SolDisp;
            return View();
        }

        [Authorize(Roles = "POA")]
        public ActionResult PlanifPres()
        {
            Session["IdDirec"] = "1";
            var mNuOpciones = db.MNU_OPCIONES.Include(p => p.MNU_MODULOS)
            .AsNoTracking()
            .Where(d => d.COD_MODULO == 1);

            ViewBag.Message = "Your application description page.";

            return View(mNuOpciones.ToList());
        }

        [Authorize(Roles = "Contabilidad")]
        public ActionResult Contabilidad()
        {
            Session["IdDirec"] = "2";
            var mNuOpciones = db.MNU_OPCIONES.Include(p => p.MNU_MODULOS)
            .AsNoTracking()
            .Where(d => d.COD_MODULO == 1);

            ViewBag.Message = "Your application description page.";

            return View(mNuOpciones.ToList());
        }

        [Authorize(Roles = "Inventarios")]
        public ActionResult Inventarios()
        {
            Session["IdDirec"] = "3";
            var mNuOpciones = db.MNU_OPCIONES.Include(p => p.MNU_MODULOS)
            .AsNoTracking()
            .Where(d => d.COD_MODULO == 1);

            ViewBag.Message = "Your application description page.";

            return View(mNuOpciones.ToList());
        }
        public ActionResult Nomina()
        {
            Session["IdDirec"] = "4";
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            var mNuOpciones = db.MNU_OPCIONES.Include(p => p.MNU_MODULOS)
            .AsNoTracking()
            .Where(d => d.COD_MODULO == 1);
            ViewBag.Message = "Your application description page.";
            int IdRol = Convert.ToInt32(Session["IdTipoRol"]);

            var rP_SETUP = db.RP_SETUP.Where(f => f.ID_INSTITUCION == IdInst).SingleOrDefault();

            Session["AnioProc"] = rP_SETUP.ANIO_PROC;
            Session["MesProc"] = rP_SETUP.MES_PROC;

            if (Convert.ToInt32(Session["IdTipoRol"]) > 0)
            {
                var rP_TIPOROL = db.RP_TIPOROL.Where(f => f.ID_INSTITUCION == IdInst &&
                f.ID_TIPOROL == IdRol).SingleOrDefault();
                ViewBag.Tit = " : " + rP_TIPOROL.DESCRIPCION;
            }
            else
            {
                ViewBag.Tit = " : [No Existe Ningún Rol Activo]";
            }
            return View(mNuOpciones.ToList());
        }

        [Authorize(Roles = "Administracion")]
        public ActionResult Settings()
        {
            Session["IdDirec"] = "8";
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}