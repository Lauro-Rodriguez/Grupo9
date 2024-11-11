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
    public class IN_PRESENTAController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IN_PRESENTA
        public ActionResult Index(string CodCaract, string sortOrder, string currentFilter, string searchString, int? page)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            ViewBag.descrip_cagen = String.IsNullOrEmpty(sortOrder) ? "descrip_cagen" : "";
            ViewBag.descrip_cagen = sortOrder == "string" ? "descrip_cagen" : "string";

            var iN_PRESENTA = db.IN_PRESENTA.Include(i => i.IN_CARACGEN).Where(f => f.ID_INSTITUCION == IdInst
            && f.PERI_CODIGO == PeriCod);

            if (CodCaract != null)
            {
                iN_PRESENTA = db.IN_PRESENTA.Include(i => i.IN_CARACGEN).Where(f => f.ID_INSTITUCION == IdInst
                 && f.PERI_CODIGO == PeriCod && f.COD_CAGEN == CodCaract);
            }

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
                iN_PRESENTA = iN_PRESENTA.Where(s => s.DESCRIP_CAGEN.Contains(searchString)
                                       || s.IN_CARACGEN.DESCRIP_CAGEN.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "cod_presen":
                    iN_PRESENTA = iN_PRESENTA.OrderByDescending(s => s.COD_PRESEN);
                    break;
                case "descrip_cagen":
                    iN_PRESENTA = iN_PRESENTA.OrderBy(s => s.DESCRIP_CAGEN);
                    break;
                default:
                    iN_PRESENTA = iN_PRESENTA.OrderBy(s => s.COD_PRESEN);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentFilter = searchString;

            FillCaract("");
            return View(iN_PRESENTA.ToList());
        }

        private void FillCaract(string ValTipo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var iN_CARACGEN = db.IN_CARACGEN.Where(i => i.ID_INSTITUCION == IdInst &&
                  i.PERI_CODIGO == PeriCod && i.CARACT == "UM").ToList();

            iN_CARACGEN.Add(new IN_CARACGEN { COD_CAGEN = "", DESCRIP_CAGEN = "[Seleccione una Unidad de Medidad]" });

            ViewBag.COD_CAGEN = new SelectList(iN_CARACGEN.OrderBy(o => o.DESCRIP_CAGEN), "COD_CAGEN", "DESCRIP_CAGEN", ValTipo);
        }
        // GET: IN_PRESENTA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_PRESENTA iN_PRESENTA = db.IN_PRESENTA.Find(id);
            if (iN_PRESENTA == null)
            {
                return HttpNotFound();
            }
            return View(iN_PRESENTA);
        }

        // GET: IN_PRESENTA/Create
        public ActionResult Create(string id, int? id2)
        {
            string Caract = "";

            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_PRESENTA iN_PRESENTA = db.IN_PRESENTA.Find(IdInst, PeriCod, id, id2);
            if (id != null)
            {
                Caract = iN_PRESENTA.COD_CAGEN;
                ViewBag.EsNuevo = 0;
                ViewBag.CodPresen = iN_PRESENTA.COD_PRESEN;
                ViewBag.UserCrea = iN_PRESENTA.USER_CREA;
                ViewBag.FechaCrea = iN_PRESENTA.FECHA_CREA;
            }
            else
            {
                ViewBag.EsNuevo = 1;
                ViewBag.CodPresen = 0;
                ViewBag.UserCrea = Session["Usuario"].ToString();
                ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");
            }
            FillCaract(Caract);
            return View(iN_PRESENTA);
        }

        // POST: IN_PRESENTA/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,COD_CAGEN,COD_PRESEN,DESCRIP_CAGEN,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF")] IN_PRESENTA iN_PRESENTA, int EsNuevo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            iN_PRESENTA.ID_INSTITUCION = IdInst;
            iN_PRESENTA.PERI_CODIGO = PeriCod;

            int CodPres = 0;

            try
            {
                //var MaxCod = db.IN_PRESENTA.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod &&
                //f.COD_CAGEN == iN_PRESENTA.COD_CAGEN)
                //.First(x => x.COD_PRESEN == db.IN_PRESENTA.Max(y => y.COD_PRESEN));

                CodPres = (from nd in db.IN_PRESENTA
                           where nd.ID_INSTITUCION == IdInst && nd.PERI_CODIGO == PeriCod
                           && nd.COD_CAGEN == iN_PRESENTA.COD_CAGEN
                           select nd.COD_PRESEN).Max();
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
                        iN_PRESENTA.COD_PRESEN = CodPres + 1;
                        db.IN_PRESENTA.Add(iN_PRESENTA);
                    }
                    else
                    {
                        iN_PRESENTA.USER_MODIF = Session["Usuario"].ToString();
                        iN_PRESENTA.FECHA_MODIF = System.DateTime.Today;
                        db.Entry(iN_PRESENTA).State = EntityState.Modified;
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
                ViewBag.UserCrea = Session["Usuario"].ToString();
                ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");
                ViewBag.EsNuevo = 1;
            }

            FillCaract("");
            return View(iN_PRESENTA);
        }

        // GET: IN_PRESENTA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_PRESENTA iN_PRESENTA = db.IN_PRESENTA.Find(id);
            if (iN_PRESENTA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_INSTITUCION = new SelectList(db.IN_CARACGEN, "ID_INSTITUCION", "DESCRIP_CAGEN", iN_PRESENTA.ID_INSTITUCION);
            return View(iN_PRESENTA);
        }

        // POST: IN_PRESENTA/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,COD_CAGEN,COD_PRESEN,DESCRIP_CAGEN,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF")] IN_PRESENTA iN_PRESENTA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iN_PRESENTA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_INSTITUCION = new SelectList(db.IN_CARACGEN, "ID_INSTITUCION", "DESCRIP_CAGEN", iN_PRESENTA.ID_INSTITUCION);
            return View(iN_PRESENTA);
        }

        // GET: IN_PRESENTA/Delete/5
        public ActionResult Delete(string id, int id2)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_PRESENTA iN_PRESENTA = db.IN_PRESENTA.Find(IdInst, PeriCod, id, id2);
            if (iN_PRESENTA == null)
            {
                return HttpNotFound();
            }
            return View(iN_PRESENTA);
        }

        // POST: IN_PRESENTA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id, int id2)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            IN_PRESENTA iN_PRESENTA = db.IN_PRESENTA.Find(IdInst, PeriCod, id, id2);

            try
            {
                db.IN_PRESENTA.Remove(iN_PRESENTA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                ViewBag.MsgError = TempData["MsgError"];
            }

            return View(iN_PRESENTA);
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
