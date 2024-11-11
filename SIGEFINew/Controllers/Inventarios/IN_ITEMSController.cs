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
using ClosedXML.Excel;
namespace SIGEFINew.Controllers.Inventarios
{
    [Authorize(Roles = "Master,Productos")]
    public class IN_ITEMSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IN_ITEMS
        public ActionResult Index(int? IdBodega, string sortOrder, string currentFilter, string searchString, int? page)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            ViewBag.nom_item = String.IsNullOrEmpty(sortOrder) ? "nom_item" : "";
            ViewBag.nom_item = sortOrder == "string" ? "nom_item" : "string";
            var iN_ITEMS = db.IN_ITEMS.Include(c=>c.IN_CARACGEN).Include(p=>p.IN_PRESENTA).Where(i => i.ID_INSTITUCION == IdInst && i.PERI_CODIGO == PeriCod && i.ID_BODEGA==IdBodega);

            if (IdBodega != null)
            {
                iN_ITEMS = db.IN_ITEMS.Where(f => f.ID_INSTITUCION == IdInst
                 && f.PERI_CODIGO == PeriCod && f.ID_BODEGA == IdBodega);
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
            ViewBag.IdB = IdBodega;
            FillCatalogos(0, 0,null,1);
            return View(iN_ITEMS.ToList());
        }

        private void FillCatalogos(int IdBod, int IDclase, IN_ITEMS items,int EsNuevo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            int Clase=0;
            int Tipo = 0;

            var iN_BODEGAS = db.IN_BODEGAS.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod).ToList();
            iN_BODEGAS.Add(new IN_BODEGAS { ID_BODEGA = 0, NOM_BODEGA = "[Seleccione una Bodega]" });

