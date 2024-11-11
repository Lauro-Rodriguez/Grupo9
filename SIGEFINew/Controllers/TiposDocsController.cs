using PagedList;
using SIGEFINew.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace SIGEFINew.Controllers
{
    [Authorize(Roles = "Master,CO_TiposDoc")]
    public class TiposDocsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TiposDocs
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var tiposdocs = from s in db.TiposDocs
                            select s;

            ViewBag.Descripcion = String.IsNullOrEmpty(sortOrder) ? "descripcion" : "";

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
                tiposdocs = tiposdocs.Where(s => s.DESCRIPCION.Contains(searchString)
                                       || s.DESCRIPCION.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "descripcion":
                    tiposdocs = tiposdocs.OrderByDescending(s => s.DESCRIPCION);
                    break;
                default:
                    tiposdocs = tiposdocs.OrderBy(s => s.DESCRIPCION);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentFilter = searchString;

            return View(tiposdocs.ToPagedList(pageNumber, pageSize));
        }

        // GET: TiposDocs/Details/5
        public ActionResult Details(string id)
        {

            TiposDoc tiposDoc = db.TiposDocs.Find(id);
            if (tiposDoc == null)
            {
                return HttpNotFound();
            }
            return View(tiposDoc);
        }

        // GET: TiposDocs/Create
        public ActionResult Create(int? id, string id2)
        {
            TiposDoc tiposDoc = db.TiposDocs.Find(id2);
            var tipossri = from s in db.TiposSRI
                           select s;

            if (id2 != null)
            {
                ViewBag.TIPO_SRI = new SelectList(tipossri, "CODIGO", "DESCRIPCION", tiposDoc.TIPO_SRI);
                ViewBag.EsNuevo = 0;
            }
            else
            {
                ViewBag.TIPO_SRI = new SelectList(tipossri, "CODIGO", "DESCRIPCION");
                ViewBag.EsNuevo = 1;
            }

            return View(tiposDoc);
        }

        // POST: TiposDocs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "COD_TIPO_DOC,DESCRIPCION,TIPO_SRI")] TiposDoc tiposDoc, int EsNuevo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (EsNuevo == 1)
                    {
                        db.TiposDocs.Add(tiposDoc);
                    }
                    else
                    {
                        db.Entry(tiposDoc).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception Ex)
                {
                    TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                    ViewBag.MsgError = TempData["MsgError"];
                }

            }
            var tipossri = from s in db.TiposSRI
                           select s;
            ViewBag.TIPO_SRI = new SelectList(tipossri, "CODIGO", "DESCRIPCION", tiposDoc.TIPO_SRI);
            ViewBag.EsNuevo = EsNuevo;
            return View(tiposDoc);
        }

        // GET: TiposDocs/Edit/5
        public ActionResult Edit(int? id, string id2)
        {
            if (id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposDoc tiposDoc = db.TiposDocs.Find(id2);
            if (tiposDoc == null)
            {
                return HttpNotFound();
            }
            var tipossri = from s in db.TiposSRI
                           select s;
            ViewBag.TipoSRI = tiposDoc.TIPO_SRI;
            ViewBag.TIPO_SRI = new SelectList(tipossri, "CODIGO", "DESCRIPCION");
            return View(tiposDoc);
        }

        // POST: TiposDocs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "COD_TIPO_DOC,DESCRIPCION,TIPO_SRI")] TiposDoc tiposDoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiposDoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var tipossri = from s in db.TiposSRI
                           select s;
            ViewBag.TIPO_SRI = new SelectList(tipossri, "CODIGO", "DESCRIPCION");
            return View(tiposDoc);
        }

        // GET: TiposDocs/Delete/5
        public ActionResult Delete(int? id, string id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposDoc tiposDoc = db.TiposDocs.Find(id2);
            if (tiposDoc == null)
            {
                return HttpNotFound();
            }
            return View(tiposDoc);
        }

        // POST: TiposDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, string id2)
        {
            TiposDoc tiposDoc = db.TiposDocs.Find(id2);
            try
            {
                db.TiposDocs.Remove(tiposDoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                ViewBag.MsgError = TempData["MsgError"];
            }
            return View(tiposDoc);
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
