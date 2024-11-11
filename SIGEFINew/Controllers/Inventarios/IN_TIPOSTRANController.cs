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
    public class IN_TIPOSTRANController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IN_TIPOSTRAN
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            ViewBag.descrip_tipotran = String.IsNullOrEmpty(sortOrder) ? "descrip_tipotran" : "";
            ViewBag.descrip_tipotran = sortOrder == "string" ? "descrip_tipotran" : "string";

            var iN_TIPOSTRAN = db.IN_TIPOSTRAN.Where(i => i.ID_INSTITUCION == IdInst && i.PERI_CODIGO == PeriCod);

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
                iN_TIPOSTRAN = iN_TIPOSTRAN.Where(s => s.DESCRIP_TIPOTRAN.Contains(searchString)
                                       || s.DESCRIP_TIPOTRAN.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "cod_tipotran":
                    iN_TIPOSTRAN = iN_TIPOSTRAN.OrderByDescending(s => s.COD_TIPOTRAN);
                    break;
                case "descrip_tipotran":
                    iN_TIPOSTRAN = iN_TIPOSTRAN.OrderBy(s => s.DESCRIP_TIPOTRAN);
                    break;
                default:
                    iN_TIPOSTRAN = iN_TIPOSTRAN.OrderBy(s => s.COD_TIPOTRAN);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentFilter = searchString;

            return View(iN_TIPOSTRAN.ToList());
        }

        // GET: IN_TIPOSTRAN/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_TIPOSTRAN iN_TIPOSTRAN = db.IN_TIPOSTRAN.Find(id);
            if (iN_TIPOSTRAN == null)
            {
                return HttpNotFound();
            }
            return View(iN_TIPOSTRAN);
        }

        private void FillTiposMov(string ValTipo)
        {
            var TipoCaract = new List<Tipo_Caract>
            {
                new Tipo_Caract{CodCaract="",NomCaract="[Elija un Tipo]"}, 
                new Tipo_Caract{CodCaract="AV",NomCaract="Ajuste Valores"},//1 //Contabilidad y Con Donante
                new Tipo_Caract{CodCaract="CO",NomCaract="Compra"},//2 //Las 4 opciones
                new Tipo_Caract{CodCaract="CE",NomCaract="Consignación Entregada"},//3 //Las 4 opciones
                new Tipo_Caract{CodCaract="CR",NomCaract="Consignación Recibida"},//4 //Las 4 opciones
                new Tipo_Caract{CodCaract="CI",NomCaract="Consumo Interno"},//5 //Contabilidad y Con Donante
                new Tipo_Caract{CodCaract="DO",NomCaract="Donación"}, //6 //Contabilidad y Con Donante
                new Tipo_Caract{CodCaract="GA",NomCaract="Garantía"}, //7 //Contabilidad y Con Donante
                new Tipo_Caract{CodCaract="IN",NomCaract="Inversión"},//8 //Contabilidad y Con Donante
                new Tipo_Caract{CodCaract="OT",NomCaract="Otros"}, //9 //Contabilidad y Con Donante
                new Tipo_Caract{CodCaract="PR",NomCaract="Préstamo"}, //10 //Contabilidad y Con Donante
                new Tipo_Caract{CodCaract="PD",NomCaract="Producción"}, //11 //Contabilidad y Con Donante
                new Tipo_Caract{CodCaract="TR",NomCaract="Transferencia"}, //12 //Contabilidad y Con Donante
                new Tipo_Caract{CodCaract="VE",NomCaract="Venta"} //13 //Contabilidad y Con Donante
            };

            ViewBag.TIPO_MOV = new SelectList(TipoCaract, "CodCaract", "NomCaract", ValTipo);
        }

        // GET: IN_TIPOSTRAN/Create
        public ActionResult Create(int? id)
        {
            string Caract = "";

            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_TIPOSTRAN iN_TIPOSTRAN = db.IN_TIPOSTRAN.Find(IdInst, PeriCod, id);
            if (id != null)
            {
                Caract = iN_TIPOSTRAN.TIPO_MOV;
                ViewBag.EsNuevo = 0;
                ViewBag.UserCrea = iN_TIPOSTRAN.USER_CREA;
                ViewBag.FechaCrea = iN_TIPOSTRAN.FECHA_CREA;
            }
            else
            {
                ViewBag.EsNuevo = 1;
                ViewBag.UserCrea = Session["Usuario"].ToString();
                ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");
            }
            FillTiposMov(Caract);
            return View(iN_TIPOSTRAN);
        }

        // POST: IN_TIPOSTRAN/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,COD_TIPOTRAN,TIPO_MOV,DESCRIP_TIPOTRAN,ENV_CO_CC,CON_IVA,CON_DONANTE,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF")] IN_TIPOSTRAN iN_TIPOSTRAN, int EsNuevo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            iN_TIPOSTRAN.ID_INSTITUCION = IdInst;
            iN_TIPOSTRAN.PERI_CODIGO = PeriCod;

            int CodPres = 0;

            try
            {
                CodPres = (from nd in db.IN_TIPOSTRAN
                           where nd.ID_INSTITUCION == IdInst && nd.PERI_CODIGO == PeriCod
                           select nd.COD_TIPOTRAN).Max();
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
                        iN_TIPOSTRAN.COD_TIPOTRAN = CodPres + 1;
                        db.IN_TIPOSTRAN.Add(iN_TIPOSTRAN);
                    }
                    else
                    {
                        iN_TIPOSTRAN.USER_MODIF = Session["Usuario"].ToString();
                        iN_TIPOSTRAN.FECHA_MODIF = System.DateTime.Today;
                        db.Entry(iN_TIPOSTRAN).State = EntityState.Modified;
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

            FillTiposMov("");
            return View(iN_TIPOSTRAN);
        }

        // GET: IN_TIPOSTRAN/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_TIPOSTRAN iN_TIPOSTRAN = db.IN_TIPOSTRAN.Find(id);
            if (iN_TIPOSTRAN == null)
            {
                return HttpNotFound();
            }
            return View(iN_TIPOSTRAN);
        }

        // POST: IN_TIPOSTRAN/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,COD_TIPOTRAN,TIPO_MOV,DESCRIP_TIPOTRAN,ENV_CO_CC,CON_IVA,CON_DONANTE,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF")] IN_TIPOSTRAN iN_TIPOSTRAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iN_TIPOSTRAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iN_TIPOSTRAN);
        }

        // GET: IN_TIPOSTRAN/Delete/5
        public ActionResult Delete(int? id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_TIPOSTRAN iN_TIPOSTRAN = db.IN_TIPOSTRAN.Find(IdInst, PeriCod, id);
            if (iN_TIPOSTRAN == null)
            {
                return HttpNotFound();
            }

            return View(iN_TIPOSTRAN);
        }

        // POST: IN_TIPOSTRAN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_TIPOSTRAN iN_TIPOSTRAN = db.IN_TIPOSTRAN.Find(IdInst, PeriCod, id);
            db.IN_TIPOSTRAN.Remove(iN_TIPOSTRAN);
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
