using SIGEFINew.Models;
using System.Web.Mvc;

namespace SIGEFINew.Controllers
{
    public class CGCuentasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CGCuentas
        //public ActionResult Index()
        //{
        //    return View(db.CGCuentas.ToList());
        //}

        // GET: CGCuentas/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CGCuentas cGCuentas = db.CGCuentas.Find(id);
        //    if (cGCuentas == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cGCuentas);
        //}

        // GET: CGCuentas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CGCuentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,CODIGO_CG,NOMBRE_CG")] CGCuentas cGCuentas)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.CGCuentas.Add(cGCuentas);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(cGCuentas);
        //}

        // GET: CGCuentas/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CGCuentas cGCuentas = db.CGCuentas.Find(id);
        //    if (cGCuentas == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cGCuentas);
        //}

        // POST: CGCuentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,CODIGO_CG,NOMBRE_CG")] CGCuentas cGCuentas)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(cGCuentas).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(cGCuentas);
        //}

        // GET: CGCuentas/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CGCuentas cGCuentas = db.CGCuentas.Find(id);
        //    if (cGCuentas == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cGCuentas);
        //}

        // POST: CGCuentas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CGCuentas cGCuentas = db.CGCuentas.Find(id);
        //    db.CGCuentas.Remove(cGCuentas);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
