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
    public class IN_TIPOSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: IN_TIPOS
        public ActionResult Index(int? id, string sortOrder, string currentFilter, string searchString, int? page)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var iN_TIPOS = db.IN_TIPOS.Include(c => c.IN_CLASES).Where(i => i.ID_INSTITUCION ==
                  IdInst && i.PERI_CODIGO == PeriCod && i.ID_CLASE == id);

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
                iN_TIPOS = iN_TIPOS.Where(s => s.NOM_TIPO.Contains(searchString)
                                       || s.NOM_TIPO.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_tipo":
                    iN_TIPOS = iN_TIPOS.OrderByDescending(s => s.ID_TIPO);
                    break;
                case "nom_tipo":
                    iN_TIPOS = iN_TIPOS.OrderBy(s => s.NOM_TIPO);
                    break;
                default:
                    iN_TIPOS = iN_TIPOS.OrderBy(s => s.ID_TIPO);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentFilter = searchString;

            ViewBag.IdClase = id;
            return View(iN_TIPOS.ToList());
        }

        // GET: IN_TIPOS/Details/5
        public ActionResult Details(int id, int id2)
        {
            return RedirectToAction("Index", "IN_SUBTIPOS", new { @id = id, @id2 = id2 });
        }

        // GET: IN_TIPOS/Create
        public ActionResult Create(int? IdClase, int? id2)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_TIPOS iN_TIPOS = db.IN_TIPOS.Find(IdInst, PeriCod, IdClase, id2);

            if (id2 != null)
            {
                ViewBag.EsNuevo = 0;
                ViewBag.IdClase = IdClase;
            }
            else
            {
                ViewBag.EsNuevo = 1;
                ViewBag.IdClase = IdClase;
            }

            return View(iN_TIPOS);
        }

        // POST: IN_TIPOS/Create
        [HttpPost]
        public ActionResult Create(IN_TIPOS iN_TIPOS, int EsNuevo, int IdClase)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            iN_TIPOS.ID_INSTITUCION = IdInst;
            iN_TIPOS.PERI_CODIGO = PeriCod;
            iN_TIPOS.ID_CLASE = IdClase;
            int CodPres = 0;

            try
            {
                CodPres = (from nd in db.IN_TIPOS
                           where nd.ID_INSTITUCION == IdInst && nd.PERI_CODIGO == PeriCod
                           && nd.ID_CLASE == IdClase
                           select nd.ID_TIPO).Max();
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
                        iN_TIPOS.ID_TIPO = CodPres + 1;
                        db.IN_TIPOS.Add(iN_TIPOS);
                    }
                    else
                    {
                        db.Entry(iN_TIPOS).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = IdClase });
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
            return View(iN_TIPOS);
        }

        // GET: IN_TIPOS/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IN_TIPOS/Edit/5
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

        // GET: IN_TIPOS/Delete/5

        public ActionResult Delete(int IdClase, int id2)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            if (id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_TIPOS iN_TIPOS = db.IN_TIPOS.Find(IdInst, PeriCod, IdClase, id2);
            if (iN_TIPOS == null)
            {
                return HttpNotFound();
            }
            return View(iN_TIPOS);
        }

        // POST: IN_CLASES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int IdClase, int id2)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            IN_TIPOS iN_TIPOS = db.IN_TIPOS.Find(IdInst, PeriCod, IdClase, id2);

            try
            {
                db.IN_TIPOS.Remove(iN_TIPOS);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = IdClase });
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                ViewBag.MsgError = TempData["MsgError"];
            }
            return View(iN_TIPOS);
        }
    }
}
