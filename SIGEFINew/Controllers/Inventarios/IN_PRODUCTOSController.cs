using ClosedXML.Excel;
using SIGEFINew.Clases;
using SIGEFINew.Models;
using SIGEFINew.Models.Contabilidad;
using SIGEFINew.Models.Inventarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SIGEFINew.Controllers
{
    [Authorize(Roles = "Master,Productos")]
    public class IN_PRODUCTOSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: IN_PRODUCTOS
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            ViewBag.nom_item = String.IsNullOrEmpty(sortOrder) ? "nom_item" : "";
            ViewBag.nom_item = sortOrder == "string" ? "nom_item" : "string";
            
            var iN_ITEMS = db.IN_PRODUCTOS.Include(c => c.IN_CARACGEN).Include(p => p.IN_PRESENTA)
                .Where(i => i.ID_INSTITUCION == IdInst && i.PERI_CODIGO == PeriCod );


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
                iN_ITEMS = iN_ITEMS.Where(s => s.NOM_ITEM.Contains(searchString)
                                       || s.NOM_ITEM.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "cod_item":
                    iN_ITEMS = iN_ITEMS.OrderByDescending(s => s.COD_ITEM);
                    break;
                case "nom_item":
                    iN_ITEMS = iN_ITEMS.OrderBy(s => s.NOM_ITEM);
                    break;
                default:
                    iN_ITEMS = iN_ITEMS.OrderBy(s => s.COD_ITEM);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentFilter = searchString;

            FillCatalogos( 0, null, 1);
            return View(iN_ITEMS.ToList());
        }

        private void FillCatalogos(int IDclase, IN_PRODUCTOS items, int EsNuevo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            int Clase = 0;
            int Tipo = 0;
            bool UsaCodBar;

            var iN_PARAM=db.IN_PARAM.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod).FirstOrDefault();
            UsaCodBar = iN_PARAM.ORD_X_COD; //Se usa para controlar que el código del producto es por código de barras

            var iN_CLASES = db.IN_CLASES.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod).ToList();
            iN_CLASES.Add(new IN_CLASES { ID_CLASE = 0, DESRIPCION = "[Seleccione un Tipo]" });

            if (EsNuevo == 0)
            {
                Clase = items.ID_CLASE;
                Tipo = items.ID_TIPO;
            }

            var iN_TIPOS = db.IN_TIPOS.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod
              && f.ID_CLASE == Clase).ToList();
            iN_TIPOS.Add(new IN_TIPOS { ID_TIPO = 0, NOM_TIPO = "[Seleccione una Familia]" });

            var iN_SUBTIPOS = db.IN_SUBTIPOS.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod
              && f.ID_CLASE == Clase && f.ID_TIPO == Tipo).ToList();
            iN_SUBTIPOS.Add(new IN_SUBTIPOS { ID_SUBTIPO = 0, NOM_SUBTIPO = "[Seleccione una SubFamilia]" });

            var iN_CARACGEN = db.IN_CARACGEN.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod
                && f.CARACT == "UM");

            var iN_PRESENTAS = db.IN_PRESENTA.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod);

            var Proveed = db.Proveedores.ToList();
            Proveed.Add(new Proveedores { COD_PROV = "", NOMBRE = "[Seleccione un Consignante]" });

            var CtasOrdDb = db.CO_CUENTASCONT.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod
               && f.CODIGO_CG.Contains(@"9.1.1.")).ToList();
            CtasOrdDb.Add(new CO_CUENTASCONT { CODIGO_CG = "", NOMBRE_CG = "[Seleccione una Cuenta]" });

            var CtasOrdHb = db.CO_CUENTASCONT.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod
               && f.CODIGO_CG.Contains(@"9.2.1.")).ToList();
            CtasOrdHb.Add(new CO_CUENTASCONT { CODIGO_CG = "", NOMBRE_CG = "[Seleccione una Cuenta]" });

            var mPolit = (from Item in db.Catalogos
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod
                          && Item.TRE_CODIGO == 1
                          && Item.TIPO == "D"
                          select Item).ToList();

            List<Models.Presupuesto.Catalogos> Items = new List<Models.Presupuesto.Catalogos>();

            foreach (var Polit in mPolit)
            {
                Items.Add(new Models.Presupuesto.Catalogos()
                {
                    CAT_CODIGO = Polit.CAT_CODIGO,
                    CAT_NOMBRE = Polit.CAT_NOMBRE
                });
            }

            Items.Add(new Models.Presupuesto.Catalogos { CAT_CODIGO = "", CAT_NOMBRE = "[Seleccione un Catálogo]" });

            var TipoPresup = new List<TipoPres>
            {
                //new TipoPres{TIPO_PRESUP=0,DescTipoPres="Elija un Tipo de Costeo"},
                new TipoPres{TIPO_PRESUP=1,DescTipoPres="Promedio Ponderado"},
                new TipoPres{TIPO_PRESUP=2,DescTipoPres="Por Lotes"},
                new TipoPres{TIPO_PRESUP=3,DescTipoPres="FIFO"},
                new TipoPres{TIPO_PRESUP=4,DescTipoPres="LIFO"}
            };

            var CostoProd = new List<TipoPres>
            {
                //new TipoPres{TIPO_PRESUP=0,DescTipoPres="Elija un Costo"},
                new TipoPres{TIPO_PRESUP=1,DescTipoPres="A"},
                new TipoPres{TIPO_PRESUP=2,DescTipoPres="B"},
                new TipoPres{TIPO_PRESUP=3,DescTipoPres="C"}
            };

            if (EsNuevo == 1)
            {
                ViewBag.ID_CLASE = new SelectList(iN_CLASES.OrderBy(o => o.DESRIPCION), "ID_CLASE", "DESRIPCION", IDclase);
                ViewBag.CAT_CODIGO = new SelectList(Items.OrderBy(o => o.CAT_NOMBRE), "CAT_CODIGO", "CAT_NOMBRE");
                ViewBag.ID_TIPO = new SelectList(iN_TIPOS.OrderBy(o => o.ID_TIPO), "ID_TIPO", "NOM_TIPO");
                ViewBag.ID_SUBTIPO = new SelectList(iN_SUBTIPOS.OrderBy(o => o.ID_SUBTIPO), "ID_SUBTIPO", "NOM_SUBTIPO");
                ViewBag.COD_CAGEN = new SelectList(db.IN_CARACGEN, "COD_CAGEN", "DESCRIP_CAGEN");
                ViewBag.COD_PRESEN = new SelectList(db.IN_PRESENTA, "COD_PRESEN", "DESCRIP_CAGEN");
                ViewBag.TIPO_COSTEO = new SelectList(TipoPresup, "TIPO_PRESUP", "DescTipoPres", 1);
                ViewBag.COSTO_PROD = new SelectList(CostoProd, "DescTipoPres", "DescTipoPres", 1);
                ViewBag.COD_CONSIG = new SelectList(Proveed.OrderBy(o => o.NOMBRE), "COD_PROV", "NOMBRE", "");
                ViewBag.CODIGO_CGDB = new SelectList(CtasOrdDb, "CODIGO_CG", "NOMBRE_CG", "");
                ViewBag.CODIGO_CGCR = new SelectList(CtasOrdDb, "CODIGO_CG", "NOMBRE_CG", "");
            }
            else
            {
                ViewBag.ID_CLASE = new SelectList(iN_CLASES.OrderBy(o => o.DESRIPCION), "ID_CLASE", "DESRIPCION", IDclase);
                ViewBag.CAT_CODIGO = new SelectList(Items.OrderBy(o => o.CAT_NOMBRE), "CAT_CODIGO", "CAT_NOMBRE", items.CAT_CODIGO);
                ViewBag.ID_TIPO = new SelectList(iN_TIPOS, "ID_TIPO", "NOM_TIPO", items.ID_TIPO);
                ViewBag.ID_SUBTIPO = new SelectList(iN_SUBTIPOS.OrderBy(o => o.ID_SUBTIPO), "ID_SUBTIPO", "NOM_SUBTIPO", items.ID_SUBTIPO);
                ViewBag.COD_CAGEN = new SelectList(db.IN_CARACGEN, "COD_CAGEN", "DESCRIP_CAGEN");
                ViewBag.COD_PRESEN = new SelectList(db.IN_PRESENTA, "COD_PRESEN", "DESCRIP_CAGEN");
                ViewBag.TIPO_COSTEO = new SelectList(TipoPresup, "TIPO_PRESUP", "DescTipoPres", 1);
                ViewBag.COSTO_PROD = new SelectList(CostoProd, "DescTipoPres", "DescTipoPres", 1);
                ViewBag.COD_CONSIG = new SelectList(Proveed.OrderBy(o => o.NOMBRE), "COD_PROV", "NOMBRE", "");
                ViewBag.CODIGO_CGDB = new SelectList(CtasOrdDb, "CODIGO_CG", "NOMBRE_CG", "");
                ViewBag.CODIGO_CGCR = new SelectList(CtasOrdDb, "CODIGO_CG", "NOMBRE_CG", "");
            }

            ViewBag.UsaCodBar = UsaCodBar;
        }
        // GET: IN_PRODUCTOS/Details/5
        public ActionResult Details(int? ID_BODEGA)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var iN_BODEGAS = db.IN_BODEGAS.Where(i => i.ID_INSTITUCION == IdInst && i.PERI_CODIGO == PeriCod).ToList();
            iN_BODEGAS.Add(new IN_BODEGAS { ID_BODEGA = 0, NOM_BODEGA = "[Seleccione una Bodega]" });

            ViewBag.ID_BODEGA = new SelectList(iN_BODEGAS.OrderBy(o=>o.ID_BODEGA), "ID_BODEGA", "NOM_BODEGA");

            List<IN_PRODUCTOS> Items = new List<IN_PRODUCTOS>();

            var t = (from c in db.IN_PRODUCTOS
                 join a in db.IN_CLASES
                     on c.ID_CLASE equals a.ID_CLASE
                 where !db.IN_ITEMS.Any(p => p.ID_INSTITUCION == IdInst && p.PERI_CODIGO == PeriCod &&
                    p.COD_ITEM == c.COD_ITEM && p.ID_BODEGA == ID_BODEGA) && a.TIPO_USO.ToString().Trim() != "BIENES"
                 select c).ToList();

            if (ID_BODEGA > 0)
            {
                foreach (var Polit in t)
                {
                    //IN_CARGA_INI CargaIni = new IN_CARGA_INI();
                    Items.Add(new IN_PRODUCTOS()
                    {
                        ID_INSTITUCION = Polit.ID_INSTITUCION,
                        PERI_CODIGO = Polit.PERI_CODIGO,
                        COD_ITEM = Polit.COD_ITEM.Trim(),
                        NOM_ITEM = Polit.NOM_ITEM,
                    });
                }
            }
            

            return View(Items);
        }

        // GET: IN_PRODUCTOS/Create
        public ActionResult Create(String id2)
        {
            int IdClase = 0;
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            int Nuevo;

            IN_PRODUCTOS iN_ITEMS = db.IN_PRODUCTOS.Find(IdInst, PeriCod, id2);
            
            if (id2 != null)
            {
                ViewBag.EsNuevo = 0;
                ViewBag.IDTIPO = iN_ITEMS.ID_TIPO;
                ViewBag.IDSUBTIPO = iN_ITEMS.ID_SUBTIPO;
                ViewBag.UserCrea = iN_ITEMS.USER_CREA;
                ViewBag.FechaCrea = iN_ITEMS.FECHA_CREA.ToString("dd/MM/yyyy");
                Nuevo = 0;
            }
            else
            {
                ViewBag.EsNuevo = 1;
                ViewBag.IDTIPO = 0;
                ViewBag.IDSUBTIPO = 0;
                ViewBag.UserCrea = Session["Usuario"].ToString();
                ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");
                Nuevo = 1;
            }

            try
            {
                IdClase = iN_ITEMS.ID_CLASE;
            }
            catch
            {

            }
            FillCatalogos(IdClase, iN_ITEMS, Nuevo);

            return View(iN_ITEMS);
        }

        // POST: IN_PRODUCTOS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,COD_ITEM,ID_CLASE,ID_TIPO,ID_SUBTIPO,COD_CAGEN,COD_PRESEN,CAT_CODIGO,NOM_ITEM,MARCA_ITEM,APLIC_ITEM,TIPO_COSTEO,COSTO_PROD,APLICA_IVA,PROD_PERE,PROD_DESC,PROD_NUM_SERIE,PROD_NUM_GAR,SECCION,PERCHA,FILA,COLUMNA,STOCK_MIN,STOCK_MAX,PUNTO_REORDEN,FECHA_ULT_COMPRA,COD_CONSIG,CODIGO_CGDB,CODIGO_CGCR,COD_LINEA,COD_SUBLINEA,USER_CREA,FECHA_CREA")] IN_PRODUCTOS iN_ITEMS, int EsNuevo,bool UsaCodBar)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            iN_ITEMS.ID_INSTITUCION = IdInst;
            iN_ITEMS.PERI_CODIGO = PeriCod;

            int CodPres = 0;

            try
            {
                //var MaxCod = db.IN_PRESENTA.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod &&
                //f.COD_CAGEN == iN_PRESENTA.COD_CAGEN)
                //.First(x => x.COD_PRESEN == db.IN_PRESENTA.Max(y => y.COD_PRESEN));
                if (UsaCodBar == false)
                {
                    CodPres = Convert.ToInt32((from nd in db.IN_PRODUCTOS
                                               where nd.ID_INSTITUCION == IdInst && nd.PERI_CODIGO == PeriCod
                                               select nd.COD_ITEM).Max());
                }
                
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
                        iN_ITEMS.PRECIO1 = 0;
                        iN_ITEMS.PRECIO2 = 0;
                        iN_ITEMS.COD_LINEA = 0;
                        iN_ITEMS.COD_SUBLINEA = 0;

                        if (UsaCodBar == false)
                        {
                            iN_ITEMS.COD_ITEM = (CodPres + 1).ToString();
                        }
                        db.IN_PRODUCTOS.Add(iN_ITEMS);
                    }
                    else
                    {
                        iN_ITEMS.USER_MODIF = Session["Usuario"].ToString();
                        iN_ITEMS.FECHA_MODIF = System.DateTime.Today;
                        db.Entry(iN_ITEMS).State = EntityState.Modified;
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
                FillCatalogos(0, null, EsNuevo);
            }
            else
            {
                int IdClase = iN_ITEMS.ID_CLASE;
                FillCatalogos(IdClase, iN_ITEMS, EsNuevo);
                ViewBag.EsNuevo = 0;
            }

            return View(iN_ITEMS);
        }

        // GET: IN_PRODUCTOS/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IN_PRODUCTOS/Edit/5
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

        // GET: IN_ITEMS/Delete/5
        public ActionResult Delete(string id2)
        {
            if (id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_PRODUCTOS iN_ITEMS = db.IN_PRODUCTOS.Find(IdInst, PeriCod, id2);
            if (iN_ITEMS == null)
            {
                return HttpNotFound();
            }
            return View(iN_ITEMS);
        }

        // POST: IN_ITEMS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id2)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_PRODUCTOS iN_ITEMS = db.IN_PRODUCTOS.Find(IdInst, PeriCod, id2);

            try
            {
                db.IN_PRODUCTOS.Remove(iN_ITEMS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                ViewBag.MsgError = TempData["MsgError"];
            }

            return View(iN_ITEMS);
        }

        public ActionResult GenerarExcel()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var Productos = (from p in db.IN_PRODUCTOS
                             where p.ID_INSTITUCION == IdInst && p.PERI_CODIGO == PeriCod
                             orderby p.COD_ITEM
                             select new
                             {
                                 p.COD_ITEM,
                                 p.NOM_ITEM
                             }).ToList();

            var rutaExcelx = Server.MapPath("~/App_Data/");
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Registros");
                worksheet.Cell("A1").Value = "Código";
                worksheet.Cell("B1").Value = "Producto";

                int Fila = 2;
                string Row = "";
                foreach (var item in Productos)
                {
                    Row = "A" + Fila.ToString().Trim();
                    worksheet.Cell(Row).Value = item.COD_ITEM;

                    Row = "B" + Fila.ToString().Trim();
                    worksheet.Cell(Row).Value = item.NOM_ITEM;

                    Fila++;
                }


                workbook.SaveAs(rutaExcelx + "Productos.xlsx");

                string filename = "Productos.xlsx";
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
                string aaa = Server.MapPath("~/App_Data/" + filename);
                Response.TransmitFile(Server.MapPath("~/App_Data/" + filename));
                Response.End();

                //return File(rutaExcelx, "Registros.xlsx");
            }



            return File(rutaExcelx, "text/csv/xls/xlsx", "Productos.xls");
        }
    }
}
