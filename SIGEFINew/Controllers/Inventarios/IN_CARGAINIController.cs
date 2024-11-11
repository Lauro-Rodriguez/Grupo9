using SIGEFINew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGEFINew.Models.Inventarios;
using SIGEFINew.ViewModels;
using System.Data.Entity;
namespace SIGEFINew.Controllers.Inventarios
{
    public class IN_CARGAINIController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: IN_CARGAINI
        public ActionResult Index(int? ID_BODEGA)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            string UserCrea = Session["Usuario"].ToString();
            DateTime FechaCrea = DateTime.Today;

            var Bodegas = db.IN_BODEGAS.Where(f=>f.ID_INSTITUCION==IdInst && f.PERI_CODIGO==PeriCod).ToList();
            ViewBag.ID_BODEGA = new SelectList(Bodegas, "ID_BODEGA", "NOM_BODEGA");

            //var CargaIni = db.IN_CARGA_INI.Include(a => a.IN_ITEMS).Where(de => de.ID_INSTITUCION == IdInst &&
            //  de.PERI_CODIGO == PeriCod && de.ID_BODEGA == ID_BODEGA).ToList();

            List<ASIGNA_CARGAINI> Items = new List<ASIGNA_CARGAINI>();
            
            var t = from c in db.IN_ITEMS
                    where c.ID_BODEGA==ID_BODEGA && !db.IN_CARGA_INI.Any(p => p.ID_INSTITUCION == IdInst && p.PERI_CODIGO == PeriCod &&
                    p.ID_BODEGA == c.ID_BODEGA && p.COD_ITEM == c.COD_ITEM )
                    select c;

            foreach (var Polit in t)
            {
                //IN_CARGA_INI CargaIni = new IN_CARGA_INI();
                Items.Add(new ASIGNA_CARGAINI()
                {
                    ID_INSTITUCION = Polit.ID_INSTITUCION,
                    PERI_CODIGO = Polit.PERI_CODIGO,
                    ID_BODEGA = Polit.ID_BODEGA,
                    COD_ITEM=Polit.COD_ITEM.Trim(),
                    NOM_ITEM=Polit.NOM_ITEM,
                    CANTIDAD=0,
                    COSTO=0,
                    VALOR_TOTAL=0
                });

                //CargaIni.ID_INSTITUCION = IdInst;
                //CargaIni.PERI_CODIGO=PeriCod;
                //CargaIni.ID_BODEGA = Polit.ID_BODEGA;
                //CargaIni.COD_ITEM = Polit.COD_ITEM;
                //CargaIni.CANTIDAD = 0;
                //CargaIni.COSTO = 0;
                //CargaIni.VALOR_TOTAL = 0;
                //CargaIni.USER_CREA = UserCrea;
                //CargaIni.FECHA_CREA = FechaCrea;
                //db.IN_CARGA_INI.Add(CargaIni);
                
            }
            //db.SaveChanges();
            return View(Items);
        }

        // GET: IN_CARGAINI/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IN_CARGAINI/Create
        public ActionResult Create(int? ID_BODEGA)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            string UserCrea = Session["Usuario"].ToString();
            DateTime FechaCrea = DateTime.Today;

            var Bodegas = db.IN_BODEGAS.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod).ToList();
            ViewBag.ID_BODEGA = new SelectList(Bodegas, "ID_BODEGA", "NOM_BODEGA");

            //var CargaIni = db.IN_CARGA_INI.Include(a => a.IN_ITEMS).Where(de => de.ID_INSTITUCION == IdInst &&
            //  de.PERI_CODIGO == PeriCod && de.ID_BODEGA == ID_BODEGA).ToList();

            List<ASIGNA_CARGAINI> Items = new List<ASIGNA_CARGAINI>();

            var t = from c in db.IN_ITEMS
                    where c.ID_BODEGA == ID_BODEGA && !db.IN_CARGA_INI.Any(p => p.ID_INSTITUCION == IdInst && p.PERI_CODIGO == PeriCod &&
                      p.ID_BODEGA == c.ID_BODEGA && p.COD_ITEM == c.COD_ITEM)
                    select c;

            foreach (var Polit in t)
            {
                //IN_CARGA_INI CargaIni = new IN_CARGA_INI();
                Items.Add(new ASIGNA_CARGAINI()
                {
                    ID_INSTITUCION = Polit.ID_INSTITUCION,
                    PERI_CODIGO = Polit.PERI_CODIGO,
                    ID_BODEGA = Polit.ID_BODEGA,
                    COD_ITEM = Polit.COD_ITEM,
                    NOM_ITEM = Polit.NOM_ITEM,
                    CANTIDAD = 0,
                    COSTO = 0,
                    VALOR_TOTAL = 0
                });

                //CargaIni.ID_INSTITUCION = IdInst;
                //CargaIni.PERI_CODIGO=PeriCod;
                //CargaIni.ID_BODEGA = Polit.ID_BODEGA;
                //CargaIni.COD_ITEM = Polit.COD_ITEM;
                //CargaIni.CANTIDAD = 0;
                //CargaIni.COSTO = 0;
                //CargaIni.VALOR_TOTAL = 0;
                //CargaIni.USER_CREA = UserCrea;
                //CargaIni.FECHA_CREA = FechaCrea;
                //db.IN_CARGA_INI.Add(CargaIni);

            }
            //db.SaveChanges();
            return View(Items);
        }

        // POST: IN_CARGAINI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<ASIGNA_CARGAINI> course)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // GET: IN_CARGAINI/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IN_CARGAINI/Edit/5
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

        // GET: IN_CARGAINI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IN_CARGAINI/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Save(int EsNuevo,int Reajuste,int NumPedido, ASIGNA_CARGAINI[] order)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            string Anio = Session["Anio"].ToString();

            string UserCrea = Session["Usuario"].ToString();
            DateTime FechaCrea = DateTime.Today;

            var NumCert = (from NC in db.CO_PARAMETROS
                           where NC.ID_INSTITUCION == IdInst && NC.PERI_CODIGO == PeriCod
                           && NC.ANIO_CODIGO == Anio
                           select NC).FirstOrDefault();

            int NumCertD = NumCert.NUM_CERTDISP + 1;
            int NumTran = NumCert.NUM_TRANSAC + 1;

            NumCert.NUM_CERTDISP = NumCertD;
            NumCert.NUM_TRANSAC = NumTran;

            if (Session["Usuario"].ToString() != "")
            {
                string result = "Error! Order Is Not Complete!";

                foreach (var item in order)
                {
                    if (item.VALOR_TOTAL > 0)
                    {
                        IN_CARGA_INI O = new IN_CARGA_INI();
                        O.ID_INSTITUCION = IdInst;
                        O.PERI_CODIGO = PeriCod;
                        O.ID_BODEGA = item.ID_BODEGA;
                        O.COD_ITEM = item.COD_ITEM.Replace(" ","").Replace("\n","");
                        O.CANTIDAD = item.CANTIDAD;
                        O.COSTO = item.COSTO;
                        O.VALOR_TOTAL = item.VALOR_TOTAL;
                        O.USER_CREA = UserCrea;
                        O.FECHA_CREA = FechaCrea;
                        db.IN_CARGA_INI.Add(O);
                    }
                }

                try
                {
                    db.SaveChanges();
                    TempData["success"] = "Los datos se guardaron correctamente!!";
                    TempData["NUM_CERTIF"] = NumCertD;
                    result = "Los Datos se Guardaron Satisfactoriamente!";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception Ex)
                {

                }

                //}
                //return RedirectToAction("Index");
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }
    }
}
