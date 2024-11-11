using SIGEFINew.Clases;
using SIGEFINew.Models;
using SIGEFINew.Models.Inventarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace SIGEFINew.Controllers.Inventarios
{
    public class IN_CARACGENController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IN_CARACGEN
        public ActionResult Index(string CodCaract, string sortOrder, string currentFilter, string searchString, int? page)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var iN_CARACGEN = db.IN_CARACGEN.Where(i => i.ID_INSTITUCION == IdInst &&
                  i.PERI_CODIGO == PeriCod);

            ViewBag.descrip_cagen = String.IsNullOrEmpty(sortOrder) ? "descrip_cagen" : "";
            ViewBag.descrip_cagen = sortOrder == "string" ? "descrip_cagen" : "string";

            if (CodCaract != null)
            {
                iN_CARACGEN = db.IN_CARACGEN.Where(i => i.ID_INSTITUCION == IdInst &&
                  i.PERI_CODIGO == PeriCod && i.CARACT == CodCaract);
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
                iN_CARACGEN = iN_CARACGEN.Where(s => s.DESCRIP_CAGEN.Contains(searchString)
                                       || s.COD_CAGEN.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "cod_cagen":
                    iN_CARACGEN = iN_CARACGEN.OrderByDescending(s => s.COD_CAGEN);
                    break;
                case "descrip_cagen":
                    iN_CARACGEN = iN_CARACGEN.OrderBy(s => s.DESCRIP_CAGEN);
                    break;
                default:
                    iN_CARACGEN = iN_CARACGEN.OrderBy(s => s.DESCRIP_CAGEN);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentFilter = searchString;

            FillTiposCarac("");
            return View(iN_CARACGEN.ToList());
        }

        private void FillTiposCarac(string ValTipo)
        {
            var TipoCaract = new List<Tipo_Caract>
            {
                new Tipo_Caract{CodCaract="",NomCaract="[Elija un Tipo]"},
                new Tipo_Caract{CodCaract="UM",NomCaract="UNIDAD DE MEDIDA"},
                new Tipo_Caract{CodCaract="NG",NomCaract="NOMBRES GENERICOS"},
                new Tipo_Caract{CodCaract="EP",NomCaract="ESTADO DEL PRODUCTO"},
                //new Tipo_Caract{CodCaract="TT",NomCaract="TIPO DE TRANSACCION"},
                new Tipo_Caract{CodCaract="FC",NomCaract="FACTORES DE CONVERSION"}
            };

            ViewBag.CARACT = new SelectList(TipoCaract, "CodCaract", "NomCaract", ValTipo);
        }

        // GET: IN_CARACGEN/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_CARACGEN iN_CARACGEN = db.IN_CARACGEN.Find(id);
            if (iN_CARACGEN == null)
            {
                return HttpNotFound();
            }
            return View(iN_CARACGEN);
        }

        // GET: IN_CARACGEN/Create
        public ActionResult Create(string id)
        {
            string Caract = "";
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_CARACGEN iN_CARACGEN = db.IN_CARACGEN.Find(IdInst, PeriCod, id);
            if (id != null)
            {
                Caract = iN_CARACGEN.CARACT;
                ViewBag.EsNuevo = 0;
                ViewBag.UserCrea = iN_CARACGEN.USER_CREA;
                ViewBag.FechaCrea = iN_CARACGEN.FECHA_CREA;
            }
            else
            {
                ViewBag.EsNuevo = 1;
                ViewBag.UserCrea = Session["Usuario"].ToString();
                ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");
            }
            FillTiposCarac(Caract);
            return View(iN_CARACGEN);
        }

        // POST: IN_CARACGEN/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,COD_CAGEN,DESCRIP_CAGEN,CARACT,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF")] IN_CARACGEN iN_CARACGEN, int EsNuevo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            iN_CARACGEN.ID_INSTITUCION = IdInst;
            iN_CARACGEN.PERI_CODIGO = PeriCod;
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (EsNuevo == 1)
                    {
                        db.IN_CARACGEN.Add(iN_CARACGEN);
                    }
                    else
                    {
                        iN_CARACGEN.USER_MODIF = Session["Usuario"].ToString();
                        iN_CARACGEN.FECHA_MODIF = System.DateTime.Today;
                        db.Entry(iN_CARACGEN).State = EntityState.Modified;
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
            FillTiposCarac("");
            return View(iN_CARACGEN);
        }

        // GET: IN_CARACGEN/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_CARACGEN iN_CARACGEN = db.IN_CARACGEN.Find(id);
            if (iN_CARACGEN == null)
            {
                return HttpNotFound();
            }
            return View(iN_CARACGEN);
        }

        // POST: IN_CARACGEN/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,COD_CAGEN,DESCRIP_CAGEN,CARACT,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF")] IN_CARACGEN iN_CARACGEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iN_CARACGEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iN_CARACGEN);
        }

        // GET: IN_CARACGEN/Delete/5
        public ActionResult Delete(string id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_CARACGEN iN_CARACGEN = db.IN_CARACGEN.Find(IdInst, PeriCod, id);
            if (iN_CARACGEN == null)
            {
                return HttpNotFound();
            }
            return View(iN_CARACGEN);
        }

        // POST: IN_CARACGEN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            IN_CARACGEN iN_CARACGEN = db.IN_CARACGEN.Find(IdInst, PeriCod, id);

            try
            {
                db.IN_CARACGEN.Remove(iN_CARACGEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                ViewBag.MsgError = TempData["MsgError"];
            }

            return View(iN_CARACGEN);

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
