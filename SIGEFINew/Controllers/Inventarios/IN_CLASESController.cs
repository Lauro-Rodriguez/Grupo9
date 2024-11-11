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
    public class IN_CLASESController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: IN_CLASES
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            ViewBag.desripcion = String.IsNullOrEmpty(sortOrder) ? "desripcion" : "";
            ViewBag.desripcion = sortOrder == "string" ? "desripcion" : "string";

            var iN_CLASES = db.IN_CLASES.Where(i => i.ID_INSTITUCION == IdInst && i.PERI_CODIGO == PeriCod);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                iN_CLASES = iN_CLASES.Where(s => s.DESRIPCION.Contains(searchString)
                                       || s.DESRIPCION.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_clase":
                    iN_CLASES = iN_CLASES.OrderByDescending(s => s.ID_CLASE);
                    break;
                case "descrip_cagen":
                    iN_CLASES = iN_CLASES.OrderBy(s => s.DESRIPCION);
                    break;
                default:
                    iN_CLASES = iN_CLASES.OrderBy(s => s.ID_CLASE);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentFilter = searchString;

            return View(iN_CLASES.ToList());
        }

        // GET: IN_CLASES/Details/5
        public ActionResult Details(int id)
        {
            return RedirectToAction("Index", "IN_TIPOS", new { @id = id });
        }

        // GET: IN_CLASES/Create
        public ActionResult Create(int? id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_CLASES iN_CLASES = db.IN_CLASES.Find(IdInst, PeriCod, id);
            if (id != null)
            {
                ViewBag.EsNuevo = 0;
            }
            else
            {
                ViewBag.EsNuevo = 1;
            }

            return View(iN_CLASES);
        }

        // POST: IN_CLASES/Create
        [HttpPost]
        public ActionResult Create(IN_CLASES iN_CLASES, int EsNuevo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            iN_CLASES.ID_INSTITUCION = IdInst;
            iN_CLASES.PERI_CODIGO = PeriCod;

            int CodPres = 0;

            try
            {
                CodPres = (from nd in db.IN_CLASES
                           where nd.ID_INSTITUCION == IdInst && nd.PERI_CODIGO == PeriCod
                           select nd.ID_CLASE).Max();
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
                        iN_CLASES.ID_CLASE = CodPres + 1;
                        db.IN_CLASES.Add(iN_CLASES);
                    }
                    else
                    {
                        db.Entry(iN_CLASES).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
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
            return View(iN_CLASES);
        }

        // GET: IN_CLASES/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IN_CLASES/Edit/5
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

        // GET: IN_CLASES/Delete/5
        public ActionResult Delete(int id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_CLASES iN_CLASES = db.IN_CLASES.Find(IdInst, PeriCod, id);
            if (iN_CLASES == null)
            {
                return HttpNotFound();
            }
            return View(iN_CLASES);
        }

        // POST: IN_CLASES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            IN_CLASES iN_CLASES = db.IN_CLASES.Find(IdInst, PeriCod, id);
            try
            {
                db.IN_CLASES.Remove(iN_CLASES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                ViewBag.MsgError = TempData["MsgError"];
            }

            return View(iN_CLASES);
        }
    }
}
