using Microsoft.Reporting.WebForms;
using SIGEFINew.Clases;
using SIGEFINew.Models;
using SIGEFINew.Models.Inventarios;
using SIGEFINew.Reportes;
using SIGEFINew.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGEFINew.Controllers.Inventarios
{
    [Authorize(Roles = "Master,IN_Egresos")]
    public class IN_EGRESOSController : Controller
    {
        // GET: IN_EGRESOS
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);
            string Usuario = Session["Usuario"].ToString();

            var iN_TRANSAC = db.IN_TRANSACCIONES.Where(f => f.ID_INSTITUCION == IdInst
            && f.PERI_CODIGO == Pericod && (f.TIPO_INGRESO == "E" || f.TIPO_INGRESO == "EV")).Include(i => i.Periodos).Include(i => i.Proveedores);

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
                iN_TRANSAC = iN_TRANSAC.Where(s => s.Proveedores.NOMBRE.Contains(searchString)
                                       || s.NUM_DOC.Contains(searchString)
                                       || s.DETALLE.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nombre":
                    iN_TRANSAC = iN_TRANSAC.OrderByDescending(s => s.Proveedores.NOMBRE);
                    break;
                case "fecha_doc":
                    iN_TRANSAC = iN_TRANSAC.OrderBy(s => s.FECHA_DOC);
                    break;
                default:
                    iN_TRANSAC = iN_TRANSAC.OrderBy(s => s.NUM_DOC);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentFilter = searchString;

            return View(iN_TRANSAC.ToList());
        }

        // GET: IN_EGRESOS/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IN_EGRESOS/Create
        public ActionResult Create(int? IdBodega, int? NumTransac)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            IN_TRANSACCIONES iN_COMPRAS = db.IN_TRANSACCIONES.Find(IdInst, PeriCod, IdBodega, NumTransac, "E", "EV");

            LLenaCombos();

            if (NumTransac != null)
            {
                ViewBag.EsNuevo = 0;
            }
            else
            {
                ViewBag.EsNuevo = 1;
                ViewBag.UserCrea = Session["Usuario"].ToString();
                ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");
            }

            ViewBag.TipoTran = "E";
            ViewBag.TipoIngre = "EV";
            ViewBag.TipoPago = "EF";

            var PInv = db.IN_PARAM.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod
              ).FirstOrDefault();
            ViewBag.NumDecCUnit = PInv.UTIL_DEC_CUNI;

            return View(iN_COMPRAS);
        }

        // POST: IN_EGRESOS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_INSTITUCION,PERI_CODIGO,ID_BODEGA,NUM_TRANSACCION,TIPO_TRAN,TIPO_INGRESO,NUM_COMPRA,FECHA_TRANSAC,TIPO_MOV,TIPO_PAGO,COD_TIPO_DOC,COD_PROV,ID_UNIDOPERATIVA,AUTORIZADO_POR,USO,DETALLE,ID_OBJETO,TIPO_SRI,FECHA_DOC,NUM_DOC,FECHACAD_DOC,AUTORIZACION,NUM_CXP,ANULADO,CERRADO,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF")] IN_TRANSACCIONES iN_COMPRAS, int EsNuevo)
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
                if (EsNuevo == 1)
                {
                    NumDisp = (from nd in db.IN_TRANSACCIONES
                               where nd.ID_INSTITUCION == IdInst && nd.PERI_CODIGO == PeriCod && nd.TIPO_TRAN=="E" && nd.TIPO_INGRESO=="EV"
                               select nd.NUM_TRANSACCION).Max();
                }

            }
            catch
            {

            }

            if (EsNuevo == 1)
            {
                iN_COMPRAS.NUM_TRANSACCION = NumDisp + 1;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (EsNuevo == 1)
                    {
                        db.IN_TRANSACCIONES.Add(iN_COMPRAS);
                    }
                    else
                    {
                        var DetC = db.IN_DETALLETRANSAC.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod && f.ID_BODEGA == iN_COMPRAS.ID_BODEGA
                      && f.NUM_TRANSACCION == iN_COMPRAS.NUM_TRANSACCION);

                        db.IN_DETALLETRANSAC.RemoveRange(DetC);

                        db.Entry(iN_COMPRAS).State = EntityState.Modified;
                    }

                    foreach (var course in Products)
                    {
                        var Productos = new IN_DETALLETRANSAC();

                        Productos.ID_INSTITUCION = IdInst;
                        Productos.PERI_CODIGO = PeriCod;
                        Productos.ID_BODEGA = iN_COMPRAS.ID_BODEGA;
                        if (EsNuevo == 1)
                        {
                            Productos.NUM_TRANSACCION = NumDisp + 1;
                        }
                        else
                        {
                            Productos.NUM_TRANSACCION = iN_COMPRAS.NUM_COMPRA;
                        }
                        Productos.TIPO_TRAN = iN_COMPRAS.TIPO_TRAN;
                        Productos.TIPO_INGRESO = iN_COMPRAS.TIPO_INGRESO;
                        Productos.NUM_FILA = course.NUM_FILA;
                        Productos.COD_ITEM = course.COD_ITEM;
                        //if (course.PORC_IVA == 12)
                        //{
                        //    Productos.APLICA_IVA = true;
                        //}
                        //else
                        //{
                        //    Productos.APLICA_IVA = false;
                        //}
                        Productos.PORC_IVA = course.PORC_IVA;
                        Productos.CANTIDAD = course.CANTIDAD;
                        Productos.COSTO_UNIT = course.COSTO_UNIT;
                        Productos.SUBTOTAL = course.SUBTOTAL;
                        Productos.TOTAL_GENERAL = course.COSTO_TOTAL;
                        Productos.TOTAL_IVA = course.VAL_IVA;
                        db.IN_DETALLETRANSAC.Add(Productos);
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
            ViewBag.ID_BODEGA = new SelectList(Bodegas.OrderBy(o => o.NOM_BODEGA), "ID_BODEGA", "NOM_BODEGA", iN_COMPRAS.ID_BODEGA);

            return View(iN_COMPRAS);
        }


        private void LLenaCombos()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            string Usuario = Session["Usuario"].ToString();

            var par = new Funciones();

            var Proveed = db.Proveedores.ToList();

            var Bodegas = (from c in db.IN_BODEGAS
                           join a in db.IN_USERSXBOD
                           on c.ID_BODEGA equals a.ID_BODEGA
                           where c.ID_INSTITUCION == IdInst && c.PERI_CODIGO == PeriCod && a.USUA_LOGIN == Usuario
                           select c).ToList();

            Bodegas.Add(new IN_BODEGAS { ID_BODEGA = 0, NOM_BODEGA = "[Seleccione una Bodega]" });

            var Uso = (from a in db.IN_CLASES
                       from b in db.IN_ITEMS
                       where a.ID_INSTITUCION == b.ID_INSTITUCION
                       && a.PERI_CODIGO == b.PERI_CODIGO && a.ID_CLASE == b.ID_CLASE
                       select new
                       {
                           a.TIPO_USO,
                           DESRIPCION = a.TIPO_USO == 0 ? "Consumo" : "Inversion" //Uso de CASE WHEN
                       }).Distinct().ToList();

            List<IN_CLASES> Items = new List<IN_CLASES>();
            Items.Add(new IN_CLASES { TIPO_USO = 0, DESRIPCION = "[Usos]" });
            foreach (var item in Uso)
            {
                Items.Add(new IN_CLASES()
                {
                    TIPO_USO = item.TIPO_USO,
                    DESRIPCION = item.DESRIPCION
                });
            }

            ViewBag.USO = new SelectList(Items, "TIPO_USO", "DESRIPCION");

            var TiposT = db.IN_TIPOSTRAN.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod
              && f.TIPO_MOV == "").ToList();

            ViewBag.TIPO_MOV = new SelectList(TiposT, "COD_TIPOTRAN", "DESCRIP_TIPOTRAN");

            var mPolit = (from Item in db.IN_ITEMS
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod && Item.ID_BODEGA == 0
                          select Item).OrderBy(O => O.NOM_ITEM).ToList();

            mPolit.Add(new IN_ITEMS { COD_ITEM = "", NOM_ITEM = "[Seleccione un Producto]" });



            ViewBag.COD_ITEM = new SelectList(mPolit.OrderBy(o => o.NOM_ITEM), "COD_ITEM", "NOM_ITEM");

            ViewBag.ID_BODEGA = new SelectList(Bodegas.OrderBy(o => o.NOM_BODEGA), "ID_BODEGA", "NOM_BODEGA");

            Proveed.Add(new Proveedores { COD_PROV = "", NOMBRE = "[Proveedores]" });
            ViewBag.COD_PROV = new SelectList(Proveed.OrderBy(o => o.NOMBRE), "COD_PROV", "NOMBRE");
            ViewBag.AUTORIZADO_POR = new SelectList(Proveed.OrderBy(o => o.NOMBRE), "COD_PROV", "NOMBRE");

            var TipDoc = par.FillTiposDoc();
            TipDoc.Add(new TiposDoc { COD_TIPO_DOC = "", DESCRIPCION = "[Seleccione un Tipo]" });
            ViewBag.COD_TIPO_DOC = new SelectList(TipDoc.OrderBy(o => o.DESCRIPCION), "COD_TIPO_DOC", "DESCRIPCION");

            var UniOp = db.IN_UNIOPE.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod).ToList();
            UniOp.Add(new IN_UNIOPE { ID_UNIDOPERATIVA = 0, DESCRIPCION = "[Unidad Operativa]" });
            ViewBag.ID_UNIDOPERATIVA = new SelectList(UniOp.OrderBy(o => o.DESCRIPCION), "ID_UNIDOPERATIVA", "DESCRIPCION");
        }
        // GET: IN_EGRESOS/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IN_EGRESOS/Edit/5
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

        // GET: IN_EGRESOS/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IN_EGRESOS/Delete/5
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

        public ActionResult Print(int id, string TipoTran, string TipoIngre)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            int Direc = Convert.ToInt32(Session["OrgCodigo"]);
            string User = Session["Usuario"].ToString();

            DataSetInven DS = new DataSetInven();

            ReportParameter[] parameters = new ReportParameter[8];
            parameters[0] = new ReportParameter("Cargo1", "");
            parameters[1] = new ReportParameter("Nombre1", "");

            parameters[2] = new ReportParameter("Cargo2", "");
            parameters[3] = new ReportParameter("Nombre2", "");

            parameters[4] = new ReportParameter("Cargo3", "");
            parameters[5] = new ReportParameter("Nombre3", "");

            parameters[6] = new ReportParameter("Cargo4", "");
            parameters[7] = new ReportParameter("Nombre4", "");


            //List<PartIngreso> TiposD = new List<PartIngreso>();

            var Empresa = (from s in db.INSTITUCIONES
                           from t in db.CO_PARAMETROS
                           from u in db.PERIODOS
                           where s.ID_INSTITUCION == IdInst
                           && s.ID_INSTITUCION == u.ID_INSTITUCION && u.PERI_CODIGO == PeriCod
                           && u.ID_INSTITUCION == t.ID_INSTITUCION && u.PERI_CODIGO == t.PERI_CODIGO
                           select new { Cod = s.NOM_INSTITUCION, t.IVA_GASTO }).FirstOrDefault();

            //var Transac = (from a in db.IN_TRANSACCIONES
            //               from b in db.Proveedores
            //               from c in db.Proveedores
            //               from g in db.Users
            //               from e in db.IN_DETALLETRANSAC
            //               where a.ID_INSTITUCION == IdInst && a.PERI_CODIGO == PeriCod
            //                        && a.NUM_TRANSACCION == id && a.TIPO_TRAN == TipoTran && a.TIPO_INGRESO == TipoIngre
            //               && a.COD_PROV == b.COD_PROV
            //                && g.UserName == a.USER_CREA
            //                && a.AUTORIZADO_POR == c.COD_PROV
            //                 && a.ID_INSTITUCION == e.ID_INSTITUCION && a.PERI_CODIGO == e.PERI_CODIGO
            //                        && a.NUM_TRANSACCION == e.NUM_TRANSACCION && a.TIPO_TRAN == e.TIPO_TRAN
            //                        && a.TIPO_INGRESO == e.TIPO_INGRESO
            //               select a).ToList();

            var SolicDisPres =
            (from a in db.IN_TRANSACCIONES
             from b in db.Proveedores
             from c in db.Proveedores
             from d in db.IN_ITEMS
             from e in db.IN_DETALLETRANSAC
             from f in db.IN_BODEGAS
             from g in db.Users
             where a.ID_INSTITUCION == IdInst && a.PERI_CODIGO == PeriCod
                                   && a.NUM_TRANSACCION == id && a.TIPO_TRAN == TipoTran && a.TIPO_INGRESO == TipoIngre
                                   && a.COD_PROV == b.COD_PROV && a.AUTORIZADO_POR == c.COD_PROV
                                   && a.ID_INSTITUCION == e.ID_INSTITUCION && a.PERI_CODIGO == e.PERI_CODIGO
                                   && a.NUM_TRANSACCION == e.NUM_TRANSACCION && a.TIPO_TRAN == e.TIPO_TRAN
                                   && a.TIPO_INGRESO == e.TIPO_INGRESO
                                   && d.ID_INSTITUCION == e.ID_INSTITUCION && d.PERI_CODIGO == e.PERI_CODIGO
                                   && d.COD_ITEM == e.COD_ITEM
                                   && f.ID_INSTITUCION == a.ID_INSTITUCION && f.PERI_CODIGO == a.PERI_CODIGO
                                   && f.ID_BODEGA == a.ID_BODEGA
                                   && g.UserName == a.USER_CREA
             select new
             {
                 NUM_INGRESO = a.NUM_TRANSACCION,
                 b.NOMBRE,
                 UNIDOPER = "",
                 PROG_NOM = "",
                 a.NUM_DOC,
                 a.FECHA_DOC,
                 TIPO_CONSUMO = "",
                 a.DETALLE,
                 a.FECHA_TRANSAC,
                 OBJETO_RECEP = "",
                 AUTORIZADO = c.NOMBRE,
                 COD_PRODUCTO = e.COD_ITEM,
                 d.NOM_ITEM,
                 e.CANTIDAD,
                 e.COSTO_UNIT,
                 TIPOMOV = a.COD_TIPO_DOC.Trim(),
                 e.VALOR_DESC,
                 e.TOTAL_IVA,
                 TOTAL_FACTURA = e.TOTAL_GENERAL,
                 OTROS_COSTOS = 0,
                 f.NOM_BODEGA,
                 USUA_CEDULA = g.NumIdentif,
                 USUA_NOMBRE = g.FullName,
                 e.SUBTOTAL
             }).ToList();

            //Proceso para cargar la imagen del LOGO
            string pathImg = Path.Combine(Server.MapPath("~/Images"), "LOGO.ICO");
            Image image1 = Image.FromFile(pathImg);
            var ms = new MemoryStream();
            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var bytes = ms.ToArray();

            DataRow Reg2 = DS.INSTITUCIONES.NewRow();
            Reg2["NOM_INSTITUCION"] = Empresa.Cod;
            Reg2["ANIO"] = Session["Anio"];
            Reg2["LOGO"] = bytes;
            Reg2["IVAGASTO"] = Empresa.IVA_GASTO;
            DS.INSTITUCIONES.Rows.Add(Reg2);

            decimal VTotal = 0;
            foreach (var TD in SolicDisPres)
            {
                DataRow Reg = DS.TRANSACCIONES.NewRow();
                Reg["NUM_INGRESO"] = TD.NUM_INGRESO;
                Reg["NOMBRE"] = TD.NOMBRE;
                Reg["UNIDOPER"] = TD.UNIDOPER;
                Reg["PROG_NOM"] = TD.PROG_NOM;
                Reg["NUM_DOCUM"] = TD.NUM_DOC;
                Reg["FECHA_DOCUM"] = TD.FECHA_DOC;
                Reg["TIPO_CONSUMO"] = TD.TIPO_CONSUMO;
                Reg["DETALLE"] = TD.DETALLE;
                Reg["FECHA_REGISTRO"] = TD.FECHA_TRANSAC;
                Reg["NUM_TRANSAC"] = TD.NUM_INGRESO;
                Reg["OBJETO_RECEP"] = TD.OBJETO_RECEP;
                Reg["AUTORIZADO"] = TD.AUTORIZADO;
                Reg["COD_PRODUCTO"] = TD.COD_PRODUCTO;
                Reg["NOM_ITEM"] = TD.NOM_ITEM;
                Reg["CANTIDAD"] = TD.CANTIDAD;
                Reg["COSTO_UNIT"] = TD.COSTO_UNIT;
                Reg["TIPOMOV"] = TD.TIPOMOV;
                Reg["VALOR_DESC"] = TD.VALOR_DESC;
                Reg["TOTAL_IVA"] = TD.TOTAL_IVA;
                Reg["TOTAL_FACTURA"] = TD.TOTAL_FACTURA;
                Reg["OTROS_COSTOS"] = TD.OTROS_COSTOS;
                Reg["NOM_BODEGA"] = TD.NOM_BODEGA;
                Reg["USUA_CEDULA"] = TD.USUA_CEDULA;
                Reg["USUA_CEDULA"] = TD.USUA_NOMBRE;
                Reg["SUBTOTAL"] = TD.SUBTOTAL;
                //VTotal = TD.VALOR + VTotal;
                DS.TRANSACCIONES.Rows.Add(Reg);
            }

            LocalReport lr = new LocalReport();

            var allFondos = db.FIRMAS.Where(d => d.ID_INSTITUCION == IdInst
            && d.PERI_CODIGO == PeriCod && d.CODIGO_DOC == 37 && d.USUA_FIRMA == User).OrderBy(o => o.NUM_FILA);

            foreach (var course in allFondos)
            {
                switch (course.NUM_FILA)
                {
                    case 1:
                        parameters[0] = new ReportParameter("Cargo1", course.NOMBRE);
                        parameters[1] = new ReportParameter("Nombre1", course.CARGO);
                        break;
                    case 2:
                        parameters[2] = new ReportParameter("Cargo2", course.NOMBRE);
                        parameters[3] = new ReportParameter("Nombre2", course.CARGO);
                        break;
                    case 3:
                        parameters[4] = new ReportParameter("Cargo3", course.NOMBRE);
                        parameters[5] = new ReportParameter("Nombre3", course.CARGO);
                        break;
                    case 4:
                        parameters[6] = new ReportParameter("Cargo4", course.NOMBRE);
                        parameters[7] = new ReportParameter("Nombre4", course.CARGO);
                        break;
                }
            }

            //parameters[8] = new ReportParameter("LaSumade", VariablesGlobales.mNumLetra.Convertir(Convert.ToString(VTotal), true));
            //parameters[0] = new ReportParameter("Cargo1", "Cargo");
            //string path = Path.Combine(Server.MapPath("~/Reportes/Presupuesto"), "Report1.rdlc");
            string path = Path.Combine(Server.MapPath("~/Reportes/Inventarios"), "RepEgreso2.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            ReportDataSource rd = new ReportDataSource("DSetLogo", DS.Tables["INSTITUCIONES"]);
            ReportDataSource rd2 = new ReportDataSource("DSetTransacciones", DS.Tables["TRANSACCIONES"]);

            lr.DataSources.Add(rd);
            lr.DataSources.Add(rd2);

            lr.SetParameters(parameters);
            lr.Refresh();

            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutPutFormat>" + "PDF" + "</OutPutFormat>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] strams;
            byte[] renderedBytes;
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out strams,
                out warnings);

            return File(renderedBytes, mimeType);

            //byte[] file = lr.Render("Excel");

            //return File(new MemoryStream(file).ToArray(),
            //          System.Net.Mime.MediaTypeNames.Application.Octet,
            //          /*Esto para forzar la descarga del archivo*/
            //          string.Format("{0}{1}", "archivoprueba.", "Excel"));

            //string reportType
            //return RedirectToAction("Index");
        }
    }
}
