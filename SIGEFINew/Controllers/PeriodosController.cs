using SIGEFINew.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SIGEFINew.Controllers
{
    [Authorize]
    public class PeriodosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Periodos
        public ActionResult Index()
        {
            //     var Courses = db.Documentos
            //.Include(c => c.TIPOS_DOCUMEN)
            ////.Include(d => d.DIRECCIONES)
            //.AsNoTracking()
            //.Where(d => d.UserName.ToString().Contains(Usuario))
            //.ToList();
            var pERIODOS = db.PERIODOS.Include(p => p.INSITUCIONES);
            //.AsNoTracking()
            //.Where(d => d.ID_INSTITUCION == (int)Session["IdInst"]);

            return View(pERIODOS.ToList());
        }

        // GET: Periodos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodos periodos = db.PERIODOS.Find(id);
            if (periodos == null)
            {
                return HttpNotFound();
            }
            return View(periodos);
        }

        // GET: Periodos/Create
        public ActionResult Create(int? id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            Periodos periodos = db.PERIODOS.Find(IdInst, id);
            if (id != null)
            {
                ViewBag.EsNuevo = 0;
            }
            else
            {
                ViewBag.EsNuevo = 1;
            }

            ViewBag.ID_INSTITUCION = new SelectList(db.INSTITUCIONES, "ID_INSTITUCION", "NOM_INSTITUCION");
            return View(periodos);
        }

        // POST: Periodos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,PERI_DESCRIPCION,ACTIVO,FECHA_CREA,CERRADO")] Periodos periodos, int EsNuevo)
        {
            //periodos.PERI_CODIGO = 1;
            //if (ModelState.IsValid)
            //{
            try
            {
                if (EsNuevo == 1)
                {
                    db.PERIODOS.Add(periodos);
                }
                else
                {
                    db.Entry(periodos).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                ViewBag.MsgError = TempData["MsgError"];
            }
            //}
            ViewBag.EsNuevo = EsNuevo;
            ViewBag.ID_INSTITUCION = new SelectList(db.INSTITUCIONES, "ID_INSTITUCION", "NOM_INSTITUCION", periodos.ID_INSTITUCION);
            return View(periodos);
        }

        // GET: Periodos/Edit/5
        public ActionResult Edit(int? id, int? idInst)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodos periodos = db.PERIODOS.Find(id, idInst);
            if (periodos == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_INSTITUCION = new SelectList(db.INSTITUCIONES, "ID_INSTITUCION", "NOM_INSTITUCION", periodos.ID_INSTITUCION);
            return View(periodos);
        }

        // POST: Periodos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PERI_CODIGO,ID_INSTITUCION,PERI_DESCRIPCION,ACTIVO,FECHA_CREA,CERRADO")] Periodos periodos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(periodos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_INSTITUCION = new SelectList(db.INSTITUCIONES, "ID_INSTITUCION", "NOM_INSTITUCION", periodos.ID_INSTITUCION);
            return View(periodos);
        }

        // GET: Periodos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodos periodos = db.PERIODOS.Find(id);
            if (periodos == null)
            {
                return HttpNotFound();
            }
            return View(periodos);
        }

        // POST: Periodos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Periodos periodos = db.PERIODOS.Find(id);
            db.PERIODOS.Remove(periodos);
            db.SaveChanges();
            return RedirectToAction("Index");
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
