using SIGEFINew.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace SIGEFINew.Controllers
{
    [Authorize(Roles = "Master,Bancos")]
    public class BANCOSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BANCOS
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var bancos = from s in db.BANCOS
                         select s;
            ViewBag.Codigo_Banco = String.IsNullOrEmpty(sortOrder) ? "codigo_banco" : "";
            ViewBag.Descripcion = sortOrder == "string" ? "descripcion" : "strimg";

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
                bancos = bancos.Where(s => s.DESCRIPCION.Contains(searchString)
                                       || s.DESCRIPCION.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "codigo_banco":
                    bancos = bancos.OrderByDescending(s => s.CODIGO_BANCO);
                    break;
                case "descripcion":
                    bancos = bancos.OrderBy(s => s.DESCRIPCION);
                    break;
                default:
                    bancos = bancos.OrderBy(s => s.CODIGO_BANCO);
                    break;
            }
            //int pageSize = 10;
            //int pageNumber = (page ?? 1);

            ViewBag.CurrentFilter = searchString;
            return View(bancos);
        }

        // GET: BANCOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANCOS bANCOS = db.BANCOS.Find(id);
            if (bANCOS == null)
            {
                return HttpNotFound();
            }
            return View(bANCOS);
        }

        // GET: BANCOS/Create
        public ActionResult Create(int? id)
        {
            BANCOS bANCOS = db.BANCOS.Find(id);

            if (id > 0)
            {
                ViewBag.EsNuevo = 0;
            }
            else
            {
                ViewBag.EsNuevo = 1;
            }
            return View(bANCOS);
        }

        // POST: BANCOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CODIGO_BANCO,DESCRIPCION,CODIGO_ENVIO,TIPO,CONDICION")] BANCOS bANCOS, int EsNuevo)
        {
            //int maxcodbanco = (from s in db.BANCOS
            //             select s).Max(m=>m.CODIGO_BANCO);
            int ban = bANCOS.CODIGO_BANCO;

            if (ModelState.IsValid)
            {
                if (EsNuevo == 1)
                {
                    db.BANCOS.Add(bANCOS);
                }
                else
                {
                    db.Entry(bANCOS).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bANCOS);
        }

        // GET: BANCOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANCOS bANCOS = db.BANCOS.Find(id);
            if (bANCOS == null)
            {
                return HttpNotFound();
            }
            return View(bANCOS);
        }

        // POST: BANCOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CODIGO_BANCO,DESCRIPCION,CODIGO_ENVIO,TIPO,CONDICION")] BANCOS bANCOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bANCOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bANCOS);
        }

        // GET: BANCOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANCOS bANCOS = db.BANCOS.Find(id);
            if (bANCOS == null)
            {
                return HttpNotFound();
            }
            return View(bANCOS);
        }

        // POST: BANCOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BANCOS bANCOS = db.BANCOS.Find(id);
            db.BANCOS.Remove(bANCOS);
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
