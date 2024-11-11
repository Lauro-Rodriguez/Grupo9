using SIGEFINew.Models;
using SIGEFINew.Models.Inventarios;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SIGEFINew.Controllers.Inventarios
{
    public class IN_BODEGASController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IN_BODEGAS
        public ActionResult Index()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            var iN_BODEGAS = db.IN_BODEGAS.Include(i => i.Periodos).Where(f => f.ID_INSTITUCION == IdInst
             && f.PERI_CODIGO == PeriCod);

            return View(iN_BODEGAS.ToList());
        }

        // GET: IN_BODEGAS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_BODEGAS iN_BODEGAS = db.IN_BODEGAS.Find(id);
            if (iN_BODEGAS == null)
            {
                return HttpNotFound();
            }
            return View(iN_BODEGAS);
        }

        // GET: IN_BODEGAS/Create
        public ActionResult Create(int? id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_BODEGAS iN_BODEGAS = db.IN_BODEGAS.Find(IdInst, PeriCod, id);

            if (id != null)
            {
                ViewBag.EsNuevo = 0;
            }
            else
            {
                ViewBag.EsNuevo = 1;
            }
            ViewBag.UserCrea = Session["Usuario"].ToString();
            ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");
            return View(iN_BODEGAS);
        }

        // POST: IN_BODEGAS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,ID_BODEGA,NOM_BODEGA,TIPO_BODEGA,RESP_BODEGA,DIR_BODEGA,TELF_BODEGA,NUM_ING_BODEGA,NUM_EGR_BODEGA,NUM_ORD_COMPRA,NUM_NTA_ENT,NUMAUT_INGBOD,NUMAUT_EGRBOD,NUMAUT_ORDCOMP,NUMAUT_NOTENT,INCRE1_INGBOD,INCRE1_EGRBOD,INCRE1_ORDCOMP,INCRE1_NOTENT,COD_INI_PROD,COD_AUTOMAT,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF,NUM_DEV_INGRE,NUM_DEV_EGRE,NUM_TRAN_INGRE,NUM_TRAN_EGRE,NUM_GUIA_REMI,NUM_ORDEN_PROD,NUM_PED_MAT,USA_CODBAR,IVA_COSTOPROD")] IN_BODEGAS iN_BODEGAS, int EsNuevo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            iN_BODEGAS.ID_INSTITUCION = IdInst;
            iN_BODEGAS.PERI_CODIGO = PeriCod;

            int NumDisp = 0;
            if (ModelState.IsValid)
            {
                if (EsNuevo == 1)
                {
                    try
                    {
                        NumDisp = (from nd in db.IN_BODEGAS
                                   where nd.ID_INSTITUCION == IdInst && nd.PERI_CODIGO == PeriCod
                                   select nd.ID_BODEGA).Max();
                    }
                    catch
                    {

                    }
                    iN_BODEGAS.ID_BODEGA = NumDisp + 1;
                    db.IN_BODEGAS.Add(iN_BODEGAS);


                }
                else
                {
                    db.Entry(iN_BODEGAS).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(iN_BODEGAS);
        }

        // GET: IN_BODEGAS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_BODEGAS iN_BODEGAS = db.IN_BODEGAS.Find(id);
            if (iN_BODEGAS == null)
            {
                return HttpNotFound();
            }
            return View(iN_BODEGAS);
        }

        // POST: IN_BODEGAS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,ID_BODEGA,NOM_BODEGA,TIPO_BODEGA,RESP_BODEGA,DIR_BODEGA,TELF_BODEGA,NUM_ING_BODEGA,NUM_EGR_BODEGA,NUM_ORD_COMPRA,NUM_NTA_ENT,NUMAUT_INGBOD,NUMAUT_EGRBOD,NUMAUT_ORDCOMP,NUMAUT_NOTENT,INCRE1_INGBOD,INCRE1_EGRBOD,INCRE1_ORDCOMP,INCRE1_NOTENT,COD_INI_PROD,COD_AUTOMAT,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF")] IN_BODEGAS iN_BODEGAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iN_BODEGAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iN_BODEGAS);
        }

        // GET: IN_BODEGAS/Delete/5
        public ActionResult Delete(int? id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_BODEGAS iN_BODEGAS = db.IN_BODEGAS.Find(IdInst,PeriCod, id);
            if (iN_BODEGAS == null)
            {
                return HttpNotFound();
            }
            return View(iN_BODEGAS);
        }

        // POST: IN_BODEGAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            IN_BODEGAS iN_BODEGAS = db.IN_BODEGAS.Find(IdInst,PeriCod, id);

            try
            {
                db.IN_BODEGAS.Remove(iN_BODEGAS);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                ViewBag.MsgError = TempData["MsgError"];
            }

            return View(iN_BODEGAS);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
