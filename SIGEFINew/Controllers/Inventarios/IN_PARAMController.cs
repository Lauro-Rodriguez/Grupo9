using SIGEFINew.Models;
using SIGEFINew.Models.Inventarios;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SIGEFINew.Controllers.Inventarios
{
    public class IN_PARAMController : Controller
    {
        // GET: IN_PARAM
        private ApplicationDbContext db = new ApplicationDbContext();
        int PorcIVA;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);
            var Param = from s in db.IN_PARAM
                        where s.ID_INSTITUCION == IdInst && s.PERI_CODIGO == Pericod
                        select s;

            var Param2 = (from s in db.CO_PARAMETROS
                          where s.ID_INSTITUCION == IdInst && s.PERI_CODIGO == Pericod
                          select s).FirstOrDefault();

            PorcIVA = Param2.PORIVA;

            //string Anio = Session["Anio"].ToString();


            IN_PARAM iN_PARAM = db.IN_PARAM.Find(IdInst, Pericod, 1);

            if (Param.Count() == 0)
            {
                ViewBag.UserCrea = Session["Usuario"].ToString();
                ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");
                ViewBag.EsNuevo = 0;
                ViewBag.IVA = PorcIVA;
                return View(iN_PARAM);
            }
            else
            {
                ViewBag.UserCrea = iN_PARAM.USER_CREA;
                ViewBag.FechaCrea = iN_PARAM.FECHA_CREA;
            }
            ViewBag.IVA = PorcIVA;
            return View(iN_PARAM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,NUM_FILA,PORC_IVA,AMPLI_DESC,UNID_OPER,ORD_PROD,NAUT_PROD,CONT_BOD,CONT_TSP,NSEP_DEVO,NSEP_TRAN,NSEP_STOCK,COD_PROD,ORD_X_COD,REC_CON_IVA,UTIL_DEC_CANTI,UTIL_DEC_CUNI,UTIL_DEC_CTOT,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF,ING_MANUAL_ALERTSTOCK,ALERTSTOCK_MINMAX,CTA_INVERSION,PERM_QING_M_QORD,CAM_P_UNITARIO,NOT_TRANSINCONT")] IN_PARAM iN_PARAM)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);

            var Param = from s in db.IN_PARAM
                        where s.ID_INSTITUCION == IdInst && s.PERI_CODIGO == Pericod
                        select s;

            var Param2 = (from s in db.CO_PARAMETROS
                          where s.ID_INSTITUCION == IdInst && s.PERI_CODIGO == Pericod
                          select s).FirstOrDefault();

            PorcIVA = Param2.PORIVA;

            int TotReg = Param.Count();

            iN_PARAM.ID_INSTITUCION = IdInst;
            iN_PARAM.PERI_CODIGO = Pericod;
            iN_PARAM.NUM_FILA = 1;
            if (TotReg == 0)
            {
                iN_PARAM.NUM_FILA = 1;
                iN_PARAM.NAUT_PROD = false;
                //iN_PARAM.CONT_BOD = false;
                iN_PARAM.CONT_TSP = 0;
                //iN_PARAM.ORD_X_COD = false;
                iN_PARAM.REC_CON_IVA = false;

            }
            if (ModelState.IsValid)
            {
                if (TotReg == 0)
                {

                    db.IN_PARAM.Add(iN_PARAM);
                }
                else
                {
                    iN_PARAM.USER_MODIF = Session["Usuario"].ToString();
                    iN_PARAM.FECHA_MODIF = DateTime.Today;
                    db.Entry(iN_PARAM).State = EntityState.Modified;
                }

                db.SaveChanges();
                TempData["success"] = "Los datos se guardaron correctamente!!";
                //return View(iN_PARAM);
            }
            ViewBag.UserCrea = Session["Usuario"].ToString();
            ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");
            ViewBag.IVA = PorcIVA;
            return View(iN_PARAM);
        }
    }
}