            var iN_CLASES = db.IN_CLASES.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod).ToList();
            iN_CLASES.Add(new IN_CLASES { ID_CLASE = 0, DESRIPCION = "[Seleccione un Tipo]" });

            if (EsNuevo == 0) 
            {
                Clase = items.ID_CLASE;
                Tipo = items.ID_TIPO;
            }

            var iN_TIPOS = db.IN_TIPOS.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod
              && f.ID_CLASE==Clase).ToList();
            iN_TIPOS.Add(new IN_TIPOS { ID_TIPO = 0, NOM_TIPO = "[Seleccione una Familia]" });

            var iN_SUBTIPOS = db.IN_SUBTIPOS.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod
              && f.ID_CLASE==Clase && f.ID_TIPO==Tipo).ToList();
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

            Items.Add(new Models.Presupuesto.Catalogos { CAT_CODIGO ="", CAT_NOMBRE = "[Seleccione un Catálogo]" });

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
                ViewBag.ID_BODEGA = new SelectList(iN_BODEGAS.OrderBy(o => o.NOM_BODEGA), "ID_BODEGA", "NOM_BODEGA", IdBod);
                ViewBag.ID_CLASE = new SelectList(iN_CLASES.OrderBy(o => o.DESRIPCION), "ID_CLASE", "DESRIPCION", IDclase);
                ViewBag.CAT_CODIGO = new SelectList(Items.OrderBy(o => o.CAT_NOMBRE), "CAT_CODIGO", "CAT_NOMBRE");
                ViewBag.ID_TIPO = new SelectList(iN_TIPOS.OrderBy(o=>o.ID_TIPO), "ID_TIPO", "NOM_TIPO");
                ViewBag.ID_SUBTIPO = new SelectList(iN_SUBTIPOS.OrderBy(o=>o.ID_SUBTIPO), "ID_SUBTIPO", "NOM_SUBTIPO");
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

                ViewBag.ID_BODEGA = new SelectList(iN_BODEGAS.OrderBy(o => o.NOM_BODEGA), "ID_BODEGA", "NOM_BODEGA", IdBod);
                ViewBag.ID_CLASE = new SelectList(iN_CLASES.OrderBy(o => o.DESRIPCION), "ID_CLASE", "DESRIPCION", IDclase);
                ViewBag.CAT_CODIGO = new SelectList(Items.OrderBy(o => o.CAT_NOMBRE), "CAT_CODIGO", "CAT_NOMBRE",items.CAT_CODIGO);
                ViewBag.ID_TIPO = new SelectList(iN_TIPOS, "ID_TIPO", "NOM_TIPO",items.ID_TIPO);
                ViewBag.ID_SUBTIPO = new SelectList(iN_SUBTIPOS.OrderBy(o => o.ID_SUBTIPO), "ID_SUBTIPO", "NOM_SUBTIPO",items.ID_SUBTIPO);
                ViewBag.COD_CAGEN = new SelectList(db.IN_CARACGEN, "COD_CAGEN", "DESCRIP_CAGEN");
                ViewBag.COD_PRESEN = new SelectList(db.IN_PRESENTA, "COD_PRESEN", "DESCRIP_CAGEN");
                ViewBag.TIPO_COSTEO = new SelectList(TipoPresup, "TIPO_PRESUP", "DescTipoPres", 1);
                ViewBag.COSTO_PROD = new SelectList(CostoProd, "DescTipoPres", "DescTipoPres", 1);
                ViewBag.COD_CONSIG = new SelectList(Proveed.OrderBy(o => o.NOMBRE), "COD_PROV", "NOMBRE", "");
                ViewBag.CODIGO_CGDB = new SelectList(CtasOrdDb, "CODIGO_CG", "NOMBRE_CG", "");
                ViewBag.CODIGO_CGCR = new SelectList(CtasOrdDb, "CODIGO_CG", "NOMBRE_CG", "");
            }
            
            
        }
        // GET: IN_ITEMS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IN_ITEMS iN_ITEMS = db.IN_ITEMS.Find(id);
            if (iN_ITEMS == null)
            {
                return HttpNotFound();
            }
            return View(iN_ITEMS);
        }

        // GET: IN_ITEMS/Create
        public ActionResult Create(int? id, String id2)
        {
            int IdBod = 0;
            int IdClase = 0;
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            int Nuevo;
            IN_ITEMS iN_ITEMS = db.IN_ITEMS.Find(IdInst, PeriCod, id, id2);
            if (id != null)
            {
                ViewBag.EsNuevo = 0;
                ViewBag.IDTIPO = iN_ITEMS.ID_TIPO;
                ViewBag.IDSUBTIPO = iN_ITEMS.ID_SUBTIPO;
                ViewBag.UserCrea = iN_ITEMS.USER_CREA;
                ViewBag.FechaCrea = iN_ITEMS.FECHA_CREA;
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
                IdBod = iN_ITEMS.ID_BODEGA;
                IdClase = iN_ITEMS.ID_CLASE;
            }
            catch
            {

            }
            FillCatalogos(IdBod, IdClase, iN_ITEMS,Nuevo);

            return View(iN_ITEMS);
        }

        // POST: IN_ITEMS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,ID_BODEGA,COD_ITEM,ID_CLASE,ID_TIPO,ID_SUBTIPO,COD_CAGEN,COD_PRESEN,CAT_CODIGO,NOM_ITEM,MARCA_ITEM,APLIC_ITEM,TIPO_COSTEO,COSTO_PROD,APLICA_IVA,PROD_PERE,PROD_DESC,PROD_NUM_SERIE,PROD_NUM_GAR,SECCION,PERCHA,FILA,COLUMNA,STOCK_MIN,STOCK_MAX,PUNTO_REORDEN,FECHA_ULT_COMPRA,COD_CONSIG,CODIGO_CGDB,CODIGO_CGCR,COD_LINEA,COD_SUBLINEA,USER_CREA,FECHA_CREA")] IN_ITEMS iN_ITEMS, int EsNuevo)
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

                CodPres = Convert.ToInt32((from nd in db.IN_ITEMS
                                           where nd.ID_INSTITUCION == IdInst && nd.PERI_CODIGO == PeriCod
                                           && nd.ID_BODEGA == iN_ITEMS.ID_BODEGA
                                           select nd.COD_ITEM).Max());
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
                        //iN_ITEMS.COD_LINEA = 0;
                        //iN_ITEMS.COD_SUBLINEA = 0;
                        iN_ITEMS.COD_ITEM = (CodPres + 1).ToString();
                        db.IN_ITEMS.Add(iN_ITEMS);
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
                FillCatalogos(0, 0,null,EsNuevo);
            }
            else
            {
                int IdBod = iN_ITEMS.ID_BODEGA;
                int IdClase = iN_ITEMS.ID_CLASE;
                FillCatalogos(IdBod, IdClase, iN_ITEMS,EsNuevo);
                ViewBag.EsNuevo = 0;
            }

            return View(iN_ITEMS);
        }

        // GET: IN_ITEMS/Edit/5
        public ActionResult Edit(int? id, string id2)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_ITEMS iN_ITEMS = db.IN_ITEMS.Find(IdInst, PeriCod, id, id2);

            int IdBod = iN_ITEMS.ID_BODEGA;
            int IdClase = iN_ITEMS.ID_CLASE;

            FillCatalogos(IdBod, IdClase,null,2);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (iN_ITEMS == null)
            {
                return HttpNotFound();
            }
            ViewBag.EsNuevo = 0;
            return View(iN_ITEMS);
        }

        // POST: IN_ITEMS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,ID_BODEGA,COD_ITEM,ID_CLASE,ID_TIPO,ID_SUBTIPO,COD_CAGEN,COD_PRESEN,CAT_CODIGO,NOM_ITEM,MARCA_ITEM,APLIC_ITEM,TIPO_COSTEO,COSTO_PROD,APLICA_IVA,PROD_PERE,PROD_DESC,PROD_NUM_SERIE,PROD_NUM_GAR,SECCION,PERCHA,FILA,COLUMNA,STOCK_MIN,STOCK_MAX,PUNTO_REORDEN,PRECIO1,PRECIO2,FECHA_ULT_COMPRA,COD_CONSIG,CODIGO_CGDB,CODIGO_CGCR,COD_LINEA,COD_SUBLINEA,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF")] IN_ITEMS iN_ITEMS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iN_ITEMS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_INSTITUCION = new SelectList(db.IN_BODEGAS, "ID_INSTITUCION", "NOM_BODEGA", iN_ITEMS.ID_INSTITUCION);
            ViewBag.ID_INSTITUCION = new SelectList(db.IN_CARACGEN, "ID_INSTITUCION", "DESCRIP_CAGEN", iN_ITEMS.ID_INSTITUCION);
            ViewBag.ID_INSTITUCION = new SelectList(db.IN_CLASES, "ID_INSTITUCION", "DESRIPCION", iN_ITEMS.ID_INSTITUCION);
            ViewBag.ID_INSTITUCION = new SelectList(db.IN_PRESENTA, "ID_INSTITUCION", "DESCRIP_CAGEN", iN_ITEMS.ID_INSTITUCION);
            ViewBag.ID_INSTITUCION = new SelectList(db.IN_SUBTIPOS, "ID_INSTITUCION", "NOM_SUBTIPO", iN_ITEMS.ID_INSTITUCION);
            ViewBag.ID_INSTITUCION = new SelectList(db.IN_TIPOS, "ID_INSTITUCION", "NOM_TIPO", iN_ITEMS.ID_INSTITUCION);
            return View(iN_ITEMS);
        }

        // GET: IN_ITEMS/Delete/5
        public ActionResult Delete(int? id, string id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_ITEMS iN_ITEMS = db.IN_ITEMS.Find(IdInst, PeriCod, id, id2);
            if (iN_ITEMS == null)
            {
                return HttpNotFound();
            }
            return View(iN_ITEMS);
        }

        // POST: IN_ITEMS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string id2)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_ITEMS iN_ITEMS = db.IN_ITEMS.Find(IdInst, PeriCod, id, id2);

            try
            {
                db.IN_ITEMS.Remove(iN_ITEMS);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GenerarExcel(int? IdBodega)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            
            var Productos = (from p in db.IN_ITEMS
                             join a in db.IN_BODEGAS on p.ID_BODEGA equals a.ID_BODEGA
                             where p.ID_INSTITUCION == IdInst && p.PERI_CODIGO==PeriCod
                                 && p.ID_BODEGA== IdBodega
                             orderby p.COD_ITEM
                                 select new
                                 {
                                     p.ID_BODEGA,
                                     a.NOM_BODEGA,
                                     p.COD_ITEM,
                                     p.NOM_ITEM
                                     
                                 }).ToList();

            var rutaExcelx = Server.MapPath("~/App_Data/");
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Registros");
                worksheet.Cell("A1").Value = "Cod. Bodega";
                worksheet.Cell("B1").Value = "Nom. Bodega";
                worksheet.Cell("C1").Value = "Código";
                worksheet.Cell("D1").Value = "Producto";

                int Fila = 2;
                string Row = "";
                foreach (var item in Productos)
                {
                    Row = "A" + Fila.ToString().Trim();
                    worksheet.Cell(Row).Value = item.ID_BODEGA.ToString();

                    Row = "B" + Fila.ToString().Trim();
                    worksheet.Cell(Row).Value = item.NOM_BODEGA;

                    Row = "C" + Fila.ToString().Trim();
                    worksheet.Cell(Row).Value = item.COD_ITEM;

                    Row = "D" + Fila.ToString().Trim();
                    worksheet.Cell(Row).Value = item.NOM_ITEM;

                    Fila++;
                }


                workbook.SaveAs(rutaExcelx + "Registros.xlsx");

                string filename = "Productos.xlsx";
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
                string aaa = Server.MapPath("~/App_Data/" + filename);
                Response.TransmitFile(Server.MapPath("~/App_Data/" + filename));
                Response.End();

                //return File(rutaExcelx, "Registros.xlsx");
            }



            //StringBuilder builder = new StringBuilder();

            ////Agregamos las cabezeras 
            //builder.Append("N° Identificación").Append(";")
            //.Append("Apellidos/Nombres").Append(";")
            //.Append("Profesión u Oficio").Append(";")
            //.Append("Edad").Append(";")
            //.Append("Teléfono").Append(";")
            //.Append("Correo Electrónico").Append(";")
            //.Append("Barrio,Urb.,Coop.").Append(";")
            //.Append("Dirección Domiciliaria").Append(";")

            ////Sintomas mas frecuentes
            //.Append("Fiebre").Append(";")
            //.Append("Dolor de Garganta").Append(";")
            //.Append("Tos Seca").Append(";")
            //.Append("Flema").Append(";")
            //.Append("Dificultad para Respirar").Append(";")
            //.Append("Dolor de cabeza").Append(";")
            //.Append("Pérdida del Olfato y Gusto").Append(";")
            //.Append("Pérdida del Apetito").Append(";")
            //.Append("Cansancio al Caminar").Append(";")
            //.Append("Dolores Musculares").Append(";")
            //.Append("Ninguno").Append(";")

            ////Sintomas poco frecuentes
            //.Append("Secreción Nasal").Append(";")
            //.Append("Flema con sangre").Append(";")
            //.Append("Dolor del Pecho").Append(";")
            //.Append("Piel y labios azules").Append(";")
            //.Append("Ronchas en la Piel").Append(";")
            //.Append("Enrojecimiento de los ojos").Append(";")
            //.Append("Sudoración Excesiva").Append(";")
            //.Append("Escalofrios").Append(";")
            //.Append("Diarrea").Append(";")
            //.Append("Confusión").Append(";")
            //.Append("Ninguno").Append(";")

            ////Factores de Alto Riesgo
            //.Append("Asma").Append(";")
            //.Append("Cancer").Append(";")
            //.Append("Diabetes").Append(";")
            //.Append("Epilepsia").Append(";")
            //.Append("Hipertensión").Append(";")
            //.Append("Patologías renales").Append(";")
            //.Append("Patologías cardíacas").Append(";")
            //.Append("Sobrepeso").Append(";")
            //.Append("Edad Avanzada").Append(";")
            //.Append("Hay alguien en casa con COVID-19").Append(";")
            //.Append("Ha estado en sitios de riesgo de COVID-19").Append(";")
            //.Append("Ninguno").Append(";")

            //.Append("Esta tomando algún medicamento").Append(";")
            //.Append("Es alérgico a algún medicamento").Append(";")
            //.Append("Fuma o ha fumado?").Append(";")
            //.Append("Consume Droga").Append(";")
            //.Append("IGM").Append(";")
            //.Append("IGG").Append(";")
            //.Append("IGG2").Append(";")

            //.Append("Latitud").Append(";")
            //.Append("Longitud").Append(";")

            //.Append("Observaciones").Append(";")
            //;
            //builder.Append("\n");

            //foreach (var item in colaboradores)
            //{

            //    builder.Append(item.CEDULA).Append(";")
            //    .Append(item.APELLIDOSNOMBRES).Append(";")
            //    .Append(item.PROFESION).Append(";")
            //    .Append(item.EDAD).Append(";")
            //    .Append(item.TELEFONO).Append(";")
            //    .Append(item.EMAIL).Append(";")
            //    .Append(item.BARRIO).Append(";")
            //    .Append(item.DIRECDOM).Append(";")

            //    //Sintomas mas frecuentes
            //    .Append(item.FIEBRE).Append(";")
            //    .Append(item.DOLOR_GTA).Append(";")
            //    .Append(item.TOS_SECA).Append(";")
            //    .Append(item.FLEMA).Append(";")
            //    .Append(item.DIFIC_RESPIRA).Append(";")
            //    .Append(item.DOLOR_CABEZA).Append(";")
            //    .Append(item.PERD_OLFGTO).Append(";")
            //    .Append(item.PERD_APETITO).Append(";")
            //    .Append(item.CANSA_CAMINA).Append(";")
            //    .Append(item.DOLOR_MUSCU).Append(";")
            //    .Append(item.SMF_NINGUNO).Append(";")

            //    //Sintomas poco frecuentes
            //    .Append(item.SECRE_NASAL).Append(";")
            //    .Append(item.FLEMA_SANGRE).Append(";")
            //    .Append(item.DOLOR_PECHO).Append(";")
            //    .Append(item.PIELLAB_AZUL).Append(";")
            //    .Append(item.RONCHA_PIEL).Append(";")
            //    .Append(item.ENROJECE_OJO).Append(";")
            //    .Append(item.SUDOR_EXCE).Append(";")
            //    .Append(item.ESCALOFRIOS).Append(";")
            //    .Append(item.DIARREA).Append(";")
            //    .Append(item.CONFUSION).Append(";")
            //    .Append(item.SPF_NINGUNO).Append(";")

            //    //Factores de Alto Riesgo
            //    .Append(item.ASMA).Append(";")
            //    .Append(item.CANCER).Append(";")
            //    .Append(item.DIABETES).Append(";")
            //    .Append(item.EPILEPSIA).Append(";")
            //    .Append(item.HIPERTENSION).Append(";")
            //    .Append(item.PATOLOG_RENAL).Append(";")
            //    .Append(item.PATOLOG_CARDIA).Append(";")
            //    .Append(item.SOBREPESO).Append(";")
            //    .Append(item.EDAD_AVANZADA).Append(";")
            //    .Append(item.CON_COVID).Append(";")
            //    .Append(item.SITIO_COVID).Append(";")
            //    .Append(item.FAR_NINGUNO).Append(";")

            //    .Append(item.TOMA_MEDICINA).Append(";")
            //    .Append(item.ALERGIA_MEDICINA).Append(";")
            //    .Append(item.FUMA_OAFUMADO).Append(";")
            //    .Append(item.USA_DROGA).Append(";")

            //    .Append(item.IGM).Append(";")
            //    .Append(item.IGG1).Append(";")
            //    .Append(item.IGG2).Append(";")

            //    .Append(item.LATITUD).Append(";")
            //    .Append(item.LONGITUD).Append(";")
            //    .Append(item.OBSERVACION).Append(";")
            //    ;


            //    builder.Append("\n");// agregamos una nueva fila 
            //}


            //// Lo encodeamos con UTF8 para mostrar los acentos correctamente.
            //var excelBytes = Encoding.UTF8.GetBytes(builder.ToString());
            //var excelConUT8Encoding = Encoding.UTF8.GetPreamble().Concat(excelBytes).ToArray();

            //// guardamos el contenido del archivo en la ruta especificada
            //var rutaExcel = Server.MapPath("~/App_Data/excel.xls");
            //System.IO.File.WriteAllBytes(rutaExcel, excelConUT8Encoding);

            return File(rutaExcelx, "text/csv/xls/xlsx", "Registros.xls");
        }
    }
}
