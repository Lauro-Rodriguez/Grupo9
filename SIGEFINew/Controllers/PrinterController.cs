using Microsoft.Reporting.WebForms;
using SIGEFINew.Models;
using SIGEFINew.Reportes;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SIGEFINew.Controllers
{
    public class PrinterController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        DataSetRep DS = new DataSetRep();
        // GET: Printer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrintCargaIniIng()
        {
            //List<PartIngreso> TiposD = new List<PartIngreso>();
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var CargaIni =
                from PR in db.Programas
                join SP in db.SubProgramas on PR.PROG_CODIGO equals SP.PROG_CODIGO
                join PRY in db.Proyectoes on SP.SUBP_CODIGO equals PRY.SUBP_CODIGO
                join AC in db.Actividads on PRY.PROY_CODIGO equals AC.PROY_CODIGO
                join PI in db.PartIngresoes on AC.ACTI_CODIGO equals PI.ACTI_CODIGO
                join GR in db.Grupoes on PI.GRUP_CODIGO equals GR.GRUP_CODIGO
                join DI in db.DETALLEINGRESOS on PI.PAIN_CODIGO equals DI.PAIN_CODIGO
                where PR.ID_INSTITUCION == SP.ID_INSTITUCION &&
                SP.ID_INSTITUCION == PRY.ID_INSTITUCION && SP.PROG_CODIGO == PRY.PROG_CODIGO &&
                PRY.ID_INSTITUCION == AC.ID_INSTITUCION && PRY.PROG_CODIGO == AC.PROG_CODIGO && PRY.SUBP_CODIGO == AC.SUBP_CODIGO &&
                AC.ID_INSTITUCION == PI.ID_INSTITUCION && AC.PROG_CODIGO == PI.PROG_CODIGO && AC.SUBP_CODIGO == PI.SUBP_CODIGO &&
                AC.PROY_CODIGO == PI.PROY_CODIGO &&
                DI.PERI_CODIGO == PeriCod &&
                GR.ID_INSTITUCION == PI.ID_INSTITUCION &&
                DI.ID_INSTITUCION == PI.ID_INSTITUCION &&
                DI.ID_INSTITUCION == IdInst && DI.PERI_CODIGO == PeriCod
                select new
                {
                    PR.PROG_CODIGO,
                    PR.PROG_NOMBRE,
                    SP.SUBP_CODIGO,
                    SUBP_NOMBRE = SP.PROG_NOMBRE,
                    PRY.PROY_CODIGO,
                    PRY.PROY_NOMBRE,
                    AC.ACTI_CODIGO,
                    AC.ACTI_NOMBRE,
                    GR.GRUP_CODIGO,
                    GR.GRUP_NOMBRE,
                    PI.PAIN_CLAVE,
                    PI.PAIN_NOMBRE,
                    DI.DETI_VALOR
                };

            //Proceso para cargar la imagen del LOGO
            string pathImg = Path.Combine(Server.MapPath("~/Images"), "IMAGE1.JPG");
            Image image1 = Image.FromFile(pathImg);
            var ms = new MemoryStream();
            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var bytes = ms.ToArray();

            var Empresa = (from s in db.INSTITUCIONES
                           where s.ID_INSTITUCION == IdInst
                           select new { Cod = s.NOM_INSTITUCION }).FirstOrDefault();

            DataRow Reg2 = DS.INSTITUCIONES.NewRow();
            Reg2["NOM_INSTITUCION"] = Empresa.Cod;
            Reg2["ANIO"] = Session["Anio"];
            Reg2["LOGO"] = bytes;
            DS.INSTITUCIONES.Rows.Add(Reg2);

            foreach (var TD in CargaIni)
            {
                DataRow Reg = DS.CARGANI.NewRow();
                Reg["PROG_CODIGO"] = TD.PROG_CODIGO;
                Reg["PROG_NOMBRE"] = TD.PROG_NOMBRE;
                Reg["SUBP_CODIGO"] = TD.SUBP_CODIGO;
                Reg["SUBP_NOMBRE"] = TD.SUBP_NOMBRE;
                Reg["PROY_CODIGO"] = TD.PROY_CODIGO;
                Reg["PROY_NOMBRE"] = TD.PROY_NOMBRE;
                Reg["ACTI_CODIGO"] = TD.ACTI_CODIGO;
                Reg["ACTI_NOMBRE"] = TD.ACTI_NOMBRE;
                Reg["GRUP_CODIGO"] = TD.GRUP_CODIGO;
                Reg["GRUP_NOMBRE"] = TD.GRUP_NOMBRE;
                Reg["PAIN_CLAVE"] = TD.PAIN_CLAVE;
                Reg["PAIN_NOMBRE"] = TD.PAIN_NOMBRE;
                Reg["DETI_VALOR"] = TD.DETI_VALOR;
                //Reg["TIPO"] = "INGRESOS";
                DS.CARGANI.Rows.Add(Reg);
            }

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reportes/Presupuesto"), "RepCargaIni.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            ReportDataSource rd = new ReportDataSource("DSetLogo", DS.Tables["INSTITUCIONES"]);
            ReportDataSource rd2 = new ReportDataSource("DSetDetPartidas", DS.Tables["CARGANI"]);
            lr.DataSources.Add(rd);
            lr.DataSources.Add(rd2);
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

        public ActionResult PrintCargaIniEgr()
        {
            //List<PartIngreso> TiposD = new List<PartIngreso>();
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var CargaIni =
                from PR in db.Programas
                join SP in db.SubProgramas on PR.PROG_CODIGO equals SP.PROG_CODIGO
                join PRY in db.Proyectoes on SP.SUBP_CODIGO equals PRY.SUBP_CODIGO
                join AC in db.Actividads on PRY.PROY_CODIGO equals AC.PROY_CODIGO
                join PI in db.PartEgresoes on AC.ACTI_CODIGO equals PI.ACTI_CODIGO
                join GR in db.Grupoes on PI.GRUP_CODIGO equals GR.GRUP_CODIGO
                join DI in db.DETALLEGRESOS on PI.PAEG_CODIGO equals DI.PAEG_CODIGO
                where PR.ID_INSTITUCION == SP.ID_INSTITUCION &&
                SP.ID_INSTITUCION == PRY.ID_INSTITUCION && SP.PROG_CODIGO == PRY.PROG_CODIGO &&
                PRY.ID_INSTITUCION == AC.ID_INSTITUCION && PRY.PROG_CODIGO == AC.PROG_CODIGO && PRY.SUBP_CODIGO == AC.SUBP_CODIGO &&
                AC.ID_INSTITUCION == PI.ID_INSTITUCION && AC.PROG_CODIGO == PI.PROG_CODIGO && AC.SUBP_CODIGO == PI.SUBP_CODIGO &&
                AC.PROY_CODIGO == PI.PROY_CODIGO &&
                DI.PERI_CODIGO == PeriCod &&
                GR.ID_INSTITUCION == PI.ID_INSTITUCION &&
                DI.ID_INSTITUCION == PI.ID_INSTITUCION &&
                DI.ID_INSTITUCION == IdInst && DI.PERI_CODIGO == PeriCod
                select new
                {
                    PR.PROG_CODIGO,
                    PR.PROG_NOMBRE,
                    SP.SUBP_CODIGO,
                    SUBP_NOMBRE = SP.PROG_NOMBRE,
                    PRY.PROY_CODIGO,
                    PRY.PROY_NOMBRE,
                    AC.ACTI_CODIGO,
                    AC.ACTI_NOMBRE,
                    GR.GRUP_CODIGO,
                    GR.GRUP_NOMBRE,
                    PI.PAEG_CLAVE,
                    PI.PAEG_NOMBRE,
                    DI.VALOR_TOTAL
                };

            //Proceso para cargar la imagen del LOGO
            string pathImg = Path.Combine(Server.MapPath("~/Images"), "IMAGE1.JPG");
            Image image1 = Image.FromFile(pathImg);
            var ms = new MemoryStream();
            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var bytes = ms.ToArray();

            var Empresa = (from s in db.INSTITUCIONES
                           where s.ID_INSTITUCION == IdInst
                           select new { Cod = s.NOM_INSTITUCION }).FirstOrDefault();

            DataRow Reg2 = DS.INSTITUCIONES.NewRow();
            Reg2["NOM_INSTITUCION"] = Empresa.Cod;
            Reg2["ANIO"] = Session["Anio"];
            Reg2["LOGO"] = bytes;
            DS.INSTITUCIONES.Rows.Add(Reg2);

            foreach (var TD in CargaIni)
            {
                DataRow Reg = DS.CARGANI.NewRow();
                Reg["PROG_CODIGO"] = TD.PROG_CODIGO;
                Reg["PROG_NOMBRE"] = TD.PROG_NOMBRE;
                Reg["SUBP_CODIGO"] = TD.SUBP_CODIGO;
                Reg["SUBP_NOMBRE"] = TD.SUBP_NOMBRE;
                Reg["PROY_CODIGO"] = TD.PROY_CODIGO;
                Reg["PROY_NOMBRE"] = TD.PROY_NOMBRE;
                Reg["ACTI_CODIGO"] = TD.ACTI_CODIGO;
                Reg["ACTI_NOMBRE"] = TD.ACTI_NOMBRE;
                Reg["GRUP_CODIGO"] = TD.GRUP_CODIGO;
                Reg["GRUP_NOMBRE"] = TD.GRUP_NOMBRE;
                Reg["PAIN_CLAVE"] = TD.PAEG_CLAVE;
                Reg["PAIN_NOMBRE"] = TD.PAEG_NOMBRE;
                Reg["DETI_VALOR"] = TD.VALOR_TOTAL;
                //Reg["TIPO"] = "EGRESOS";
                DS.CARGANI.Rows.Add(Reg);
            }

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reportes/Presupuesto"), "RepCargaIniEgr.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            ReportDataSource rd = new ReportDataSource("DSetLogo", DS.Tables["INSTITUCIONES"]);
            ReportDataSource rd2 = new ReportDataSource("DSetDetPartidas", DS.Tables["CARGANI"]);
            lr.DataSources.Add(rd);
            lr.DataSources.Add(rd2);
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