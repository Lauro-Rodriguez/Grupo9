using SIGEFINew.Models;
using SIGEFINew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGEFINew.Controllers.Inventarios
{
    public class StockProductosController : Controller
    {
        // GET: StockProductos
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var query2 = from a in db.IN_PRODUCTOS
                         select new
                         {
                             COD_ITEM = a.COD_ITEM,
                             NOM_ITEM = a.NOM_ITEM,

                             CINI = (from c in db.IN_CARGA_INI
                                     where c.COD_ITEM == a.COD_ITEM
                                     select c.CANTIDAD).DefaultIfEmpty().Sum(),

                             ING = (from d in db.IN_DETALLETRANSAC
                                    where d.TIPO_TRAN == "I" && d.COD_ITEM == a.COD_ITEM
                                    select d.CANTIDAD).DefaultIfEmpty().Sum(),

                             EGR = (from d in db.IN_DETALLETRANSAC
                                    where d.TIPO_TRAN == "E" && d.COD_ITEM == a.COD_ITEM
                                    select d.CANTIDAD).DefaultIfEmpty().Sum(),

                             STOCK = (from c in db.IN_CARGA_INI
                                      where c.COD_ITEM == a.COD_ITEM
                                      select c.CANTIDAD).DefaultIfEmpty().Sum()+
                                      (float)(from d in db.IN_DETALLETRANSAC
                                              where d.TIPO_TRAN == "I" && d.COD_ITEM == a.COD_ITEM
                                              select d.CANTIDAD).DefaultIfEmpty().Sum()-
                                      (float)(from d in db.IN_DETALLETRANSAC
                                              where d.TIPO_TRAN == "E" && d.COD_ITEM == a.COD_ITEM
                                              select d.CANTIDAD).DefaultIfEmpty().Sum()
                         };

            List<StockProductos> Productos = new List<StockProductos>();

            foreach (var item in query2)
            {
                Productos.Add(new StockProductos()
                {
                    COD_ITEM = item.COD_ITEM,
                    NOM_ITEM = item.NOM_ITEM,
                    CINI = (float)item.CINI,
                    ING = (float)item.ING,
                    EGR = (float)item.EGR,
                    STOCK = (float)item.STOCK
                });
            }

            return View(Productos);
        }

        // GET: StockProductos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StockProductos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockProductos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
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

        // GET: StockProductos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StockProductos/Edit/5
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

        // GET: StockProductos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StockProductos/Delete/5
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
    }
}
