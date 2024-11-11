using SIGEFINew.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGEFINew.Controllers
{
    [Authorize]
    public class DOCUMSPDFSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DOCUMSPDFS
        public ActionResult Index(int? CodPlan, string CodPObtvo, int? CodFase)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            
            string Codigo = CodPlan.ToString().Trim() + CodPObtvo + CodFase.ToString().Trim();

            var dOCSPFSD = db.DOCUMSPDFS.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod && f.CODIGO == Codigo);

            ViewBag.CodPlan = CodPlan;
            ViewBag.CodPObtvo = CodPObtvo;
            ViewBag.CodFase = CodFase;

            return View(dOCSPFSD.ToList());
        }

        public ActionResult SubirArchivo(HttpPostedFileBase file, int CodPlan, string CodPObtvo, int CodFase)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            string Codigo = CodPlan.ToString().Trim() + CodPObtvo + CodFase.ToString().Trim();

            DOCUMSPDFS dOCUMSPDFS=new DOCUMSPDFS();

            string path = Server.MapPath("~/Documentos/PDF/POA/");
            
            HttpFileCollectionBase files = Request.Files;
            var Ruta = "";

            if (file != null && file.ContentLength > 0)
            {
                string FPath = "";
                for (int i = 0; i < files.Count; i++)
                {
                    file = files[i];
                    var ExisteArc = db.DOCUMSPDFS.Where(f => f.NOM_ARCHIVO == file.FileName).ToList();
                    if (ExisteArc.Count == 0)
                    {
                        file.SaveAs(path + file.FileName);
                        Ruta = "~/Documentos/PDF/POA/" + file.FileName;
                        FPath = path + file.FileName;
                    }
                    
                }

                dOCUMSPDFS.ID_INSTITUCION = IdInst;
                dOCUMSPDFS.PERI_CODIGO = PeriCod;
                dOCUMSPDFS.CODIGO = Codigo;
                dOCUMSPDFS.MODULO = "POA";
                dOCUMSPDFS.PATH = Ruta;
                dOCUMSPDFS.NOM_ARCHIVO = file.FileName; 

                try
                {
                    db.DOCUMSPDFS.Add(dOCUMSPDFS);
                    db.SaveChanges();
                    //Process.Start(FPath);
                }
                catch (Exception ex)
                {
                    // Aquí el código para manejar la Excepción.
                }
            }



            //if (file != null && file.ContentLength > 0)
            //{
            //    // Extraemos el contenido en Bytes del archivo subido.
            //    var _contenido = new byte[file.ContentLength];
            //    file.InputStream.Read(_contenido, 0, file.ContentLength);

            //    // Separamos el Nombre del archivo de la Extensión.
            //    int indiceDelUltimoPunto = file.FileName.LastIndexOf('.');
            //    string _nombre = file.FileName.Substring(0, indiceDelUltimoPunto);
            //    string _extension = file.FileName.Substring(indiceDelUltimoPunto + 1,
            //                        file.FileName.Length - indiceDelUltimoPunto - 1);

            //    // Instanciamos la clase Archivo y asignammos los valores.
            //    DOCUMSPDFS _archivo = new DOCUMSPDFS()
            //    {
            //        NOM_ARCHIVO = _nombre,
            //        MODULO = "POA",
            //        CODIGO = "C"
            //    };

            //    try
            //    {
            //        // Subimos el archivo al Servidor.
            //        _archivo.SubirArchivo(_contenido);
            //        // Guardamos en la base de datos la instancia del archivo
            //        using (db = new ApplicationDbContext())
            //        {
            //            db.DOCUMSPDFS.Add(_archivo);
            //            db.SaveChanges();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        // Aquí el código para manejar la Excepción.
            //    }
            //}
            return RedirectToAction("Index", "DOCUMSPDFS", new { @CodPlan = CodPlan, @CodPObtvo = CodPObtvo, @CodFase = CodFase });
        }
        // GET: DOCUMSPDFS/Details/5

        [HttpGet]
        public void DownloadFile(string FPath)
        {
            string filename = FPath;
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
            string aaa = Server.MapPath(filename);
            Response.TransmitFile(Server.MapPath( filename));
            Response.End();

            //return File(Archivo, "application/xml", "Archivo.xml");


        }
        public ActionResult VerFiles(string FPath)
        {
            Process.Start(FPath);
            return null;
        }

        public ActionResult DeleteFiles(string FPath, string contentType, string fileDownloadName, int CodPlan, string CodPObtvo, int CodFase, string Codigo,int NumFila)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            DOCUMSPDFS dOCUMSPDFS = db.DOCUMSPDFS.Find(IdInst, PeriCod, Codigo, NumFila);

            string path = Server.MapPath("~/Documentos/PDF/POA/")+dOCUMSPDFS.NOM_ARCHIVO;

            try
            {
                var bytes = System.IO.File.ReadAllBytes(path);
                System.IO.File.Delete(path);
            }
            catch(Exception ex)
            {

            }
           

            db.DOCUMSPDFS.Remove(dOCUMSPDFS);
            db.SaveChanges();

            return RedirectToAction("Index", "DOCUMSPDFS", new { @CodPlan = CodPlan, @CodPObtvo = CodPObtvo, @CodFase = CodFase });
        }

        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: DOCUMSPDFS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DOCUMSPDFS/Create
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

        // GET: DOCUMSPDFS/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DOCUMSPDFS/Edit/5
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

        // GET: DOCUMSPDFS/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DOCUMSPDFS/Delete/5
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
