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
    [Authorize(Roles = "Master,Admin")]
    public class InstitucionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Instituciones
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            return View(db.INSTITUCIONES.ToList());
        }

        // GET: Instituciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instituciones instituciones = db.INSTITUCIONES.Find(id);
            if (instituciones == null)
            {
                return HttpNotFound();
            }
            return View(instituciones);
        }

        // GET: Instituciones/Create
        public ActionResult Create(int? id)
        {
            Instituciones instituciones = db.INSTITUCIONES.Find(id);

            if (id != null)
            {
                FillCatalogos(instituciones, 0);
                ViewBag.EsNuevo = 0;
            }
            else
            {
                FillCatalogos(instituciones, 1);
                ViewBag.EsNuevo = 1;
            }

            return View(instituciones);
        }

        // POST: Instituciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_INSTITUCION,TIPO_PRESUP,COD_INSTITUCION,NOM_INSTITUCION,RUC_INSTITUCION,EMAIL,TIPO_DOCREPLEGAL,NUMDOC_REPLEGAL,NOM_REPLEGAL,RUC_CONTADOR,NOM_CONTADOR,DIRECCION,TELEFONO,FAX,ID_BASE,SERVER,CATALOGO,TIPOAMB,OBLIGACONTAB,NUMRESOL,EMAILPASSWORD,SMTPHOST,SMTPPORT,DIRFIRMADIGITAL,PASSWFIRMADIGITAL,DIRSRI,ID_PROVINCIA,ID_CANTON,FECHA_INICIO,CONTRIB_ESP,DATO_SEGURO")] Instituciones instituciones, int EsNuevo)
        {
            instituciones.FAX = "0";
            instituciones.ID_BASE = 0;
            instituciones.SERVER = "0";
            instituciones.CATALOGO = "0";
            instituciones.TIPOAMB = 1;
            instituciones.OBLIGACONTAB = "SI";
            instituciones.NUMRESOL = 0;
            instituciones.EMAILPASSWORD = "0";
            instituciones.SMTPHOST = "0";
            instituciones.SMTPPORT = 0;
            instituciones.DIRFIRMADIGITAL = "0";
            instituciones.PASSWFIRMADIGITAL = "0";
            instituciones.DIRSRI = "0";
            instituciones.FECHA_INICIO = DateTime.Today;

            if (ModelState.IsValid)
            {

            }

            try
            {
                if (EsNuevo == 1)
                {
                    db.INSTITUCIONES.Add(instituciones);
                }
                else
                {
                    db.Entry(instituciones).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                //TempData["MsgError"] = "Ha ocurrido un error, revise los datos por favor...!";
                ViewBag.MsgError = TempData["MsgError"];
            }
            FillCatalogos(instituciones, EsNuevo);
            ViewBag.EsNuevo = EsNuevo;
            return View(instituciones);
        }

        private void FillCatalogos(Instituciones instituciones, int EsNuevo)
        {
            var TipoPresup = new List<TipoPres>
            {
                new TipoPres{TIPO_PRESUP=1,DescTipoPres="Empresa Privada"},
                new TipoPres{TIPO_PRESUP=2,DescTipoPres="Entidades del Gobierno Central"},
                new TipoPres{TIPO_PRESUP=3,DescTipoPres="Entidades Descentralizadas Autónomas"},
                new TipoPres{TIPO_PRESUP=4,DescTipoPres="Entidades de Seguridad Social"},
                new TipoPres{TIPO_PRESUP=5,DescTipoPres="Empresas Públicas"},
                new TipoPres{TIPO_PRESUP=6,DescTipoPres="Entidades Financieras Públicas"},
                new TipoPres{TIPO_PRESUP=7,DescTipoPres="Entidades del Régimen Seccional Autónomo"}
            };

            var TipoDoc = new List<TipoDocRep>
            {
                new TipoDocRep{CodTipo="RUC", NomTipo="R.U.C."},
                new TipoDocRep{CodTipo="Cédula", NomTipo="Cédula"},
                new TipoDocRep{CodTipo="Pasaporte", NomTipo="Pasaporte"}
            };

            var mProv = db.PROVINCIAS.ToList();

            if (EsNuevo == 1)
            {
                var mCanton = db.CANTONES.Where(f => f.ID_PROVINCIA == 1);
                ViewBag.TIPO_PRESUP = new SelectList(TipoPresup, "TIPO_PRESUP", "DescTipoPres", 1);
                ViewBag.ID_PROVINCIA = new SelectList(mProv, "ID_PROVINCIA", "DESCRIPCION", 0);
                ViewBag.TIPO_DOCREPLEGAL = new SelectList(TipoDoc, "CodTipo", "NomTipo", 1);
                ViewBag.ID_CANTON = new SelectList(mCanton, "ID_CANTON", "DESCRIPCION");
            }
            else
            {
                int Provi = Convert.ToInt32(instituciones.ID_PROVINCIA);
                var mCanton = db.CANTONES.Where(f => f.ID_PROVINCIA == instituciones.ID_PROVINCIA);
                ViewBag.TIPO_PRESUP = new SelectList(TipoPresup, "TIPO_PRESUP", "DescTipoPres", instituciones.TIPO_PRESUP);
                ViewBag.ID_PROVINCIA = new SelectList(mProv.OrderBy(o => o.DESCRIPCION), "ID_PROVINCIA", "DESCRIPCION", Provi);
                ViewBag.TIPO_DOCREPLEGAL = new SelectList(TipoDoc, "CodTipo", "NomTipo", instituciones.TIPO_DOCREPLEGAL);
                ViewBag.ID_CANTON = new SelectList(mCanton, "ID_CANTON", "DESCRIPCION", instituciones.ID_CANTON);
            }

        }
        // GET: Instituciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instituciones instituciones = db.INSTITUCIONES.Find(id);
            if (instituciones == null)
            {
                return HttpNotFound();
            }

            var TipoPresup = new List<TipoPres>
            {
                new TipoPres{TIPO_PRESUP=1,DescTipoPres="Empresa Privada"},
                new TipoPres{TIPO_PRESUP=2,DescTipoPres="Entidades del Gobierno Central"},
                new TipoPres{TIPO_PRESUP=3,DescTipoPres="Entidades Descentralizadas Autónomas"},
                new TipoPres{TIPO_PRESUP=4,DescTipoPres="Entidades de Seguridad Social"},
                new TipoPres{TIPO_PRESUP=5,DescTipoPres="Empresas Públicas"},
                new TipoPres{TIPO_PRESUP=6,DescTipoPres="Entidades Financieras Públicas"},
                new TipoPres{TIPO_PRESUP=7,DescTipoPres="Entidades del Régimen Seccional Autónomo"}
            };

            ViewBag.TIPO_PRESUP = new SelectList(TipoPresup, "TIPO_PRESUP", "DescTipoPres", 1);

            var TipoDoc = new List<TipoDocRep>
            {
                new TipoDocRep{CodTipo="RUC", NomTipo="R.U.C."},
                new TipoDocRep{CodTipo="Cédula", NomTipo="Cédula"},
                new TipoDocRep{CodTipo="Pasaporte", NomTipo="Pasaporte"}
            };
            ViewBag.TIPO_DOCREPLEGAL = new SelectList(TipoDoc, "CodTipo", "NomTipo", 1);

            List<Provincias> Prov = new List<Provincias>();
            var mProv = (from Provincias in db.PROVINCIAS
                         select Provincias).ToList();
            ViewBag.ID_PROVINCIA = new SelectList(mProv, "ID_PROVINCIA", "DESCRIPCION", 0);

            return View(instituciones);
        }

        // POST: Instituciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_INSTITUCION,TIPO_PRESUP,COD_INSTITUCION,NOM_INSTITUCION,RUC_INSTITUCION,EMAIL,TIPO_DOCREPLEGAL,NUMDOC_REPLEGAL,NOM_REPLEGAL,RUC_CONTADOR,NOM_CONTADOR,DIRECCION,TELEFONO,FAX,ID_BASE,SERVER,CATALOGO,TIPOAMB,OBLIGACONTAB,NUMRESOL,EMAILPASSWORD,SMTPHOST,SMTPPORT,DIRFIRMADIGITAL,PASSWFIRMADIGITAL,DIRSRI,ID_PROVINCIA,ID_CANTON,FECHA_INICIO,CONTRIB_ESP")] Instituciones instituciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instituciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instituciones);
        }

        // GET: Instituciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instituciones instituciones = db.INSTITUCIONES.Find(id);
            if (instituciones == null)
            {
                return HttpNotFound();
            }
            return View(instituciones);
        }

        // POST: Instituciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instituciones instituciones = db.INSTITUCIONES.Find(id);
            try
            {
                db.INSTITUCIONES.Remove(instituciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                TempData["MsgError"] = Ex.InnerException.InnerException.Message;
                //TempData["MsgError"] = "Ha ocurrido un error, revise los datos por favor...!";
                ViewBag.MsgError = TempData["MsgError"];
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult LLenaCantones(int IdProv)
        {
            List<Cantones> Canton = new List<Cantones>();
            var mCanton = (from Cantones in db.CANTONES
                           where Cantones.ID_PROVINCIA == IdProv
                           select Cantones).ToList();

            foreach (var item in mCanton)
            {
                Canton.Add(new Cantones()
                {
                    ID_CANTON = item.ID_CANTON,
                    DESCRIPCION = item.DESCRIPCION
                });
            }
            //Canton.Add(new Cantones { ID_CANTON = 0, DESCRIPCION = "[Cantones]" });
            //Canton.OrderBy(o => o.ID_CANTON);
            return Json(Canton, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LLenaParroquias(int IdProv, int IdCanton)
        {
            List<Parroquia> Parroq = new List<Parroquia>();
            var mParroquia = (from Parroquia in db.PARROQUIAS
                              where Parroquia.ID_PROVINCIA == IdProv
                              && Parroquia.ID_CANTON == IdCanton
                              select Parroquia).ToList();

            foreach (var item in mParroquia)
            {
                Parroq.Add(new Parroquia()
                {
                    ID_PARROQUIA = item.ID_PARROQUIA,
                    DESCRIPCION = item.DESCRIPCION
                });
            }
            return Json(Parroq, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Periodos(int id)
        {
            //     var Courses = db.Documentos
            //.Include(c => c.TIPOS_DOCUMEN)
            ////.Include(d => d.DIRECCIONES)
            //.AsNoTracking()
            //.Where(d => d.UserName.ToString().Contains(Usuario))
            //.ToList();

            //var pERIODOS = db.PERIODOS.Include(p => p.INSITUCIONES)
            //    .AsNoTracking()
            //    .Where(d => d.ID_INSTITUCION == id);
            return RedirectToAction("Index", "Periodos", new { id = id });
            //return View("index");
        }
    }
}
