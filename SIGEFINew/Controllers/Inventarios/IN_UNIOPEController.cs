using SIGEFINew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGEFINew.Models.Inventarios;
using SIGEFINew.Models.Presupuesto;
using System.Data.Entity;
using System.Net;

namespace SIGEFINew.Controllers.Inventarios
{
    [Authorize]
    public class IN_UNIOPEController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: IN_UNIOPE
        public ActionResult Index()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var iNUIDOPER = db.IN_UNIOPE.Include(c => c.OrganicoF).Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod);
            return View(iNUIDOPER.ToList());
        }

        // GET: IN_UNIOPE/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IN_UNIOPE/Create
        public ActionResult Create(int? id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_UNIOPE iN_UNIOPE = db.IN_UNIOPE.Find(IdInst, PeriCod,id);

            var OrgF = db.OrganicoFs.Where(f=>f.ID_INSTITUCION==IdInst && f.PERI_CODIGO==PeriCod).ToList();

            OrgF.Add(new OrganicoF { ORG_CODIGO = 0, ORG_NOMBRE = "[Seleccione una Dirección]" });
            

            if (id != null)
            {
                ViewBag.ORG_CODIGO = new SelectList(OrgF.OrderBy(o => o.ORG_NOMBRE), "ORG_CODIGO", "ORG_NOMBRE",iN_UNIOPE.ORG_CODIGO);
                ViewBag.EsNuevo = 0;
            }
            else
            {
                ViewBag.ORG_CODIGO = new SelectList(OrgF.OrderBy(o => o.ORG_NOMBRE), "ORG_CODIGO", "ORG_NOMBRE");
                ViewBag.EsNuevo = 1;
            }

           
            //ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");

            return View(iN_UNIOPE);
        }

        // POST: IN_UNIOPE/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,ID_UNIDOPERATIVA,DESCRIPCION,ORG_CODIGO")] IN_UNIOPE iN_UNIOPE, int EsNuevo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            int NumDisp=0;

            iN_UNIOPE.ID_INSTITUCION = IdInst;
            iN_UNIOPE.PERI_CODIGO = PeriCod;
            
            if (EsNuevo == 1)
            {
                try
                {
                    NumDisp = (from nd in db.IN_UNIOPE
                               where nd.ID_INSTITUCION == IdInst && nd.PERI_CODIGO == PeriCod
                               select nd.ID_UNIDOPERATIVA).Max();
                }
                catch
                {

                }

                iN_UNIOPE.ID_UNIDOPERATIVA = NumDisp+1;
            }
            //cO_LINEACREDITO.CODIGO_CG = "0";
            try
            {
                if (ModelState.IsValid)
                {

                    if (EsNuevo == 1)
                    {
                        
                        db.IN_UNIOPE.Add(iN_UNIOPE);
                    }
                    else
                    {
                        db.Entry(iN_UNIOPE).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                 var OrgF = db.OrganicoFs.Where(f=>f.ID_INSTITUCION==IdInst && f.PERI_CODIGO==PeriCod).ToList();

                OrgF.Add(new OrganicoF { ORG_CODIGO = 0, ORG_NOMBRE = "[Seleccione una Dirección]" });
                ViewBag.ORG_CODIGO = new SelectList(OrgF.OrderBy(o => o.ORG_NOMBRE), "ORG_CODIGO", "ORG_NOMBRE", iN_UNIOPE.ORG_CODIGO);
                if (EsNuevo == 1)
                {
                    ViewBag.EsNuevo = 1;
                }
                else
                {
                    ViewBag.EsNuevo = 0;
                }
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                ViewBag.MsgError = TempData["MsgError"];
            }


            return View(iN_UNIOPE);
        }


        // GET: IN_UNIOPE/Delete/5
        public ActionResult Delete(int? id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IN_UNIOPE iN_UNIOPE = db.IN_UNIOPE.Find(IdInst, PeriCod, id);


            if (iN_UNIOPE == null)
            {
                return HttpNotFound();
            }
            return View(iN_UNIOPE);
        }

        // POST: IN_UNIOPE/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_UNIOPE iN_UNIOPE = db.IN_UNIOPE.Find(IdInst, PeriCod, id);

            try
            {
                db.IN_UNIOPE.Remove(iN_UNIOPE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                ViewBag.MsgError = TempData["MsgError"];
            }
            return View(iN_UNIOPE);
        }
    }
}
