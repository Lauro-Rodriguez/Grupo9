using SIGEFINew.Clases;
using SIGEFINew.Models;
using SIGEFINew.Models.Inventarios;
using SIGEFINew.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SIGEFINew.Controllers.Inventarios
{
    [Authorize(Roles = "Master,IN_COMPRAS")]
    public class IN_COMPRASController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: IN_COMPRAS
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);
            string Usuario = Session["Usuario"].ToString();
            var iN_COMPRAS = db.IN_COMPRAS.Where(f=>f.ID_INSTITUCION==IdInst
            && f.PERI_CODIGO==Pericod).Include(i => i.Periodos).Include(i => i.Proveedores);

            ViewBag.Nombre = String.IsNullOrEmpty(sortOrder) ? "nombre" : "";
            ViewBag.Nombre = sortOrder == "string" ? "nombre" : "string";

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
                iN_COMPRAS = iN_COMPRAS.Where(s => s.Proveedores.NOMBRE.Contains(searchString)
                                       || s.NUM_DOC.Contains(searchString)
                                       || s.DETALLE.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nombre":
                    iN_COMPRAS = iN_COMPRAS.OrderByDescending(s => s.Proveedores.NOMBRE);
                    break;
                case "fecha_doc":
                    iN_COMPRAS = iN_COMPRAS.OrderBy(s => s.FECHA_DOC);
                    break;
                default:
                    iN_COMPRAS = iN_COMPRAS.OrderBy(s => s.NUM_DOC);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentFilter = searchString;

            return View(iN_COMPRAS.ToList());
        }

        public ActionResult AddNote()
        {
            return PartialView("_Editar");
        }

        [HttpPost]
        public ActionResult AddNote(IN_COMPRAS model)
        {
            return RedirectToAction("Index");
        }
        // GET: IN_COMPRAS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_COMPRAS iN_COMPRAS = db.IN_COMPRAS.Find(id);
            if (iN_COMPRAS == null)
            {
                return HttpNotFound();
            }
            return View(iN_COMPRAS);
        }

        // GET: IN_COMPRAS/Create
        public ActionResult Create(int? id,int? NumCompra)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            string Usuario = Session["Usuario"].ToString();

            var Proveed = db.Proveedores.ToList();

            IN_COMPRAS iN_COMPRAS = db.IN_COMPRAS.Find(IdInst, PeriCod, id, NumCompra);

            var Bodegas = (from c in db.IN_BODEGAS
                     join a in db.IN_USERSXBOD
                     on c.ID_BODEGA equals a.ID_BODEGA
                     where c.ID_INSTITUCION==IdInst && c.PERI_CODIGO==PeriCod && a.USUA_LOGIN==Usuario
                          select c).ToList();

            Bodegas.Add(new IN_BODEGAS { ID_BODEGA = 0, NOM_BODEGA = "[Seleccione una Bodega]" });

            //var Compromisos = (from p in db.PR_MOVGASTO
            //                   where p.ID_INSTITUCION == IdInst && p.PERI_CODIGO == PeriCod && p.ANULADO == false 
            //                   && p.TIPO_DOCUM == "CO"
            //                   select p).ToList();

            //Compromisos.Add(new Models.Presupuesto.PR_MOVGASTO { NUM_COMPROMISO = 0, COD_PROV = "[Compromisos]" });

            Proveed.Add(new Proveedores { COD_PROV = "0", NOMBRE = "[Seleccione un Proveedor]" });

            var mPolit = (from Item in db.IN_PRODUCTOS
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod //&& Item.TIPO == "D"
                          select Item).OrderBy(O => O.NOM_ITEM).ToList();

            mPolit.Add(new IN_PRODUCTOS {COD_ITEM = "", NOM_ITEM = "[Seleccione un Producto]" });
            ViewBag.COD_ITEM = new SelectList(mPolit.OrderBy(o => o.NOM_ITEM), "COD_ITEM", "NOM_ITEM");
            //         var lst = new List<AsignaProductos>
            //{
            //    new AsignaProductos {CAT_CODIGO = "1", CANTIDAD = 1},
            //    new AsignaProductos {CAT_CODIGO = "2", CANTIDAD = 2},
            //    new AsignaProductos {CAT_CODIGO = "3", CANTIDAD = 3},
            //};
            //         ViewBag.Prods = lst;
            if (id != null)
            {
                ViewBag.EsNuevo = 0;
                ViewBag.COD_PROV = new SelectList(Proveed.OrderBy(o => o.NOMBRE), "COD_PROV", "NOMBRE",iN_COMPRAS.COD_PROV);
                ViewBag.ID_BODEGA = new SelectList(Bodegas.OrderBy(o => o.NOM_BODEGA), "ID_BODEGA", "NOM_BODEGA",iN_COMPRAS.ID_BODEGA);

                var DetProd = db.IN_DETCOMPRA.Where(f => f.ID_INSTITUCION==IdInst && f.PERI_CODIGO == PeriCod && f.ID_BODEGA == id
                      && f.NUM_COMPRA == NumCompra).ToList();

                foreach (var Polit in DetProd)
                {
                    AddDetCompra(Polit.NUM_FILA, Polit.COD_ITEM, Polit.PORC_IVA, Polit.CANTIDAD, Polit.COSTO_UNIT,
                        Polit.SUBTOTAL, Polit.COSTO_TOTAL, Polit.VAL_IVA);
                }

            }
            else
            {
                ViewBag.COD_PROV = new SelectList(Proveed.OrderBy(o => o.NOMBRE), "COD_PROV", "NOMBRE");
                ViewBag.ID_BODEGA = new SelectList(Bodegas.OrderBy(o => o.NOM_BODEGA), "ID_BODEGA", "NOM_BODEGA");
                ViewBag.EsNuevo = 1;
            }

            ViewBag.UserCrea = Session["Usuario"].ToString();
            ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");

            

            return View(iN_COMPRAS);
        }

        // POST: IN_COMPRAS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,ID_BODEGA,NUM_COMPRA,NUM_COMPROMISO,FECHA_COMPRA,COD_TIPO_DOC,COD_PROV,DETALLE,TIPO_SRI,FECHA_DOC,NUM_DOC,AUTORIZACION,FECHACAD_DOC,USER_CREA,FECHA_CREA")] IN_COMPRAS iN_COMPRAS,int EsNuevo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            string Usuario = Session["Usuario"].ToString();

            iN_COMPRAS.ID_INSTITUCION = IdInst;
            iN_COMPRAS.PERI_CODIGO = PeriCod;

            //List<IN_COMPRAS> Items = new List<IN_COMPRAS>();

            var Products = Session["Productos"] as List<AsignaProductos>;
            if (Products == null)
            {
                LLenaCombos();
                ViewBag.EsNuevo = 1;
                ViewBag.UserCrea = Session["Usuario"].ToString();
                ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");
                return View(iN_COMPRAS);
            }
            int NumDisp = 0;
            try
            {
                if (EsNuevo==1)
                {
                    NumDisp = (from nd in db.IN_COMPRAS
                               where nd.ID_INSTITUCION == IdInst && nd.PERI_CODIGO == PeriCod
                               select nd.NUM_COMPRA).Max();
                }
                
            }
            catch
            {

            }

            if (EsNuevo == 1)
            {
                iN_COMPRAS.NUM_COMPRA = NumDisp + 1;
            }
            

            //foreach (var item in Items)
            //{
            //    Items.Add(new IN_COMPRAS()
            //    {
            //        ID_INSTITUCION = iN_COMPRAS.ID_INSTITUCION,
            //        PERI_CODIGO = iN_COMPRAS.PERI_CODIGO,
            //        NUM_COMPRA= iN_COMPRAS.NUM_COMPRA,
            //        NUM_COMPROMISO= iN_COMPRAS.NUM_COMPROMISO,
            //        FECHA_COMPRA= iN_COMPRAS.FECHA_COMPRA,
            //        COD_TIPO_DOC= iN_COMPRAS.COD_TIPO_DOC,
            //        COD_PROV= iN_COMPRAS.COD_PROV,
            //        DETALLE= iN_COMPRAS.DETALLE,
            //        TIPO_SRI= iN_COMPRAS.TIPO_SRI,
            //        FECHA_DOC= iN_COMPRAS.FECHA_DOC,
            //        NUM_DOC= iN_COMPRAS.NUM_DOC,
            //        AUTORIZACION= iN_COMPRAS.AUTORIZACION,
            //        FECHACAD_DOC= iN_COMPRAS.FECHACAD_DOC,
            //        USER_CREA= iN_COMPRAS.USER_CREA,
            //        FECHA_CREA= iN_COMPRAS.FECHA_CREA
            //    });
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    if (EsNuevo == 1)
                    {
                        db.IN_COMPRAS.Add(iN_COMPRAS);
                    }
                    else
                    {
                        var DetC = db.IN_DETCOMPRA.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod && f.ID_BODEGA == iN_COMPRAS.ID_BODEGA
                      && f.NUM_COMPRA == iN_COMPRAS.NUM_COMPRA);

                        db.IN_DETCOMPRA.RemoveRange(DetC);

                        db.Entry(iN_COMPRAS).State = EntityState.Modified;
                    }

                    foreach (var course in Products)
                    {
                        var Productos = new IN_DETCOMPRA();
                        
                        Productos.ID_INSTITUCION = IdInst;
                        Productos.PERI_CODIGO = PeriCod;
                        Productos.ID_BODEGA = iN_COMPRAS.ID_BODEGA;
                        if (EsNuevo == 1)
                        {
                            Productos.NUM_COMPRA = NumDisp + 1;
                        }
                        else
                        {
                            Productos.NUM_COMPRA = iN_COMPRAS.NUM_COMPRA;
                        }
                        Productos.NUM_FILA = course.NUM_FILA;
                        Productos.COD_ITEM = course.COD_ITEM;
                        if (course.PORC_IVA == 12)
                        {
                            Productos.APLICA_IVA = true;
                        }
                        else
                        {
                            Productos.APLICA_IVA = false;
                        }
                        Productos.PORC_IVA = course.PORC_IVA;
                        Productos.CANTIDAD = course.CANTIDAD;
                        Productos.COSTO_UNIT = course.COSTO_UNIT;
                        Productos.SUBTOTAL = course.SUBTOTAL;
                        Productos.COSTO_TOTAL = course.COSTO_TOTAL;
                        Productos.VAL_IVA = course.VAL_IVA;
                        db.IN_DETCOMPRA.Add(Productos);
                    }

                    db.SaveChanges();
                }
                catch (Exception ex) //(DbEntityValidationException ex)
                {
                    //foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    //{
                    //    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    //    {
                    //        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    //    }
                    //}
                    string msgErr = ex.InnerException.InnerException.Message;

                    TempData["MsgError"] = msgErr;
                    ViewBag.MsgError = TempData["MsgError"];

                    Products.RemoveAll(x => x.NUM_FILA > 0);

                    Session["Productos"] = Products;

                    LLenaCombos();
                    ViewBag.UserCrea = Session["Usuario"].ToString();
                    ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");
                    ViewBag.EsNuevo = 1;
                    return View(iN_COMPRAS);
                }

                Products.RemoveAll(x => x.NUM_FILA > 0);
                Session["Productos"] = Products;
                return RedirectToAction("Index");
            }
            LLenaCombos();
            ViewBag.UserCrea = Session["Usuario"].ToString();
            ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");
            ViewBag.EsNuevo = 1;

            var mPolit = (from Item in db.IN_PRODUCTOS
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod //&& Item.TIPO == "D"
                          select Item).OrderBy(O => O.NOM_ITEM).ToList();

            mPolit.Add(new IN_PRODUCTOS { COD_ITEM = "", NOM_ITEM = "[Seleccione un Producto]" });
            ViewBag.COD_ITEM = new SelectList(mPolit.OrderBy(o => o.NOM_ITEM), "COD_ITEM", "NOM_ITEM");

            
            var Bodegas = (from c in db.IN_BODEGAS
                           join a in db.IN_USERSXBOD
                           on c.ID_BODEGA equals a.ID_BODEGA
                           where c.ID_INSTITUCION == IdInst && c.PERI_CODIGO == PeriCod && a.USUA_LOGIN == Usuario
                           select c).ToList();

            Bodegas.Add(new IN_BODEGAS { ID_BODEGA = 0, NOM_BODEGA = "[Seleccione una Bodega]" });
            ViewBag.ID_BODEGA = new SelectList(Bodegas.OrderBy(o => o.NOM_BODEGA), "ID_BODEGA", "NOM_BODEGA",iN_COMPRAS.ID_BODEGA);

            return View(iN_COMPRAS);
        }

        private void LLenaCombos()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var Proveed = db.Proveedores.ToList();

            var Compromisos = (from p in db.PR_MOVGASTO
                               where p.ID_INSTITUCION == IdInst &&
                               p.PERI_CODIGO == PeriCod && p.ANULADO == false && p.TIPO_DOCUM == "CO"
                               select p).ToList();

            Compromisos.Add(new Models.Presupuesto.PR_MOVGASTO { NUM_COMPROMISO = 0, COD_PROV = "[Compromisos]" });
            Proveed.Add(new Proveedores { COD_PROV = "0", NOMBRE = "[Seleccione un Proveedor]" });

            ViewBag.COD_PROV = new SelectList(Proveed.OrderBy(o => o.NOMBRE), "COD_PROV", "NOMBRE");
            ViewBag.NUM_COMPROMISO = new SelectList(Compromisos.OrderBy(o => o.NUM_COMPROMISO), "NUM_COMPROMISO", "NUM_COMPROMISO");
        }

        // GET: IN_COMPRAS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_COMPRAS iN_COMPRAS = db.IN_COMPRAS.Find(id);
            if (iN_COMPRAS == null)
            {
                return HttpNotFound();
            }
            ViewBag.COD_PROV = new SelectList(db.Proveedores, "COD_PROV", "NUMCEDRUC_PROV", iN_COMPRAS.COD_PROV);
            return View(iN_COMPRAS);
        }

        // POST: IN_COMPRAS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,NUM_COMPRA,NUM_COMPROMISO,FECHA_COMPRA,COD_TIPO_DOC,COD_PROV,DETALLE,TIPO_SRI,FECHA_DOC,NUM_DOC,AUTORIZACION,FECHACAD_DOC,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF")] IN_COMPRAS iN_COMPRAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iN_COMPRAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.COD_PROV = new SelectList(db.Proveedores, "COD_PROV", "NUMCEDRUC_PROV", iN_COMPRAS.COD_PROV);
            return View(iN_COMPRAS);
        }

        // GET: IN_COMPRAS/Delete/5
        public ActionResult Delete(int? id, int? NumCompra)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_COMPRAS iN_COMPRAS = db.IN_COMPRAS.Find(IdInst, PeriCod, id, NumCompra);

            if (iN_COMPRAS == null)
            {
                return HttpNotFound();
            }
            return View(iN_COMPRAS);
        }

        // POST: IN_COMPRAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int NumCompra)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            IN_COMPRAS iN_COMPRAS = db.IN_COMPRAS.Find(IdInst, PeriCod, id, NumCompra);

            db.IN_COMPRAS.Remove(iN_COMPRAS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Save2(int NumComp, DateTime Fecha, string CodTipDoc,
           string CodProv, String Detalle, string TipoSRI, DateTime FechaDoc)
        {
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Save(int NumComp, DateTime Fecha, string CodTipDoc,
            string CodProv, String Detalle, string TipoSRI, DateTime FechaDoc,
            string NumDoc, string Autoriza, DateTime FechaCadDoc, IN_DETCOMPRA[] order)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            string Anio = Session["Anio"].ToString();

            var NumCert = (from NC in db.CO_PARAMETROS
                           where NC.ID_INSTITUCION == IdInst && NC.PERI_CODIGO == PeriCod
                           && NC.ANIO_CODIGO == Anio
                           select NC).FirstOrDefault();

            int NumCertD = NumCert.NUM_COMPROMISO + 1;
            int NumTran = NumCert.NUM_TRANSAC + 1;

            NumCert.NUM_COMPROMISO = NumCertD;
            NumCert.NUM_TRANSAC = NumTran;

            if (Session["Usuario"].ToString() != "")
            {
                string result = "Error! Order Is Not Complete!";
                //if (name != null && address != null && order != null)
                //{
                IN_COMPRAS model = new IN_COMPRAS();

                model.ID_INSTITUCION = IdInst;
                model.PERI_CODIGO = PeriCod;
                model.NUM_COMPRA = 1;
                model.NUM_COMPROMISO = NumComp;
                model.FECHA_COMPRA = Fecha;
                model.COD_TIPO_DOC = CodTipDoc;
                model.COD_PROV = CodProv;
                model.DETALLE = Detalle;
                model.TIPO_SRI = TipoSRI;
                model.FECHA_DOC = FechaDoc;
                model.NUM_DOC = NumDoc;
                model.AUTORIZACION = Autoriza;
                model.FECHACAD_DOC = FechaCadDoc;
                model.USER_CREA = Session["Usuario"].ToString();
                model.FECHA_CREA = DateTime.Now;
                db.IN_COMPRAS.Add(model);

                int NumFila = 1;
                foreach (var item in order)
                {
                    IN_DETCOMPRA O = new IN_DETCOMPRA();
                    O.ID_INSTITUCION = IdInst;
                    O.PERI_CODIGO = PeriCod;
                    O.NUM_COMPRA = 1;
                    O.NUM_FILA = NumFila;
                    O.COD_ITEM = item.COD_ITEM;
                    O.APLICA_IVA = item.APLICA_IVA;
                    O.CANTIDAD = item.CANTIDAD;
                    O.COSTO_UNIT = item.COSTO_UNIT;
                    O.SUBTOTAL = item.SUBTOTAL;
                    O.COSTO_TOTAL = O.COSTO_TOTAL;
                    O.VAL_IVA = item.VAL_IVA;
                    db.IN_DETCOMPRA.Add(O);
                }
                db.SaveChanges();
                result = VariablesGlobales.MsgSuccess;
                //}
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }

        public void AddDetCompra(int NumFila, string CatCod, int PorcIva, decimal Cantidad,
            decimal CostoUnit, decimal SubTotal, decimal CostoTotal, decimal ValIva)
        {
            List<AsignaProductos> Prods;
            if (Session["Productos"] == null)
            {
                Prods = new List<AsignaProductos>();
            }
            else
            {
                Prods = Session["Productos"] as List<AsignaProductos>;
            }

            Prods.Add(new AsignaProductos
            {
                NUM_FILA = NumFila,
                COD_ITEM= CatCod,
                APLICA_IVA = true,
                PORC_IVA = PorcIva,
                CANTIDAD = Cantidad,
                COSTO_UNIT = CostoUnit,
                SUBTOTAL = SubTotal,
                COSTO_TOTAL = CostoTotal,
                VAL_IVA = ValIva
            });

            Session["Productos"] = Prods;
            //var recursos = Session["Productos"] as List<AsignaProductos>;
        }

        public void RemoveDetCompra(int NumFila)
        {
            var Prods = Session["Productos"] as List<AsignaProductos>;

            //var parts = new List<AsignaProductos>();

            //foreach (var course in Prods)
            //{
            //    parts.Add(new AsignaProductos
            //    {
            //        NUM_FILA = course.NUM_FILA
            //    });

            //}


            //List<AsignaProductos> Prods;
            //if (Session["Productos"] == null)
            //{
            //    Prods = new List<AsignaProductos>();
            //}
            //else
            //{
            //    //Prods = Session["Productos"] as List<AsignaProductos>;
            //}
            //Prods.Remove(new AsignaProductos() { NUM_FILA = NumFila });
            Prods.RemoveAll(x => x.NUM_FILA == NumFila);
            //Prods.Remove(new AsignaProductos { NUM_FILA = NumFila });
            //var index = Prods.FindIndex(i => i.NUM_FILA == NumFila);

            Session["Productos"] = Prods;
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
