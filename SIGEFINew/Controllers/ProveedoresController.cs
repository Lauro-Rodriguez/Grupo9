using PagedList;
using SIGEFINew.Clases;
using SIGEFINew.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace SIGEFINew.Controllers
{
    [Authorize(Roles = "Master,Proveedor")]
    public class ProveedoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Proveedores
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proveedores = db.Proveedores.Include(p => p.Parroquias);
            ViewBag.Nombre = String.IsNullOrEmpty(sortOrder) ? "nombre" : "";
            ViewBag.Nombre = sortOrder == "string" ? "nombre" : "string";

            string Server = "(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.3.160)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCLCDB)))";
            string Cadena = VariablesGlobales.DMClase.get_CadenaConexion(2,Server,"sispro", "sispro", "Sispro123");

            //VariablesGlobales.DMClase.SetConexion(Cadena);
            VariablesGlobales.DMClase.SetConexionWeb(Cadena);
            DataOracle.DataFacturacion DO = new DataOracle.DataFacturacion();

            //VariablesGlobales.DMClase.ProcesarFacturas();

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
                proveedores = proveedores.Where(s => s.NOMBRE.Contains(searchString)
                                       || s.NUMCEDRUC_PROV.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nombre":
                    proveedores = proveedores.OrderByDescending(s => s.NOMBRE);
                    break;
                case "numcedruc_prov":
                    proveedores = proveedores.OrderBy(s => s.NUMCEDRUC_PROV);
                    break;
                default:
                    proveedores = proveedores.OrderBy(s => s.NOMBRE);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentFilter = searchString;

            return View(proveedores.ToPagedList(pageNumber, pageSize));
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // GET: Proveedores/Create
        public ActionResult Create(string id)
        {
            Proveedores proveedores = db.Proveedores.Find(id);
            if (id != null)
            {
                ViewBag.EsNuevo = 0;
                FillCatalogos(proveedores, 0);
            }
            else
            {
                ViewBag.EsNuevo = 1;
                FillCatalogos(proveedores, 1);
            }


            return View(proveedores);
        }

        // POST: Proveedores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "COD_PROV,NUMCEDRUC_PROV,TIPO_IDENTI,TIPO_PROV,NOMBRE,DIRECCION,TELEFONO,CODIGO_ENVIO,CODIGO_BANCO,NUM_CUENTA,TIPO_CUENTA,TIPO_IDENTITRAN,RUCCEDU_TRAN,RAZONSOCIAL,CXC_PROVEED,CXC_ANTICIPREC,CXP_PROVEED,CXP_ANTICIPENT,CONTRIBESPECIAL,OBLIGA_CONTAB,INSTIT_PUB,SEXO,EMAIL,CODI_CLASRET,ID_PROVINCIA,ID_CANTON,ID_PARROQUIA,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF")] Proveedores proveedores, int EsNuevo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (EsNuevo == 1)
                    {
                        db.Proveedores.Add(proveedores);
                    }
                    else
                    {
                        proveedores.USER_MODIF = Session["Usuario"].ToString();
                        proveedores.FECHA_MODIF = DateTime.Today;
                        db.Entry(proveedores).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception Ex)
                {
                    TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                    ViewBag.MsgError = TempData["MsgError"];
                }
            }

            FillCatalogos(proveedores, EsNuevo);

            ViewBag.EsNuevo = EsNuevo;
            return View(proveedores);
        }

        private void FillCatalogos(Proveedores Prov, int EsNuevo)
        {
            var Tipo = new List<TipoPersona>
            {
                new TipoPersona{TipoPerson=1,DescTipoPerson="Contibuyente Especial"},
                new TipoPersona{TipoPerson=2,DescTipoPerson="Sociedad / Persona natural obligada o no a llevar contabilidad"},
                new TipoPersona{TipoPerson=3,DescTipoPerson="Se Emite Liquidación de Compras de Bienes o Adquisición de Servicios (Incluye Pagos por Arrendamiento al Exterior)"},
                new TipoPersona{TipoPerson=4,DescTipoPerson="Profesionales"},
                new TipoPersona{TipoPerson=5,DescTipoPerson="Por Arrendamiento de Bienes Inmuebles Propios"}
            };

            var TipoCta = new List<TipoCuenta>
            {
                new TipoCuenta{TipoCta=1,DescTipoCuenta="Cta. Corriente"},
                new TipoCuenta{TipoCta=2,DescTipoCuenta="Cta. Ahorros"},
                new TipoCuenta{TipoCta=3,DescTipoCuenta="Cta. Especial Pagos"},
            };

            if (EsNuevo == 1)
            {
                var Canton = db.CANTONES.Where(f => f.ID_PROVINCIA == 1);
                ViewBag.ID_CANTON = new SelectList(Canton, "ID_CANTON", "DESCRIPCION");

                var Parroquia = db.PARROQUIAS.Where(f => f.ID_PROVINCIA == 1 && f.ID_CANTON == 1);
                ViewBag.ID_PARROQUIA = new SelectList(Parroquia, "ID_PARROQUIA", "DESCRIPCION");

                ViewBag.ID_PROVINCIA = new SelectList(db.PROVINCIAS, "ID_PROVINCIA", "DESCRIPCION");

                ViewBag.INSTIT_PUB = new SelectList(Tipo, "TipoPerson", "DescTipoPerson");
                ViewBag.TIPO_CUENTA = new SelectList(TipoCta, "tIPOcTA", "DescTipoCuenta");

                ViewBag.CODIGO_BANCO = new SelectList(db.BANCOS, "CODIGO_BANCO", "DESCRIPCION");

                ViewBag.CXC_PROVEED = new SelectList(db.CO_CUENTASCONT, "CODIGO_CG", "CODIGO_NOMBRE");
                ViewBag.CXP_PROVEED = new SelectList(db.CO_CUENTASCONT, "CODIGO_CG", "CODIGO_NOMBRE");
                ViewBag.CXC_ANTICIPREC = new SelectList(db.CO_CUENTASCONT, "CODIGO_CG", "CODIGO_NOMBRE");
                ViewBag.CXP_ANTICIPENT = new SelectList(db.CO_CUENTASCONT, "CODIGO_CG", "CODIGO_NOMBRE");
            }
            else
            {
                ViewBag.ID_PROVINCIA = new SelectList(db.PROVINCIAS, "ID_PROVINCIA", "DESCRIPCION", Prov.ID_PROVINCIA);

                var Canton = db.CANTONES.Where(f => f.ID_PROVINCIA == Prov.ID_PROVINCIA);
                ViewBag.ID_CANTON = new SelectList(Canton, "ID_CANTON", "DESCRIPCION", Prov.ID_CANTON);

                var Parroquia = db.PARROQUIAS.Where(f => f.ID_PROVINCIA == Prov.ID_PROVINCIA && f.ID_CANTON == Prov.ID_CANTON);
                ViewBag.ID_PARROQUIA = new SelectList(Parroquia, "ID_PARROQUIA", "DESCRIPCION", Prov.ID_PARROQUIA);

                ViewBag.INSTIT_PUB = new SelectList(Tipo, "TipoPerson", "DescTipoPerson", Prov.INSTIT_PUB);
                ViewBag.TIPO_CUENTA = new SelectList(TipoCta, "tIPOcTA", "DescTipoCuenta", Prov.TIPO_CUENTA);

                ViewBag.CODIGO_BANCO = new SelectList(db.BANCOS, "CODIGO_BANCO", "DESCRIPCION", Prov.CODIGO_BANCO);

                ViewBag.CXC_PROVEED = new SelectList(db.CO_CUENTASCONT, "CODIGO_CG", "CODIGO_NOMBRE", Prov.CXC_PROVEED);
                ViewBag.CXP_PROVEED = new SelectList(db.CO_CUENTASCONT, "CODIGO_CG", "CODIGO_NOMBRE", Prov.CXP_PROVEED);
                ViewBag.CXC_ANTICIPREC = new SelectList(db.CO_CUENTASCONT, "CODIGO_CG", "CODIGO_NOMBRE", Prov.CXC_ANTICIPREC);
                ViewBag.CXP_ANTICIPENT = new SelectList(db.CO_CUENTASCONT, "CODIGO_CG", "CODIGO_NOMBRE", Prov.CXP_ANTICIPENT);
            }

            ViewBag.UserCrea = Session["Usuario"].ToString();
            ViewBag.FechaCrea = DateTime.Today.ToString("dd/MM/yyyy");
        }
        // GET: Proveedores/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ID_PROVINCIA = new SelectList(db.Parroquias, "ID_PROVINCIA", "DESCRIPCION", proveedores.ID_PROVINCIA);
            return View(proveedores);
        }

        // POST: Proveedores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "COD_PROV,NUMCEDRUC_PROV,TIPO_IDENTI,TIPO_PROV,NOMBRE,DIRECCION,TELEFONO,CODIGO_ENVIO,NUM_CUENTA,TIPO_CUENTA,TIPO_IDENTITRAN,RUCCEDU_TRAN,RAZONSOCIAL,CXC_PROVEED,CXC_ANTICIPREC,CXP_PROVEED,CXP_ANTICIPENT,CONTRIBESPECIAL,INSTIT_PUB,SEXO,EMAIL,CODI_CLASRET,ID_PROVINCIA,ID_CANTON,ID_PARROQUIA,USER_CREA,FECHA_CREA,USER_MODIF,FECHA_MODIF")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.ID_PROVINCIA = new SelectList(db.Parroquias, "ID_PROVINCIA", "DESCRIPCION", proveedores.ID_PROVINCIA);
            return View(proveedores);
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Proveedores proveedores = db.Proveedores.Find(id);
            try
            {
                db.Proveedores.Remove(proveedores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                ViewBag.MsgError = TempData["MsgError"];
            }
            return View(proveedores);
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
