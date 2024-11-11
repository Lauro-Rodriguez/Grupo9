using SIGEFINew.Models;
using SIGEFINew.Models.Inventarios;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SIGEFINew.Controllers.Inventarios
{
    [Authorize]
    public class IN_SUBTIPOSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: IN_SUBTIPOS
        public ActionResult Index(int? id, int? id2, string sortOrder, string currentFilter, string searchString, int? page)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var iN_SUBTIPOS = db.IN_SUBTIPOS.Include(c => c.IN_TIPOS).Where(i => i.ID_INSTITUCION ==
                  IdInst && i.PERI_CODIGO == PeriCod && i.ID_CLASE == id && i.ID_TIPO == id2);

            ViewBag.IdClase = id;
            ViewBag.IdTipo = id2;
            return View(iN_SUBTIPOS);
        }

        // GET: IN_SUBTIPOS/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IN_SUBTIPOS/Create
        public ActionResult Create(int? IdClase, int? IdTipo, int? id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_SUBTIPOS iN_SUBTIPOS = db.IN_SUBTIPOS.Find(IdInst, PeriCod, IdClase, IdTipo, id);

            if (id != null)
            {
                ViewBag.EsNuevo = 0;
                ViewBag.IdClase = IdClase;
                ViewBag.IdTipo = IdTipo;
            }
            else
            {
                ViewBag.EsNuevo = 1;
                ViewBag.IdClase = IdClase;
                ViewBag.IdTipo = IdTipo;
            }

            return View(iN_SUBTIPOS);
        }

        // POST: IN_TIPOS/Create
        [HttpPost]
        public ActionResult Create(IN_SUBTIPOS iN_SUBTIPOS, int EsNuevo, int IdClase, int IdTipo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            iN_SUBTIPOS.ID_INSTITUCION = IdInst;
            iN_SUBTIPOS.PERI_CODIGO = PeriCod;
            iN_SUBTIPOS.ID_CLASE = IdClase;
            iN_SUBTIPOS.ID_TIPO = IdTipo;
            int CodPres = 0;

            try
            {
                CodPres = (from nd in db.IN_SUBTIPOS
                           where nd.ID_INSTITUCION == IdInst && nd.PERI_CODIGO == PeriCod
                           && nd.ID_CLASE == IdClase && nd.ID_TIPO == IdTipo
                           select nd.ID_SUBTIPO).Max();
            }
            catch
            {

            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (EsNuevo == 1)
                    {
                        iN_SUBTIPOS.ID_SUBTIPO = CodPres + 1;
                        db.IN_SUBTIPOS.Add(iN_SUBTIPOS);
                    }
                    else
                    {
                        db.Entry(iN_SUBTIPOS).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = IdClase, id2 = IdTipo });
                }
                catch (Exception ex)
                {
                    string msgErr = ex.InnerException.InnerException.Message;

                    TempData["MsgError"] = msgErr;
                    ViewBag.MsgError = TempData["MsgError"];
                }

            }
            if (EsNuevo == 1)
            {

                ViewBag.EsNuevo = 1;
            }
            return View(iN_SUBTIPOS);
        }

        // GET: IN_SUBTIPOS/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IN_SUBTIPOS/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: IN_SUBTIPOS/Delete/5
        public ActionResult Delete(int IdClase, int IdTipo, int id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_SUBTIPOS iN_SUBTIPOS = db.IN_SUBTIPOS.Find(IdInst, PeriCod, IdClase, IdTipo, id);
            if (iN_SUBTIPOS == null)
            {
                return HttpNotFound();
            }
            return View(iN_SUBTIPOS);
        }

        // POST: IN_CLASES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int IdClase, int IdTipo, int id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            IN_SUBTIPOS iN_SUBTIPOS = db.IN_SUBTIPOS.Find(IdInst, PeriCod, IdClase, IdTipo, id);

            try
            {
                db.IN_SUBTIPOS.Remove(iN_SUBTIPOS);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = IdClase, id2 = IdTipo });
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                ViewBag.MsgError = TempData["MsgError"];
            }
            return View(iN_SUBTIPOS);
        }
    }
}
