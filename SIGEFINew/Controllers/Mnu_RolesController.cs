using SIGEFINew.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
namespace SIGEFINew.Controllers
{
    public class Mnu_RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Mnu_Roles
        public ActionResult Index()
        {
            int IdEmp = Convert.ToInt32(Session["IdInst"]);
            var mNuRoles = db.MNU_ROLES.Include(p => p.PERIODOS)
                .AsNoTracking()
                .Where(d => d.ID_INSTITUCION == IdEmp);
            return View(mNuRoles.ToList());
        }

        // GET: Mnu_Roles/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mnu_Roles/Create
        public ActionResult Create()
        {
            ViewBag.PERI_CODIGO = new SelectList(db.PERIODOS, "PERI_CODIGO", "PERI_DESCRIPCION");
            return View();
        }

        // POST: Mnu_Roles/Create
        [HttpPost]
        public ActionResult Create(Mnu_Roles mNuroles)
        {
            mNuroles.CODIGO_ROL = 1;
            //mNuroles.ID_INSTITUCION = (int)Session["IdInst"];
            //mNuroles.USUA_CREA = Session["Usuario"].ToString();
            mNuroles.FECHA_CREA = System.DateTime.Today;
            //mNuroles.USUA_MODIF = "ALGUIEN";
            //mNuroles.FECHA_MODIF = System.DateTime.Today;
            db.MNU_ROLES.Add(mNuroles);
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PERI_CODIGO = new SelectList(db.PERIODOS, "PERI_CODIGO", "PERI_DESCRIPCION");
            return View(mNuroles);
        }

        // GET: Mnu_Roles/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mnu_Roles/Edit/5
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

        // GET: Mnu_Roles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mnu_Roles/Delete/5
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
