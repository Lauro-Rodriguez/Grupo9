using BuscaPersona;
using Evaluator;
using SIGEFINew.Clases;
using SIGEFINew.Models;
using SIGEFINew.Models.Contabilidad;
using SIGEFINew.Models.Inventarios;
using SIGEFINew.Models.Nomina;
using SIGEFINew.Models.POA;
using SIGEFINew.Models.Presupuesto;
using SIGEFINew.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace SIGEFINew.Controllers
{
    [Authorize]
    public class FuncionesController : Controller
    {
        private Engine engine;
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Funciones
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LLenaPeriodos(int IdEmp)
        {
            var Periodo = db.PERIODOS.Where(p => p.ID_INSTITUCION == IdEmp).ToList();
            List<Periodos> Items = new List<Periodos>();
            foreach (var item in Periodo)
            {
                Items.Add(new Periodos()
                {
                    PERI_CODIGO = item.PERI_CODIGO,
                    PERI_DESCRIPCION = item.PERI_DESCRIPCION
                });
            }

            //Items.Add(new Periodos { PERI_CODIGO = 0, PROG_NOMBRE = "[Seleccione un Programa]" });
            return Json(Items.OrderBy(o => o.PERI_CODIGO), JsonRequestBehavior.AllowGet);
        }

        public JsonResult LLenaProgramas()
        {
            var programas = db.Programas.Include(p => p.Instituciones).ToList();
            List<Programa> Items = new List<Programa>();
            foreach (var item in programas)
            {
                Items.Add(new Programa()
                {
                    PROG_CODIGO = item.PROG_CODIGO,
                    PROG_NOMBRE = item.PROG_NOMBRE
                });
            }

            Items.Add(new Programa { PROG_CODIGO = -1, PROG_NOMBRE = "[Seleccione un Programa]" });
            return Json(Items.OrderBy(o => o.PROG_CODIGO), JsonRequestBehavior.AllowGet);
        }

        public JsonResult LLenaGrupos(string Tipo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            var grupos = (from t in db.Grupoes
                          where t.ID_INSTITUCION == IdInst
                          && t.TIPO == Tipo && t.NIVEL == 3
                          select t).ToList();
            List<Grupo> Items = new List<Grupo>();
            foreach (var item in grupos)
            {
                Items.Add(new Grupo()
                {
                    GRUP_CODIGO = item.GRUP_CODIGO,
                    GRUP_NOMBRE = item.GRUP_NOMBRE,
                    NIVEL = item.NIVEL
                });
            }

            Items.Add(new Grupo { GRUP_CODIGO = "", GRUP_NOMBRE = "[Seleccione un Grupo]" });
            return Json(Items.OrderBy(o => o.GRUP_CODIGO), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LLenaItems(string Grupo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            List<Item> Items = new List<Item>();
            var mProy = (from Item in db.Items
                         where Item.GRUP_CODIGO == Grupo
                         && Item.ID_INSTITUCION == IdInst
                         select new { Item.ITEM_CLAVE, ITEM_NOMBRE = Item.ITEM_CLAVE + " " + Item.ITEM_NOMBRE }).ToList();

            foreach (var item in mProy)
            {
                Items.Add(new Item()
                {
                    ITEM_CLAVE = item.ITEM_CLAVE,
                    ITEM_NOMBRE = item.ITEM_NOMBRE
                });
            }
            Items.Add(new Item { ITEM_CLAVE = "", ITEM_NOMBRE = "[Seleccione un Item]" });
            return Json(Items.OrderBy(o => o.ITEM_CLAVE), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrgCod(int TRECod)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);

            var OrgCod = (from Item in db.TipoRecursos
                          where Item.ID_INSTITUCION == IdInst && Item.PERI_CODIGO == Pericod
                          && Item.TRE_CODIGO == TRECod
                          select new { Cod = Item.ORG_CODIGO }).ToList();
            int Org = 0;

            foreach (var c in OrgCod)
            {
                Org = c.Cod;
            }

            return Json(Org);
        }

        public JsonResult FillPoliticaPDOT(string Id)
        {
            //int IdInst = Convert.ToInt32(Session["IdInst"]);
            List<POA_POL_PDOT> Items = new List<POA_POL_PDOT>();
            var mPolit = (from Item in db.POA_POL_PDOT
                          where Item.COD_OE_PDOT == Id
                          select Item).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new POA_POL_PDOT()
                {
                    COD_POL_PDOT = Polit.COD_POL_PDOT,
                    NOM_POL_PDOT = Polit.NOM_POL_PDOT
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillMetaPDOT(string Id, string Id2)
        {
            //int IdInst = Convert.ToInt32(Session["IdInst"]);
            List<POA_META_PDOT> Items = new List<POA_META_PDOT>();
            var mPolit = (from Item in db.POA_META_PDOT
                          where Item.COD_OE_PDOT == Id
                          && Item.COD_POL_PDOT == Id2
                          select Item).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new POA_META_PDOT()
                {
                    COD_META_PDOT = Polit.COD_META_PDOT,
                    NOM_POL_PDOT = Polit.NOM_POL_PDOT
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillEmpleaXDirec(int OrgCodigo)
        {
            List<RP_EMPLEADOS> Items = new List<RP_EMPLEADOS>();

            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);

            var query = from c in db.RP_EMPLEADOS
                        join a in db.RP_CARGOS
                        on c.ID_CARGO equals a.ID_CARGO
                        where c.ORG_CODIGO == OrgCodigo
                        && a.OTROS1 >= 4
                        select new
                        {
                            c.ID_EMPLEADO,
                            c.APELLIDOS,
                            c.NOMBRES,
                        };

            foreach (var Polit in query)
            {
                Items.Add(new RP_EMPLEADOS()
                {
                    ID_EMPLEADO = Polit.ID_EMPLEADO,
                    APELLIDOS = Polit.APELLIDOS + " " + Polit.NOMBRES
                });
            }

            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillEmpleados(int IdEmp)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            //int IdInst = Convert.ToInt32(Session["IdInst"]);
            List<RP_EMPLEADOS> Items = new List<RP_EMPLEADOS>();
            var mPolit = (from Item in db.RP_EMPLEADOS
                          where Item.ID_INSTITUCION == IdInst
                          && Item.ACTIVO == true
                          select Item).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new RP_EMPLEADOS()
                {
                    ID_EMPLEADO = Polit.ID_EMPLEADO,
                    APELLIDOS = Polit.APELLIDOSNOMBRES
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillCatalogos(int TipRec)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            List<Catalogos> Items = new List<Catalogos>();
            var mPolit = (from Item in db.Catalogos
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod
                          && Item.TRE_CODIGO == TipRec
                          && Item.TIPO == "D"
                          select Item).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new Catalogos()
                {
                    CAT_CODIGO = Polit.CAT_CODIGO,
                    CAT_NOMBRE = Polit.CAT_NOMBRE
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Totales()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            var Total = (from x in db.DETALLEINGRESOS where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod select x.DETI_VALOR);
            decimal Suma = 0;
            try
            {
                Suma = Total.Sum();
            }
            catch { }
            string totc = Suma.ToString("n2");
            return Json(totc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TotalTechos()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var Total2 = (from x in db.TECHOSPRESUPs where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod select x.VALOR_CANTIDAD);
            decimal Difer = Total2.Sum();
            string totc = Difer.ToString("n2");
            return Json(totc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Diferencia()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var Total = (from x in db.DETALLEINGRESOS where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod select x.DETI_VALOR);
            var Total2 = (from x in db.TECHOSPRESUPs where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod select x.VALOR_CANTIDAD);
            decimal Difer = Total.Sum() - Total2.Sum();
            string totc = Difer.ToString("n2");
            return Json(totc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TotalPorc()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            var Total2 = (from x in db.TECHOSPRESUPs where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod select x.VALOR_PORCENTAJE);
            string totc = Total2.Sum().ToString("n2");
            return Json(totc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TotalEgre()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            decimal Difer = 0;
            var Total = (from x in db.DETALLEGRESOS where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod select x.VALOR_TOTAL);

            try
            {
                Difer = Total.Sum();
            }
            catch
            {
            }

            string totc = Difer.ToString("n2");
            return Json(totc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DiferEgresos()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            decimal TotalEgre = 0;
            decimal TotalIng = 0;
            var Total = (from x in db.DETALLEINGRESOS where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod select x.DETI_VALOR);
            var Total2 = (from x in db.DETALLEGRESOS where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod select x.VALOR_TOTAL);
            try
            {
                TotalIng = Total.Sum();
                TotalEgre = Total2.Sum();
            }
            catch { }
            decimal Difer = TotalIng - TotalEgre;
            string totc = Difer.ToString("n2");
            return Json(totc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TotalEgreXActiv(string ActiCod)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var Total = (from x in db.DETALLEGRESOS
                         where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod
&& x.ACTI_CODIGO == ActiCod
                         select x.VALOR_TOTAL);

            decimal Difer = Total.Sum();
            string totc = Difer.ToString("n2");
            return Json(totc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidoDoc(int TipoDoc, string Cedula)
        {
            string TDoc = "";
            switch (TipoDoc)
            {
                case 0:
                    TDoc = "R";
                    break;
                case 1:
                    TDoc = "C";
                    break;
                case 2:
                    TDoc = "P";
                    break;
                case 3:
                    TDoc = "T";
                    break;
            }

            ValidaDoc.ValidaDoc mValida = new ValidaDoc.ValidaDoc();

            if (mValida.validaIdentificacion(TDoc, Cedula))
            {
                return Json(true);
            }
            return Json(false);
        }

        public JsonResult MaskCuenPat(int Nivel)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var Mask = (from x in db.CO_PARAMETROS
                        where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod
                        select new { Cod = x.MASKCUENPATRI }).ToList();

            string mMask = "";

            foreach (var c in Mask)
            {
                mMask = c.Cod;
            }

            int nc = mMask.Length - 1;
            int n = (from c in mMask where c == '.' select c).Count() + 1;

            string NuevCod;
            string xx;
            int NumDot;
            NuevCod = "";
            NumDot = 0;

            for (int i = 0; i <= nc; i++)
            {
                xx = mMask.Substring(i, 1);
                NuevCod = NuevCod + mMask.Substring(i, 1);
                if (xx == ".")
                {
                    NumDot = NumDot + 1;
                    if (NumDot == Nivel)
                    {
                        break;
                    }
                }
                else
                {
                    //NuevCod = NuevCod + mMask.Substring(i, 1);
                    //if (NumDot == Nivel)
                    //{
                    //    break;
                    //}
                    //NumDot = 0;
                }

            }

            return Json(NuevCod, JsonRequestBehavior.AllowGet);
        }

        private string NCod(string Text)
        {
            string NuevCod;
            string xx;
            int i;
            int NumDot;
            int Num = 0;
            NuevCod = "";
            NumDot = 0;
            //MessageBox.Show(Text);
            try
            {
                for (i = 0; i <= Text.Length - 1; i++)
                {
                    xx = Text.Substring(i, 1);
                    if (xx == ".")
                    {
                        NumDot = NumDot + 1;
                    }
                    else
                    {
                        NumDot = 0;
                    }

                    if (NumDot == 0 || NumDot == 1)
                    {
                        NuevCod = NuevCod + Text.Substring(i, 1);
                        if (Text.Substring(i, 1) == "." || i == Text.Length - 1)
                        {
                            Num = Num + 1;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //MessageBox.Show(NuevCod);
                return NuevCod;
            }

            catch
            {
                return "";
            }
        }

        public JsonResult TotRecursosProy(string CodRecurso)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            int Direc = Convert.ToInt32(Session["OrgCodigo"]);

            var allFondos = db.POA_OBJTIVORECURSOS.Include(c => c.Catalogos).Where(d => d.ID_INSTITUCION == IdInst
           && d.PERI_CODIGO == PeriCod && d.COD_PLANOBJTVO == CodRecurso && d.ORG_CODIGO == Direc);

            decimal Difer = allFondos.Sum(d => d.VAL_ENERO + d.VAL_FEBRERO + d.VAL_MARZO + d.VAL_ABRIL +
            d.VAL_MAYO + d.VAL_JUNIO + d.VAL_JULIO + d.VAL_AGOSTO + d.VAL_SEPTIEMBRE +
            d.VAL_OCTUBRE + d.VAL_NOVIEMBRE + d.VAL_DICIEMBRE);

            //string totc = Difer.ToString("n2");

            return Json(Difer, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult FillRecursosProy(string CodRecurso)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            int Direc = Convert.ToInt32(Session["OrgCodigo"]);


            var allFondos = db.POA_OBJTIVORECURSOS.Include(c => c.Catalogos).Where(d => d.ID_INSTITUCION == IdInst
           && d.PERI_CODIGO == PeriCod && d.COD_PLANOBJTVO == CodRecurso && d.ORG_CODIGO == Direc);

            var viewModel = new List<AsignarDetDispPresup>();

            int i = 0;
            foreach (var course in allFondos)
            {
                viewModel.Add(new AsignarDetDispPresup
                {
                    ID_INSTITUCION = IdInst,
                    PERI_CODIGO = PeriCod,
                    CODIGO_DOC = 100,
                    ORG_CODIGO = Direc,
                    NUM_SOLIC = 0,
                    CAT_CODIGO = course.Catalogos.CAT_CODIGO,
                    DESCRIPCION = course.Catalogos.CAT_NOMBRE,
                    VALOR_REC = course.TOTAL
                });

            }
            ViewBag.Resursos = viewModel;
            return PartialView("_Cuenta", viewModel);
        }

        public JsonResult FillCuentasAux()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            List<CO_CUENTASCONT> Items = new List<CO_CUENTASCONT>();
            var mPolit = (from Item in db.CO_CUENTASCONT
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod && Item.TIPO_CG == "A"
                          && Item.ID_INSTITUCION == IdInst && Item.PERI_CODIGO == PeriCod
                          select Item).OrderBy(O => O.CODIGO_CG).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new CO_CUENTASCONT()
                {
                    CODIGO_CG = Polit.CODIGO_CG,
                    NOMBRE_CG = Polit.NOMBRE_CG
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public PartialViewResult GuardarCta(string CodRecurso, int? a)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var allFondos = db.CO_RELARETCONT.Include(c => c.CO_CUENTASCONT).Where(d => d.ID_INSTITUCION == IdInst
           && d.PERI_CODIGO == PeriCod && d.CODIGO_RET == CodRecurso).OrderBy(O => O.CODIGO_CG).ToList();

            var viewModel = new List<CO_RELARETCONT>();

            foreach (var course in allFondos)
            {
                viewModel.Add(new CO_RELARETCONT
                {
                    ID_INSTITUCION = IdInst,
                    PERI_CODIGO = PeriCod,
                    CODIGO_RET = course.CODIGO_RET,
                    CODIGO_CG = course.CODIGO_CG
                });

            }

            return PartialView("_CuentasCont");
        }

        public JsonResult FillDirecciones()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            List<OrganicoF> Items = new List<OrganicoF>();
            var mPolit = (from Item in db.OrganicoFs
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod
                          select Item).OrderBy(O => O.ORG_NOMBRE).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new OrganicoF()
                {
                    ORG_CODIGO = Polit.ORG_CODIGO,
                    ORG_NOMBRE = Polit.ORG_NOMBRE
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillProveedores(int Tipo, int Direc)
        {
            List<Proveedores> Items = new List<Proveedores>();
            var mPolit = (from Item in db.Proveedores
                          join e in db.RP_EMPLEADOS on Item.NUMCEDRUC_PROV equals e.NUM_CEDULA
                          where e.ORG_CODIGO == Direc
                          select Item).OrderBy(O => O.NOMBRE).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new Proveedores()
                {
                    COD_PROV = Polit.COD_PROV,
                    NOMBRE = Polit.NOMBRE
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillAllProveedores()
        {
            List<Proveedores> Items = new List<Proveedores>();
            var mPolit = (from Item in db.Proveedores
                          select Item).OrderBy(O => O.NOMBRE).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new Proveedores()
                {
                    COD_PROV = Polit.COD_PROV,
                    NOMBRE = Polit.NOMBRE
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillEnpleado(int IdEmple)
        {
            List<Proveedores> Items = new List<Proveedores>();
            var mPolit = (from Item in db.Proveedores
                          join e in db.RP_EMPLEADOS on Item.NUMCEDRUC_PROV equals e.NUM_CEDULA
                          where e.ID_EMPLEADO == IdEmple
                          select Item).OrderBy(O => O.NOMBRE).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new Proveedores()
                {
                    COD_PROV = Polit.COD_PROV,
                    NOMBRE = Polit.NOMBRE
                });
            }

            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillPartidasEgre()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            List<PR_PARTEGRESOS> Items = new List<PR_PARTEGRESOS>();
            var mPolit = (from Item in db.PR_PARTEGRESOS
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod
                          select Item).OrderBy(O => O.PAEG_CLAVE).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new PR_PARTEGRESOS()
                {
                    PAEG_CODIGO = Polit.PAEG_CODIGO,
                    PAEG_CLAVE = Polit.PAEG_CLAVE,
                    PAEG_NOMBRE = Polit.PAEG_NOMBRE
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillPartidasEgreInd(int Clave)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            List<PR_PARTEGRESOS> Items = new List<PR_PARTEGRESOS>();
            var mPolit = (from Item in db.PR_PARTEGRESOS
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod
                          && Item.PAEG_CODIGO == Clave
                          select Item).OrderBy(O => O.PAEG_CLAVE).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new PR_PARTEGRESOS()
                {
                    PAEG_CODIGO = Polit.PAEG_CODIGO,
                    PAEG_CLAVE = Polit.PAEG_CLAVE,
                    PAEG_NOMBRE = Polit.PAEG_NOMBRE
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillPartidasEgreCert(int NumTran, int NumCert)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            List<AsignarPartidas> Items = new List<AsignarPartidas>();
            var mPolit = (from Item in db.PR_DETCERTDISP
                          join e in db.PR_PARTEGRESOS on Item.PAEG_CODIGO equals e.PAEG_CODIGO
                          join f in db.PR_FONDOS on Item.ID_FONDO equals f.ID_FONDO
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod
                          && Item.NUM_TRANSAC == NumTran
                          && Item.NUM_CERTIF == NumCert
                          select new
                          {
                              Item.PAEG_CODIGO,
                              e.PAEG_CLAVE,
                              e.PAEG_NOMBRE,
                              Item.VALOR,
                              Item.ID_FONDO,
                              f.DESCRIPCION
                          }).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new AsignarPartidas()
                {
                    PAEG_CODIGO = Polit.PAEG_CODIGO,
                    PAEG_CLAVE = Polit.PAEG_CLAVE,
                    NOM_PARTIDA = Polit.PAEG_NOMBRE,
                    VALOR = Polit.VALOR,
                    ID_FONDO = Polit.ID_FONDO,
                    NOM_FONDO = Polit.DESCRIPCION
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillDocsCert(int NumTran, int NumCert)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            List<PR_DOCUMCERTDISP> Items = new List<PR_DOCUMCERTDISP>();
            var mPolit = (from Item in db.PR_DOCUMCERTDISP
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod
                          && Item.NUM_TRANSAC == NumTran
                          && Item.NUM_CERTIF == NumCert
                          select Item).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new PR_DOCUMCERTDISP()
                {
                    COD_DOCUM = Polit.COD_DOCUM,
                    NUM_DOCUM = Polit.NUM_DOCUM,
                    FECHA = Polit.FECHA,
                    VALOR = Polit.VALOR,
                    OBSERVA_DOCUM = Polit.OBSERVA_DOCUM
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSaldoPartida(int PaegCodigo)
        {
            decimal CInic = 0;
            decimal Refor = 0;
            decimal Valor = 0;

            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            int Direc = Convert.ToInt32(Session["OrgCodigo"]);

            var CIni =
               from CI in db.PR_DETGASTO
               where CI.ID_INSTITUCION == IdInst &&
               CI.PERI_CODIGO == PeriCod &&
               CI.PAEG_CODIGO == PaegCodigo &&
               CI.TIPO_TRAN == "CI"
               select new
               {
                   CINI = CI.VALOR
               };

            var TotCertif =
               from CI in db.PR_DETCERTDISP
               where CI.ID_INSTITUCION == IdInst &&
               CI.PERI_CODIGO == PeriCod &&
               CI.PAEG_CODIGO == PaegCodigo
               select new
               {
                   VALOR = CI.VALOR
               };

            var Reforma =
                from CI in db.PR_DETGASTO
                join MG in db.PR_MOVGASTO on CI.NUM_TRANSAC equals MG.NUM_TRANSAC
                where CI.ID_INSTITUCION == IdInst &&
                CI.PERI_CODIGO == PeriCod &&
                CI.PAEG_CODIGO == PaegCodigo
                select new
                {
                    REFOR = (MG.TIPO_TRANSAC == "RE" ? CI.VALOR : 0),
                    VALOR = (MG.TIPO_TRANSAC == "TG" ? CI.VALOR : 0)
                };

            try
            {
                CInic = CIni.Sum(ci => ci.CINI);
                Refor = Reforma.Sum(re => re.REFOR);
                Valor = Reforma.Sum(re => re.VALOR) + TotCertif.Sum(ce => ce.VALOR);
            }
            catch
            {

            }


            decimal Saldo = (CInic + Refor) - Valor;

            return Json(Saldo, JsonRequestBehavior.AllowGet);
        }

        #region Firmas
        public JsonResult FillFirmas(int Codigo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            int Direc = Convert.ToInt32(Session["OrgCodigo"]);
            string User = Session["Usuario"].ToString();

            var allFondos = db.FIRMAS.Where(d => d.ID_INSTITUCION == IdInst
            && d.PERI_CODIGO == PeriCod && d.CODIGO_DOC == Codigo && d.USUA_FIRMA == User).OrderBy(o => o.NUM_FILA);

            var viewModel = new List<FIRMAS>();

            foreach (var course in allFondos)
            {
                viewModel.Add(new FIRMAS
                {
                    ID_INSTITUCION = IdInst,
                    PERI_CODIGO = PeriCod,
                    USUA_FIRMA = course.USUA_FIRMA,
                    CODIGO_DOC = course.CODIGO_DOC,
                    NUM_FILA = course.NUM_FILA,
                    NOMBRE = course.NOMBRE,
                    CARGO = course.CARGO,
                    TIPO = course.TIPO
                });

            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        //Guardar Firmas
        public ActionResult Save(int Codigo, string Tipo, FIRMAS[] order)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            string User = Session["Usuario"].ToString();
            int NumFila = 1;

            try
            {
                var allFondos = db.FIRMAS.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod &&
           f.CODIGO_DOC == Codigo && f.USUA_FIRMA == User)
               .First(x => x.NUM_FILA == db.FIRMAS.Max(y => y.NUM_FILA));

                //NumFila = allFondos.NUM_FILA + 1;
            }
            catch
            {
                //NumFila = 1;
            }


            if (Session["Usuario"].ToString() != "")
            {
                string result = "Error! Order Is Not Complete!";
                //if (name != null && address != null && order != null)
                //{

                // Query the database for the rows to be deleted.
                var Reg = db.FIRMAS.Where(d => d.ID_INSTITUCION == IdInst
           && d.PERI_CODIGO == PeriCod && d.CODIGO_DOC == Codigo && d.USUA_FIRMA == User);
                db.FIRMAS.RemoveRange(Reg);

                //db.SaveChanges();

                foreach (var item in order)
                {
                    FIRMAS O = new FIRMAS();
                    O.ID_INSTITUCION = IdInst;
                    O.PERI_CODIGO = PeriCod;
                    O.USUA_FIRMA = User;
                    O.CODIGO_DOC = Codigo;
                    O.NUM_FILA = NumFila;
                    O.NOMBRE = item.NOMBRE;
                    O.CARGO = item.CARGO;
                    O.TIPO = Tipo;
                    db.FIRMAS.Add(O);
                    NumFila++;
                }
                db.SaveChanges();
                result = "Los Datos se Guardaron Satisfatoriamente!";
                //}
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }
        #endregion

        public JsonResult FillPartidasCompromiso(int NumTran, int NumCert)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            List<AsignarPartidas> Items = new List<AsignarPartidas>();
            var mPolit = (from Item in db.PR_DETGASTO
                          join e in db.PR_PARTEGRESOS on Item.PAEG_CODIGO equals e.PAEG_CODIGO
                          join f in db.PR_FONDOS on Item.ID_IFONDO equals f.ID_FONDO
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod
                          && Item.NUM_TRANSAC == NumTran
                          //&& Item.NUM_CERTIF == NumCert
                          select new
                          {
                              Item.PAEG_CODIGO,
                              e.PAEG_CLAVE,
                              e.PAEG_NOMBRE,
                              Item.VALOR,
                              Item.ID_IFONDO,
                              f.DESCRIPCION
                          }).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new AsignarPartidas()
                {
                    PAEG_CODIGO = Polit.PAEG_CODIGO,
                    PAEG_CLAVE = Polit.PAEG_CLAVE,
                    NOM_PARTIDA = Polit.PAEG_NOMBRE,
                    VALOR = Polit.VALOR,
                    ID_FONDO = Polit.ID_IFONDO,
                    NOM_FONDO = Polit.DESCRIPCION
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillCatalogo()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            List<Catalogos> Items = new List<Catalogos>();
            var mPolit = (from Item in db.Catalogos
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod && Item.TIPO == "D"
                          select Item).OrderBy(O => O.CAT_NOMBRE).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new Catalogos()
                {
                    CAT_CODIGO = Polit.CAT_CODIGO,
                    CAT_NOMBRE = Polit.CAT_NOMBRE
                });
            }
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPorcIVA()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var Mask = (from x in db.CO_PARAMETROS
                        where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod
                        select new { Cod = x.PORIVA }).FirstOrDefault();

            return Json(Mask.Cod, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillCatalogoInd(string Clave)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            string Catalogo = "";

            var mPolit = (from Item in db.Catalogos
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod
                          && Item.CAT_CODIGO == Clave
                          select new { Item.CAT_NOMBRE }).FirstOrDefault();
            try
            {
                Catalogo = mPolit.CAT_NOMBRE;
            }
            catch
            {

            }

            return Json(Catalogo, JsonRequestBehavior.AllowGet);
        }

        #region UsuariosxBodega
        public JsonResult FillUsersxBod()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            int Direc = Convert.ToInt32(Session["OrgCodigo"]);
            string User = Session["Usuario"].ToString();

            var query = (from c in db.IN_USERSXBOD
                         join a in db.Users
                         on c.USUA_LOGIN equals a.UserName
                         join d in db.IN_BODEGAS
                         on c.ID_BODEGA equals d.ID_BODEGA
                         where c.ID_INSTITUCION == IdInst
                         && c.PERI_CODIGO == PeriCod
                         select new
                         {
                             c.ID_BODEGA,
                             d.NOM_BODEGA,
                             c.USUA_LOGIN,
                             a.FullName
                         }).ToList();

            //var allFondos = db.IN_USERSXBOD.Where(d => d.ID_INSTITUCION == IdInst
            //&& d.PERI_CODIGO == PeriCod );

            //var viewModel = new List<IN_USERSXBOD>();

            //foreach (var course in allFondos)
            //{
            //    viewModel.Add(new IN_USERSXBOD
            //    {
            //        ID_INSTITUCION = IdInst,
            //        PERI_CODIGO = PeriCod,
            //        USUA_LOGIN = course.USUA_LOGIN
            //    });

            //}

            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveUserxBod(IN_USERSXBOD[] order)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            if (Session["Usuario"].ToString() != "")
            {
                string result = "Error! Order Is Not Complete!";

                var Reg = db.IN_USERSXBOD.Where(d => d.ID_INSTITUCION == IdInst
           && d.PERI_CODIGO == PeriCod);
                db.IN_USERSXBOD.RemoveRange(Reg);

                //db.SaveChanges();
                if (order != null)
                {
                    foreach (var item in order)
                    {
                        IN_USERSXBOD O = new IN_USERSXBOD();
                        O.ID_INSTITUCION = IdInst;
                        O.PERI_CODIGO = PeriCod;
                        O.ID_BODEGA = item.ID_BODEGA;
                        O.USUA_LOGIN = item.USUA_LOGIN;
                        db.IN_USERSXBOD.Add(O);
                    }
                }
                db.SaveChanges();
                result = "Los Datos se Guardaron Satisfatoriamente!";

                //}
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }
        #endregion
        public JsonResult FillBodegas()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var iN_BODEGAS = db.IN_BODEGAS.Where(f => f.ID_INSTITUCION == IdInst
             && f.PERI_CODIGO == PeriCod).ToList();

            List<IN_BODEGAS> Items = new List<IN_BODEGAS>();

            foreach (var Polit in iN_BODEGAS)
            {
                Items.Add(new IN_BODEGAS()
                {
                    ID_BODEGA = Polit.ID_BODEGA,
                    NOM_BODEGA = Polit.NOM_BODEGA
                });
            }

            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillBodegasInd(int IdBodega)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var iN_BODEGAS = db.IN_BODEGAS.Where(f => f.ID_INSTITUCION == IdInst
             && f.PERI_CODIGO == PeriCod && f.ID_BODEGA == IdBodega).FirstOrDefault();

            List<IN_BODEGAS> Items = new List<IN_BODEGAS>();

            return Json(iN_BODEGAS.COD_AUTOMAT, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillUsuarios()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            int Direc = Convert.ToInt32(Session["OrgCodigo"]);

            var query = (from c in db.Users
                         join a in db.RP_EMPLEADOS
                         on c.ID_EMPLEADO equals a.ID_EMPLEADO
                         where a.ID_INSTITUCION == IdInst
                         select new
                         {
                             c.UserName,
                             c.FullName
                         }).ToList();

            List<AsignaUsers> Items = new List<AsignaUsers>();

            foreach (var Polit in query)
            {
                Items.Add(new AsignaUsers()
                {
                    USER_NAME = Polit.UserName,
                    USER_NOMBRE = Polit.FullName
                });
            }

            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillTipos(int IdClase)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var iN_TIPOS = db.IN_TIPOS.Where(f => f.ID_INSTITUCION == IdInst
             && f.PERI_CODIGO == PeriCod && f.ID_CLASE == IdClase).ToList();

            List<IN_TIPOS> Items = new List<IN_TIPOS>();

            foreach (var Polit in iN_TIPOS)
            {
                Items.Add(new IN_TIPOS()
                {
                    ID_TIPO = Polit.ID_TIPO,
                    NOM_TIPO = Polit.NOM_TIPO
                });
            }
            Items.Add(new IN_TIPOS { ID_TIPO = 0, NOM_TIPO = "[Seleccione una Familia]" });
            return Json(Items.OrderBy(o => o.NOM_TIPO), JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillSubTipos(int IdClase, int IdTipo)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var iN_SUBTIPOS = db.IN_SUBTIPOS.Where(f => f.ID_INSTITUCION == IdInst
             && f.PERI_CODIGO == PeriCod && f.ID_CLASE == IdClase && f.ID_TIPO == IdTipo).ToList();

            List<IN_SUBTIPOS> Items = new List<IN_SUBTIPOS>();

            foreach (var Polit in iN_SUBTIPOS)
            {
                Items.Add(new IN_SUBTIPOS()
                {
                    ID_SUBTIPO = Polit.ID_SUBTIPO,
                    NOM_SUBTIPO = Polit.NOM_SUBTIPO
                });
            }
            Items.Add(new IN_SUBTIPOS { ID_SUBTIPO = 0, NOM_SUBTIPO = "[Seleccione una Subfamilia]" });
            return Json(Items.OrderBy(o => o.NOM_SUBTIPO), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            string path = Server.MapPath("~/Documentos/PDF/POA/");
            HttpFileCollectionBase files = Request.Files;
            string FPath = "";
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                file.SaveAs(path + file.FileName);
                FPath = path + file.FileName;
            }


            return Json(FPath);
        }

        public JsonResult FillTiposdeRol()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);

            var rP_TIPOROL = db.RP_TIPOROL.Where(f => f.ID_INSTITUCION == IdInst);

            List<RP_TIPOROL> Items = new List<RP_TIPOROL>();

            foreach (var Polit in rP_TIPOROL)
            {
                Items.Add(new RP_TIPOROL()
                {
                    ID_TIPOROL = Polit.ID_TIPOROL,
                    DESCRIPCION = Polit.DESCRIPCION
                });
            }

            return Json(Items.OrderBy(o => o.ID_TIPOROL), JsonRequestBehavior.AllowGet);
        }

        public JsonResult OkRol(int Rol)
        {
            Session["IdTipoRol"] = Rol;
            return Json(Rol, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidaFormula(string Code)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            var mVar = db.RP_VARIABLES.Where(f => f.ID_INSTITUCION == IdInst).ToList();
            var Rubros = db.RP_RUBROS.Where(f => f.ID_INSTITUCION == IdInst && f.TIPO <= 4).ToList();

            engine = new Engine();
            string HeadTxt = "";
            string result = "";
            HeadTxt = "from datetime import date" + "\r \n";
            HeadTxt = HeadTxt + "from datetime import datetime" + "\r \n";

            foreach (var Polit in Rubros)
            {
                HeadTxt = HeadTxt + Polit.COD_RUBRO + "=" + 0 + "\r \n";
            }

            foreach (var Polit in mVar)
            {
                switch (Polit.COD_VAR.Trim())
                {
                    case "Fecha1":
                        HeadTxt = HeadTxt + "Fecha1=date.today()" + "\r \n";
                        break;
                    case "Fecha2":
                        HeadTxt = HeadTxt + "Fecha2=date.today()" + "\r \n";
                        break;
                    case "Fecha3":
                        HeadTxt = HeadTxt + "Fecha3=date.today()" + "\r \n";
                        break;
                    case "FIngreso":
                        HeadTxt = HeadTxt + "FIngreso=date.today()" + "\r \n";
                        break;
                    case "FNacim":
                        HeadTxt = HeadTxt + "FNacim=date.today()" + "\r \n";
                        break;
                    case "FReingreso":
                        HeadTxt = HeadTxt + "FReingreso=date.today()" + "\r \n";
                        break;
                    case "FSalida":
                        HeadTxt = HeadTxt + "FSalida=date.today()" + "\r \n";
                        break;
                    default:
                        HeadTxt = HeadTxt + Polit.COD_VAR + "=" + 0 + "\r \n";
                        break;
                }

            }


            //engine.set_button(button);
            //HeadTxt = "Sueldo=" + 1400 + "\r \n";
            //HeadTxt = HeadTxt + "Salario=2000" + "\r \n";

            result = engine.evaluate("", HeadTxt + Code);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscaPersona(string Codigo, string Cedula)
        {
            BuscarCedula bp = new BuscarCedula();
            XmlDocument xmlDoc = new XmlDocument();
            string Nombre;
            try
            {
                var nom = bp.InvokeService(Codigo, Cedula);
                xmlDoc.LoadXml("<root>" + nom + "</root>");
                XmlNodeList address = xmlDoc.GetElementsByTagName("valor");
                Nombre = address[1].InnerXml;
            }
            catch
            {
                Nombre = "";
            }

            return Json(Nombre, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDatoSeguro()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);

            var DatoSeg = (from x in db.INSTITUCIONES
                           where x.ID_INSTITUCION == IdInst
                           select new { Cod = x.DATO_SEGURO }).FirstOrDefault();

            return Json(DatoSeg.Cod, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SavePNBV(POA_PNBV[] order)
        {
            if (Session["Usuario"].ToString() != "")
            {
                string result = "Error! Order Is Not Complete!";
                //if (name != null && address != null && order != null)
                //{

                // Query the database for the rows to be deleted.
                var Reg = db.POA_PNBV;
                //db.POA_PNBV.RemoveRange(Reg);

                //db.SaveChanges();

                foreach (var item in order)
                {
                    POA_PNBV O = new POA_PNBV();
                    if (item.COD_PNVB == 0)
                    {
                        O.COD_PNVB = 0;
                        O.NOM_PNVB = item.NOM_PNVB;
                        db.POA_PNBV.Add(O);
                    }
                }
                db.SaveChanges();
                result = "Los Datos se Guardaron Satisfatoriamente!";
                //}
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Create");
        }

        public JsonResult AprobPres()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            bool Aprobado = false;
            var Aprob = db.PERIODOS.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod).FirstOrDefault();
            if (Aprob.FECHA_APROB != null)
            {
                Aprobado = true;
            }

            return Json(Aprobado, JsonRequestBehavior.AllowGet);
        }

        //Guardar presupuesto aprobado
        public JsonResult SavePresup(DateTime Fecha, string NDoc)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            bool Guardo = false;

            Periodos periodo = db.PERIODOS.Find(IdInst, PeriCod);
            periodo.FECHA_APROB = Fecha;
            periodo.NUMDOC_APROB = NDoc;

            db.Entry(periodo).State = EntityState.Modified;

            try
            {
                Guardo = true;
                db.SaveChanges();
            }
            catch
            {
                //NumFila = 1;
            }
            return Json(Guardo, JsonRequestBehavior.AllowGet);
        }

        //Obtener saldo entre Ingresos y Egresos
        public JsonResult SaldoPresup()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            decimal TotalEgre = 0;
            decimal TotalIng = 0;
            var Total = (from x in db.DETALLEINGRESOS where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod select x.DETI_VALOR);
            var Total2 = (from x in db.DETALLEGRESOS where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod select x.VALOR_TOTAL);
            try
            {
                TotalIng = Total.Sum();
                TotalEgre = Total2.Sum();
            }
            catch { }
            decimal Difer = TotalIng - TotalEgre;
            return Json(Difer, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillPNBV()
        {
            var allFondos = db.POA_PNBV;

            var viewModel = new List<POA_PNBV>();

            foreach (var course in allFondos)
            {
                viewModel.Add(new POA_PNBV
                {
                    COD_PNVB = course.COD_PNVB,
                    NOM_PNVB = course.NOM_PNVB
                });

            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillOEI()
        {
            var allFondos = db.POA_OEI;

            var viewModel = new List<POA_OEI>();

            foreach (var course in allFondos)
            {
                viewModel.Add(new POA_OEI
                {
                    COD_OEI = course.COD_OEI,
                    NOM_OEI = course.NOM_OEI
                });

            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveOEI(POA_OEI[] order)
        {
            if (Session["Usuario"].ToString() != "")
            {
                string result = "Error! Order Is Not Complete!";
                //if (name != null && address != null && order != null)
                //{

                // Query the database for the rows to be deleted.
                var Reg = db.POA_OEI;
                //db.POA_PNBV.RemoveRange(Reg);

                //db.SaveChanges();

                foreach (var item in order)
                {
                    POA_OEI O = new POA_OEI();
                    if (item.COD_OEI == 0)
                    {
                        O.COD_OEI = 0;
                        O.NOM_OEI = item.NOM_OEI;
                        db.POA_OEI.Add(O);
                    }
                }
                db.SaveChanges();
                result = "Los Datos se Guardaron Satisfatoriamente!";
                //}
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Create");
        }

        public JsonResult FillOE_PDOT()
        {
            var allFondos = db.POA_OE_PDOT;

            var viewModel = new List<POA_OE_PDOT>();

            foreach (var course in allFondos)
            {
                viewModel.Add(new POA_OE_PDOT
                {
                    COD_OE_PDOT = course.COD_OE_PDOT,
                    NOM_OE_PDOT = course.NOM_OE_PDOT,
                    ACTIVO = course.ACTIVO,
                    COD_ED_PDOT = course.COD_ED_PDOT
                });

            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveOE_PDOT(POA_OE_PDOT[] order)
        {
            if (Session["Usuario"].ToString() != "")
            {
                string result = "Error! Order Is Not Complete!";
                //if (name != null && address != null && order != null)
                //{

                // Query the database for the rows to be deleted.
                var Reg = db.POA_OE_PDOT;
                //db.POA_PNBV.RemoveRange(Reg);

                //db.SaveChanges();

                foreach (var item in order)
                {
                    POA_OE_PDOT O = new POA_OE_PDOT();
                    if (item.COD_OE_PDOT == "0")
                    {
                        O.COD_OE_PDOT = "0";
                        O.NOM_OE_PDOT = item.NOM_OE_PDOT;
                        db.POA_OE_PDOT.Add(O);
                    }
                }
                db.SaveChanges();
                result = "Los Datos se Guardaron Satisfatoriamente!";
                //}
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Create");
        }

        public JsonResult FillSolicDisp(int NumSol)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            string[] Valores = new string[4];
            //List<PR_DETALSOLICDISPPRES> Items = new List<PR_DETALSOLICDISPPRES>();
            var mPolit = (from Item in db.PR_SOLICDISPPRESUP
                          join e in db.OrganicoFs on Item.ORG_CODIGO equals e.ORG_CODIGO
                          where Item.ID_INSTITUCION == IdInst && Item.PERI_CODIGO == PeriCod
                          && Item.NUM_SOLIC == NumSol
                          select new { e.ORG_CODIGO, Item.ID_EMPLEADO, Item.DETALLE, Item.VALOR }).FirstOrDefault();

            Valores[0] = mPolit.ORG_CODIGO.ToString();
            Valores[1] = mPolit.ID_EMPLEADO.ToString();
            Valores[2] = mPolit.DETALLE;
            Valores[3] = mPolit.VALOR.ToString();
            return Json(Valores, JsonRequestBehavior.AllowGet);
        }

        //Obtener las partidas presupuestaqrias de gasto por proyexto
        public JsonResult FillPartidasXProy(int NumSol)
        {
            //            SELECT T1."COD_PLANOBJTVO", T4."PAEG_CODIGO",T4."PAEG_CLAVE",T4."PAEG_NOMBRE",SUM(T2."VAL_ENERO" + T2."VAL_FEBRERO") AS TOTAL
            //FROM dbo."POA_PLANOBJETIVOS" T1,
            //dbo."POA_OBJTIVORECURSOS" T2,
            //dbo."Catalogos" T3,
            //dbo."PR_PARTEGRESOS" T4,
            //dbo."OrganicoFs" T5,
            //dbo."PR_SOLICDISPPRESUP" T6
            //WHERE T1."COD_PLANOBJTVO" = T2."COD_PLANOBJTVO"
            //AND T3."CAT_CODIGO" = T2."CAT_CODIGO"
            //AND T4."ITEM_CLAVE" = T3."ITEM_CLAVE"
            //AND T1."ORG_CODIGO" = T5."ORG_CODIGO"
            //AND T4."ACTI_CODIGO" = T5."ACTI_CODIGO"
            //AND T1."COD_PLANOBJTVO" = T6."COD_PLANOBJTVO"

            //AND T6."NUM_SOLIC" = 3 AND T1."ORG_CODIGO" = 5
            //GROUP BY T1."COD_PLANOBJTVO", T4."PAEG_CODIGO",T4."PAEG_CLAVE",T4."PAEG_NOMBRE"

            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            int Direc = Convert.ToInt32(Session["OrgCodigo"]);

            var viewModel = new List<AsignarPartidas>();

            var Query = (from T1 in db.POA_PLANOBJETIVOS
                         join T2 in db.POA_OBJTIVORECURSOS on T1.COD_PLANOBJTVO equals T2.COD_PLANOBJTVO
                         join T3 in db.Catalogos on T2.CAT_CODIGO equals T3.CAT_CODIGO
                         join T4 in db.PR_PARTEGRESOS on T3.ITEM_CLAVE equals T4.ITEM_CLAVE
                         join T5 in db.OrganicoFs on T1.ORG_CODIGO equals T5.ORG_CODIGO
                         join T6 in db.PR_SOLICDISPPRESUP on T1.COD_PLANOBJTVO equals T6.COD_PLANOBJTVO
                         where T1.ID_INSTITUCION == IdInst && T1.PERI_CODIGO == PeriCod && T6.NUM_SOLIC == NumSol
                                      && T1.ORG_CODIGO == Direc && T4.ACTI_CODIGO == T5.ACTI_CODIGO
                         group new
                         {
                             T4.PAEG_CODIGO,
                             T4.ITEM_CLAVE,
                             T4.PAEG_NOMBRE,
                             TOTAL = (decimal?)T2.VAL_ENERO + (decimal?)T2.VAL_FEBRERO + (decimal?)T2.VAL_MARZO
                              + (decimal?)T2.VAL_ABRIL + (decimal?)T2.VAL_MAYO + (decimal?)T2.VAL_JUNIO
                              + (decimal?)T2.VAL_JUNIO + (decimal?)T2.VAL_AGOSTO + (decimal?)T2.VAL_SEPTIEMBRE
                              + (decimal?)T2.VAL_OCTUBRE + (decimal?)T2.VAL_NOVIEMBRE + (decimal?)T2.VAL_DICIEMBRE
                         } by T1.COD_PLANOBJTVO into Lista
                         select new
                         {
                             Lista.FirstOrDefault().PAEG_CODIGO,
                             Lista.FirstOrDefault().ITEM_CLAVE,
                             Lista.FirstOrDefault().PAEG_NOMBRE,
                             TOTAL = Lista.Sum(s => s.TOTAL)
                         }).ToList();


            int i = 1;
            foreach (var course in Query)
            {
                viewModel.Add(new AsignarPartidas
                {
                    PAEG_CODIGO = course.PAEG_CODIGO,
                    PAEG_CLAVE = course.ITEM_CLAVE,
                    NOM_PARTIDA = course.PAEG_NOMBRE,
                    VALOR = (decimal)course.TOTAL,
                    ID_FONDO = 1,
                    NOM_FONDO = "FONDOS PROPIOS"
                });
                AddPartida(course.PAEG_CODIGO, (decimal)course.TOTAL, 1, i);
                i++;
            }
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddPartida(int PaegCodigo, decimal Valor, int Fondo, int NFila)
        {
            List<AsignarPartidas> Prods;
            if (Session["Partidas"] == null)
            {
                Prods = new List<AsignarPartidas>();
            }
            else
            {
                Prods = Session["Partidas"] as List<AsignarPartidas>;
            }

            Prods.Add(new AsignarPartidas
            {
                NUM_FILA = NFila,
                PAEG_CODIGO = PaegCodigo,
                VALOR = Valor,
                ID_FONDO = Fondo
            });

            Session["Partidas"] = Prods;

            decimal Total = Prods.Sum(t => t.VALOR);

            return Json(Total, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TipoIdentif(string CodProv)
        {
            string Tipo = "";
            var Identif = db.Proveedores.Where(f => f.COD_PROV == CodProv).FirstOrDefault();
            int mTipo = (int)Identif.TIPO_IDENTI;
            switch (mTipo)
            {
                case 0:
                    Tipo = "R.U.C. : ";
                    break;
                case 1:
                    Tipo = "Cédula : ";
                    break;
                case 2:
                    Tipo = "Pasaporte : ";
                    break;
                case 3:
                    Tipo = "Catastro : ";
                    break;
                default:
                    Tipo = "";
                    break;
            }

            string mIdent = Tipo + Identif.NUMCEDRUC_PROV;

            return Json(mIdent, JsonRequestBehavior.AllowGet);
        }



        public JsonResult FillCompromXProv(string CodProv)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var Compromisos = db.PR_MOVGASTO.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod
                             && f.COD_PROV == CodProv && f.TIPO_DOCUM == "CO" && f.ANULADO == false)
                .OrderBy(o => o.NUM_COMPROMISO);

            var viewModel = new List<PR_MOVGASTO>();

            viewModel.Add(new PR_MOVGASTO
            {
                NUM_COMPROMISO = 0
            });

            foreach (var course in Compromisos)
            {
                viewModel.Add(new PR_MOVGASTO
                {
                    NUM_COMPROMISO = course.NUM_COMPROMISO
                });

            }
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSaldoComprom(int NumComp)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            decimal SaldoIni = 0;
            decimal DevComp = 0;
            decimal Saldo = 0;

            if (NumComp > 0)
            {
                var CompSIni = (from x in db.PR_MOVGASTO
                                join y in db.PR_DETGASTO
                                on x.NUM_TRANSAC equals y.NUM_TRANSAC
                                where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod
                                && x.TIPO_DOCUM != "TX" && x.NUM_COMPROMISO == NumComp && x.ANULADO == false
                                select y.VALOR
                            );

                SaldoIni = CompSIni.Sum();

                try
                {
                    var Compromisos = (from x in db.PR_DETGASTO
                                       join y in db.CO_ASIENTOSCONT
                                        on x.NUM_TRANSAC equals y.NUM_TRANSAC
                                       where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod
                                       && y.NUM_COMPROMISO == NumComp && y.TIPO_COMPROB == "CD"
                                       select x.VALOR);

                    DevComp = Compromisos.Sum();
                }
                catch
                {

                }
            }

            Saldo = SaldoIni - DevComp;
            return Json(Saldo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetalleComprom(int NumComp)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            string Det="";

            try
            {
               var Detalle = db.PR_MOVGASTO.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod &&
              f.NUM_COMPROMISO == NumComp && f.TIPO_DOCUM == "CO").SingleOrDefault();

                Det = Detalle.DETALLE;
            }
            catch
            {

            }
            
           return Json(Det, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddProds(int ID_BODEGA,IN_ITEMS iN_ITEMS)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            int TotProd = 0;

            List<IN_ITEMS> Items = new List<IN_ITEMS>();
            
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
                    iN_ITEMS.ID_INSTITUCION = Polit.ID_INSTITUCION;
                    iN_ITEMS.PERI_CODIGO = Polit.PERI_CODIGO;
                    iN_ITEMS.ID_BODEGA = ID_BODEGA;
                    iN_ITEMS.COD_ITEM = Polit.COD_ITEM;
                    iN_ITEMS.ID_CLASE = Polit.ID_CLASE;
                    iN_ITEMS.ID_TIPO = Polit.ID_TIPO;
                    iN_ITEMS.ID_SUBTIPO = Polit.ID_SUBTIPO;
                    iN_ITEMS.COD_CAGEN = Polit.COD_CAGEN;
                    iN_ITEMS.COD_PRESEN = Polit.COD_PRESEN;
                    iN_ITEMS.CAT_CODIGO = Polit.CAT_CODIGO;
                    iN_ITEMS.NOM_ITEM = Polit.NOM_ITEM;
                    iN_ITEMS.MARCA_ITEM = Polit.MARCA_ITEM;
                    iN_ITEMS.APLIC_ITEM = Polit.APLIC_ITEM;
                    iN_ITEMS.TIPO_COSTEO = Polit.TIPO_COSTEO;
                    iN_ITEMS.COSTO_PROD = Polit.COSTO_PROD;
                    iN_ITEMS.APLICA_IVA = Polit.APLICA_IVA;
                    iN_ITEMS.PROD_PERE = Polit.PROD_PERE;
                    iN_ITEMS.PROD_DESC = Polit.PROD_DESC;
                    iN_ITEMS.PROD_NUM_SERIE = Polit.PROD_NUM_SERIE;
                    iN_ITEMS.PROD_NUM_GAR = Polit.PROD_NUM_GAR;
                    iN_ITEMS.STOCK_MIN = Polit.STOCK_MIN;
                    iN_ITEMS.STOCK_MAX = Polit.STOCK_MAX;
                    iN_ITEMS.PUNTO_REORDEN = Polit.PUNTO_REORDEN;
                    iN_ITEMS.PRECIO1 = Polit.PRECIO1;
                    iN_ITEMS.PRECIO2 = Polit.PRECIO2;
                    iN_ITEMS.COD_LINEA = Polit.COD_LINEA;
                    iN_ITEMS.COD_SUBLINEA = Polit.COD_SUBLINEA;
                    iN_ITEMS.USER_CREA = Polit.USER_CREA;
                    iN_ITEMS.FECHA_CREA = Polit.FECHA_CREA;

                    db.IN_ITEMS.Add(iN_ITEMS);
                }
                db.SaveChanges();

                TotProd = t.Count;
            }

            return Json(TotProd,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProdIVA(string CodItem)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);
            
            bool Org = false;

            var Prods = db.IN_PRODUCTOS.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == Pericod
                        && f.COD_ITEM == CodItem.Trim() ).FirstOrDefault();

            //var OrgCod = (from Item in db.IN_PRODUCTOS
            //              where Item.ID_INSTITUCION == IdInst && Item.PERI_CODIGO == Pericod
            //              && Item.COD_ITEM == CodItem
            //              select new { Cod = Item.APLICA_IVA }).FirstOrDefault();
            try
            {
                Org = Prods.APLICA_IVA;
            }
            catch
            {

            }

            return Json(Org);
        }

        public JsonResult GetNomProd(string CodItem)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);

            string NomProd = "";

            var Prods = db.IN_PRODUCTOS.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == Pericod
                        && f.COD_ITEM == CodItem && f.PROD_DESC==false).FirstOrDefault();

            //var OrgCod = (from Item in db.IN_PRODUCTOS
            //              where Item.ID_INSTITUCION == IdInst && Item.PERI_CODIGO == Pericod
            //              && Item.COD_ITEM == CodItem
            //              select new { Cod = Item.APLICA_IVA }).FirstOrDefault();
            try
            {
                NomProd = Prods.NOM_ITEM;
            }
            catch
            {

            }

            return Json(NomProd);
        }

        public JsonResult GetTipoSRI(string CodItem)
        {
            string NomProd = "";

            var Tipo = db.TiposDocs.Where(f => f.COD_TIPO_DOC == CodItem).FirstOrDefault();

            try
            {
                NomProd = Tipo.TIPO_SRI;
            }
            catch
            {

            }

            return Json(NomProd);
        }

        [HttpPost]
        public ActionResult VerFiles(string FPath)
        {
            Process.Start(FPath);
            return Json(FPath);
        }

        public JsonResult LLenaEmpleados(int CodProg)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            List<RP_EMPLEADOS> SubProgram = new List<RP_EMPLEADOS>();
            var mSubProg = (from RP_EMPLEADOS in db.RP_EMPLEADOS
                            where RP_EMPLEADOS.ORG_CODIGO == CodProg
                            && RP_EMPLEADOS.ID_INSTITUCION == IdInst
                            select RP_EMPLEADOS).ToList();

            foreach (var item in mSubProg)
            {
                SubProgram.Add(new RP_EMPLEADOS()
                {
                    ID_EMPLEADO = item.ID_EMPLEADO,
                    APELLIDOS = item.APELLIDOS
                });
            }

            SubProgram.Add(new RP_EMPLEADOS { ID_EMPLEADO = -1, APELLIDOS = "[Seleccione un Empleado]" });
            return Json(SubProgram.OrderBy(o => o.APELLIDOSNOMBRES), JsonRequestBehavior.AllowGet);
        }

        public JsonResult LLenaTiposTran(string CodUso)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);

            List<IN_TIPOSTRAN> TiposTran = new List<IN_TIPOSTRAN>();

            var TiposT = new string[] { };

            switch (CodUso)
            {
                case "CONSUMO":
                    TiposT = new string[] { "CE", "CI", "CR", "GA", "PD","IN" }; //Tipos que no van para consumo
                    break;
                case "INVERSION":
                    TiposT = new string[] { "CE", "CR", "GA", "PD", "CI" }; //Tipos que no van para inversión
                    break;
                default:
                    TiposT = new string[] { };
                    break;
            }

            var TipTran = db.IN_TIPOSTRAN.Where(f=>!TiposT.Any(x=>f.TIPO_MOV==x) && f.ID_INSTITUCION==IdInst
            && f.PERI_CODIGO==Pericod).ToList();

            foreach (var item in TipTran)
            {
                TiposTran.Add(new IN_TIPOSTRAN()
                {
                    COD_TIPOTRAN = item.COD_TIPOTRAN,
                    DESCRIP_TIPOTRAN = item.DESCRIP_TIPOTRAN
                });
            }

            TiposTran.Add(new IN_TIPOSTRAN { COD_TIPOTRAN = -1, DESCRIP_TIPOTRAN = "[Seleccione un Tipo]" });
            return Json(TiposTran.OrderBy(o => o.COD_TIPOTRAN), JsonRequestBehavior.AllowGet);
        }

        public JsonResult LLenaTiposTranEg(string CodUso)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);

            List<IN_TIPOSTRAN> TiposTran = new List<IN_TIPOSTRAN>();

            var TiposT = new string[] { };

            switch (CodUso)
            {
                case "CONSUMO":
                    TiposT = new string[] { "CO", "CR", "GA", "PD","IN" }; //Tipos que no van para consumo
                    break;
                case "INVERSION":
                    TiposT = new string[] { "CO", "CR", "GA", "PD","CI" }; //Tipos que no van para inversión
                    break;
                default:
                    TiposT = new string[] { };
                    break;
            }

            var TipTran = db.IN_TIPOSTRAN.Where(f => !TiposT.Any(x => f.TIPO_MOV == x) && f.ID_INSTITUCION == IdInst
            && f.PERI_CODIGO == Pericod).ToList();

            foreach (var item in TipTran)
            {
                TiposTran.Add(new IN_TIPOSTRAN()
                {
                    COD_TIPOTRAN = item.COD_TIPOTRAN,
                    DESCRIP_TIPOTRAN = item.DESCRIP_TIPOTRAN
                });
            }

            TiposTran.Add(new IN_TIPOSTRAN { COD_TIPOTRAN = -1, DESCRIP_TIPOTRAN = "[Seleccione un Tipo]" });
            return Json(TiposTran.OrderBy(o => o.COD_TIPOTRAN), JsonRequestBehavior.AllowGet);
        }

        public JsonResult LLenaProductos(string CodUso,int IdBodega)
        {
            
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var TUso1 = TIPOUSO.CONSUMO;

            switch (CodUso)
            {
                case "CONSUMO":
                    TUso1 = TIPOUSO.CONSUMO;
                    break;
                case "INVERSION":
                    TUso1 = TIPOUSO.INVERSION;
                    break;
            }

            List<IN_ITEMS> SubProgram = new List<IN_ITEMS>();

            var mSubProg = (from Item in db.IN_ITEMS
                            from b in db.IN_CLASES
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod && Item.ID_BODEGA == IdBodega
                          && Item.ID_INSTITUCION==b.ID_INSTITUCION && Item.PERI_CODIGO==b.PERI_CODIGO
                          && Item.ID_CLASE==b.ID_CLASE && b.TIPO_USO==TUso1
                          select Item).OrderBy(O => O.NOM_ITEM).ToList();

             foreach (var item in mSubProg)
            {
                SubProgram.Add(new IN_ITEMS()
                {
                    COD_ITEM = item.COD_ITEM,
                    NOM_ITEM = item.NOM_ITEM
                });
            }

            SubProgram.Add(new IN_ITEMS { COD_ITEM = "0", NOM_ITEM = "[Seleccione un Producto]" });
            return Json(SubProgram.OrderBy(o => o.NOM_ITEM), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCostoProm(string CodItem,int IdBodega)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);

            float VCantIni=0;
            float CCantIni = 0;
            float CostoProm=0;

            float VTPIng = 0;
            float VTPEgr = 0;
            float CTPIng = 0;
            float CTPEgr = 0;
            //Consultas de totales de valores
            var VPCantIni = db.IN_CARGA_INI.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == Pericod 
            && f.COD_ITEM == CodItem && f.ID_BODEGA==IdBodega).FirstOrDefault(); //Se obtiene el valor de la carga inicial en valores

            try
            {
                VTPIng = ((float)db.IN_DETALLETRANSAC.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == Pericod
              && f.COD_ITEM == CodItem && f.TIPO_TRAN == "I" && f.ID_BODEGA == IdBodega).Sum(c => c.TOTAL_GENERAL)); //Se obtiene el valor de ingresos en valores
            }
            catch { }
            
            try
            {
                VTPEgr = ((float)db.IN_DETALLETRANSAC.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == Pericod
              && f.COD_ITEM == CodItem && f.TIPO_TRAN == "E" && f.ID_BODEGA == IdBodega).Sum(c => c.TOTAL_GENERAL)); //Se obtiene el valor de egresos en valores
            }
            catch { }

            //Consultas de totales en cantidades
            try
            {
                CTPIng = ((float)db.IN_DETALLETRANSAC.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == Pericod
              && f.COD_ITEM == CodItem && f.TIPO_TRAN == "I" && f.ID_BODEGA == IdBodega).Sum(c => c.CANTIDAD)); //Se obtiene el valor de ingresos en cantidades
            }
            catch { }

            try
            {
                CTPEgr = ((float)db.IN_DETALLETRANSAC.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == Pericod
              && f.COD_ITEM == CodItem && f.TIPO_TRAN == "E" && f.ID_BODEGA == IdBodega).Sum(c => c.CANTIDAD)); //Se obtiene el valor de egresos en cantidades

            }
            catch { }
            
            try
            {
                VCantIni = VPCantIni.VALOR_TOTAL;
            }
            catch { }

            try
            {
                CCantIni = VPCantIni.CANTIDAD;
            }
            catch { }

            float VTExis = VCantIni + VTPIng - VTPEgr; //Total de existencias en valores
            float CTExis = CCantIni + CTPIng - CTPEgr; //Total de existencias en cantidades
            CostoProm = VTExis / CTExis;
            return Json(CostoProm);
        }

        public JsonResult GetStockProd(string CodItem, int IdBodega)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);

            float CCantIni = 0;
            float Stock = 0;

            float CTPIng = 0;
            float CTPEgr = 0;
            //Consultas de totales de valores
            var VPCantIni = db.IN_CARGA_INI.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == Pericod
            && f.COD_ITEM == CodItem && f.ID_BODEGA == IdBodega).FirstOrDefault(); //Se obtiene el valor de la carga inicial en valores


            //Consultas de totales en cantidades
            try
            {
                CTPIng = ((float)db.IN_DETALLETRANSAC.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == Pericod
              && f.COD_ITEM == CodItem && f.TIPO_TRAN == "I" && f.ID_BODEGA == IdBodega).Sum(c => c.CANTIDAD)); //Se obtiene el valor de ingresos en cantidades
            }
            catch { }

            try
            {
                CTPEgr = ((float)db.IN_DETALLETRANSAC.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == Pericod
              && f.COD_ITEM == CodItem && f.TIPO_TRAN == "E" && f.ID_BODEGA == IdBodega).Sum(c => c.CANTIDAD)); //Se obtiene el valor de egresos en cantidades

            }
            catch { }

            try
            {
                CCantIni = VPCantIni.CANTIDAD;
            }
            catch { }

            Stock = CCantIni + CTPIng - CTPEgr; //Total de existencias en cantidades

            return Json(Stock);
        }

        public JsonResult GetTipoTran(int CodItem)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);

            bool MovIva = false;

            var Tipo = db.IN_TIPOSTRAN.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO==Pericod
            && f.COD_TIPOTRAN==CodItem).FirstOrDefault();

            try
            {
                MovIva = Tipo.CON_IVA;
            }
            catch { }


            return Json(MovIva);
        }

        public JsonResult GetIVAGasto()
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int Pericod = Convert.ToInt32(Session["IdPeriodo"]);

            bool MovIva = false;

            var Tipo = db.CO_PARAMETROS.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == Pericod
            ).FirstOrDefault();

            try
            {
                MovIva = Tipo.IVA_GASTO;
            }
            catch { }


            return Json(MovIva);
        }

        public JsonResult LLenaCuentasCxP(int NumC)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var Comp = db.PR_MOVGASTO.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod
              && f.NUM_COMPROMISO == NumC && f.TIPO_DOCUM == "CO"
            ).FirstOrDefault();
            //string mCta = CodCta.Substring(0, 9);

            int NT = Comp.NUM_TRANSAC;

            var CtaXPagar = (from c in db.PR_DETGASTO
                         from d in db.PR_PARTEGRESOS
                         from e in db.CO_RELACION
                         from f in db.CO_CUENTASCONT
                         where c.ID_INSTITUCION == d.ID_INSTITUCION && c.PERI_CODIGO == d.PERI_CODIGO &&
                         c.PAEG_CODIGO == d.PAEG_CODIGO && d.ID_INSTITUCION == e.ID_INSTITUCION && d.PERI_CODIGO == e.PERI_CODIGO &&
                         d.ITEM_CLAVE.Substring(0, 6) == e.PPDEBITO && e.ID_INSTITUCION == f.ID_INSTITUCION &&
                         e.PERI_CODIGO == f.PERI_CODIGO && f.CODIGO_CG.Contains(e.CUENTA_PAGO) && f.TIPO_CG == "A" &&
                         c.ID_INSTITUCION == IdInst && c.PERI_CODIGO == PeriCod && c.NUM_TRANSAC == NT
                         select new { f.CODIGO_CG, NOM_CUENTA = f.CODIGO_CG + " " + f.NOMBRE_CG }).Distinct().ToList();


            List<CO_CUENTASCONT> SubProgram = new List<CO_CUENTASCONT>();

            foreach (var item in CtaXPagar)
            {
                SubProgram.Add(new CO_CUENTASCONT()
                {
                    CODIGO_CG = item.CODIGO_CG,
                    NOMBRE_CG = item.NOM_CUENTA
                });
            }
            SubProgram.Add(new CO_CUENTASCONT { CODIGO_CG = "", NOMBRE_CG = "[Seleccione una Cuenta]" });
            return Json(SubProgram.OrderBy(o => o.CODIGO_CG), JsonRequestBehavior.AllowGet);
        }

        public JsonResult LLenaCuentasDebitoComp(string CodCta, int NumC) //Carga cuentas contables de débito con compromiso
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var Comp = db.PR_MOVGASTO.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod
              && f.NUM_COMPROMISO == NumC && f.TIPO_DOCUM == "CO"
            ).FirstOrDefault();
 
            int NT = Comp.NUM_TRANSAC;

            string mCta = CodCta.Substring(0, 9);

            var CtaDebito = (from c in db.PR_DETGASTO
                         from d in db.PR_PARTEGRESOS
                         from e in db.CO_RELACION
                         from f in db.CO_CUENTASCONT
                         where c.ID_INSTITUCION == d.ID_INSTITUCION && c.PERI_CODIGO == d.PERI_CODIGO &&
                         c.PAEG_CODIGO == d.PAEG_CODIGO && d.ID_INSTITUCION == e.ID_INSTITUCION && d.PERI_CODIGO == e.PERI_CODIGO &&
                         d.ITEM_CLAVE.Substring(0, 6) == e.PPDEBITO && e.ID_INSTITUCION == f.ID_INSTITUCION &&
                         e.PERI_CODIGO == f.PERI_CODIGO && e.CODIGO_CG == f.CODIGO_CG && //f.TIPO_CG == "A" &&
                         c.ID_INSTITUCION == IdInst && c.PERI_CODIGO == PeriCod && c.NUM_TRANSAC == NT
                         select new { f.CODIGO_CG, NOM_CUENTA = f.CODIGO_CG + " " + f.NOMBRE_CG }).Distinct().ToList();

            List<CO_CUENTASCONT> SubProgram = new List<CO_CUENTASCONT>();

            foreach (var item in CtaDebito)
            {
                SubProgram.Add(new CO_CUENTASCONT()
                {
                    CODIGO_CG = item.CODIGO_CG,
                    NOMBRE_CG = item.NOM_CUENTA
                });
            }
            SubProgram.Add(new CO_CUENTASCONT { CODIGO_CG = "", NOMBRE_CG = "[Seleccione una Cuenta]" });
            return Json(SubProgram.OrderBy(o => o.CODIGO_CG), JsonRequestBehavior.AllowGet);
        }

        public JsonResult LLenaCuentasDebito(string CodCta)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            string mCta = CodCta.Substring(0, 9);

            var CtaDebito = (from e in db.CO_RELACION
                         from f in db.CO_CUENTASCONT
                         where e.ID_INSTITUCION == f.ID_INSTITUCION && e.PERI_CODIGO == f.PERI_CODIGO &&
                         e.CODIGO_CG == f.CODIGO_CG && e.CUENTA_PAGO.Contains(mCta)
                         select new {f.CODIGO_CG,NOM_CUENTA=f.CODIGO_CG+" "+f.NOMBRE_CG } ).Distinct().ToList();

            List<CO_CUENTASCONT> SubProgram = new List<CO_CUENTASCONT>();

            foreach (var item in CtaDebito)
            {
                SubProgram.Add(new CO_CUENTASCONT()
                {
                    CODIGO_CG = item.CODIGO_CG,
                    NOMBRE_CG = item.NOM_CUENTA
                });
            }
            SubProgram.Add(new CO_CUENTASCONT { CODIGO_CG = "", NOMBRE_CG = "[Seleccione una Cuenta]" });
            return Json(SubProgram.OrderBy(o => o.CODIGO_CG), JsonRequestBehavior.AllowGet);
        }

        public void AddDetCtaxPagar(int NumFila, string CodTDoc,  DateTime FechaDoc,
            string NumDoc, decimal ValorDoc, int PorcIva, decimal TotIVA, decimal Total, string CtaXPag,
            string CtaDebito, bool Rfiva, bool AD, bool IG, string Autoriza, DateTime FechaCadDoc,
            string Repos, int ICE)
        //public void AddDetCtaxPagar(int NumFila, string CodTDoc, DateTime FechaDoc)
        {
            var TipoSRI = db.TiposDocs.Where(f => f.COD_TIPO_DOC == CodTDoc).FirstOrDefault();
            
            List<AsignaDetCtasXPagar> Prods;
            if (Session["DetCtasPPag"] == null)
            {
                Prods = new List<AsignaDetCtasXPagar>();
            }
            else
            {
                Prods = Session["DetCtasPPag"] as List<AsignaDetCtasXPagar>;
            }

            Prods.Add(new AsignaDetCtasXPagar
            {
                NUM_FILA = NumFila,
                COD_TIPO_DOC = CodTDoc,
                TIPOSRI = TipoSRI.TIPO_SRI,
                //FECHA = Fecha,
                FECHA_DOC = FechaDoc,
                NUM_DOC = NumDoc,
                VALOR_DOC = ValorDoc,
                IVA_PORC = PorcIva,
                TOT_IVA = TotIVA,
                TOTAL = Total,
                CTA_X_PAG = CtaXPag,
                CTA_DEBITO = CtaDebito,
                RFIVA = Rfiva,
                AD = AD,
                IG = IG,
                AUTORIZACION = Autoriza,
                FECHACAD_DOC = FechaCadDoc,
                REPOSICION = Repos,
                ICE = ICE
            });

            Session["DetCtasPPag"] = Prods;
            //var recursos = Session["Productos"] as List<AsignaProductos>;
        }

        public void RemoveDetCxP(int NumFila) //Borra filas de detalle de Cuentas por Pagar
        {
            var Prods = Session["DetCtasPPag"] as List<AsignaDetCtasXPagar>;

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

            Session["DetCtasPPag"] = Prods;
        }

        public ActionResult Save(string CodProv,DateTime FechaTran,  string TipoTran, 
            int NumComp, String Detalle, string TipoMov) //, CO_DETCTASPPAG[] order)
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
                //model.FECHA_COMPRA = Fecha;
                //model.COD_TIPO_DOC = CodTipDoc;
                model.COD_PROV = CodProv;
                model.DETALLE = Detalle;
                //model.TIPO_SRI = TipoSRI;
                //model.FECHA_DOC = FechaDoc;
                //model.NUM_DOC = NumDoc;
                //model.AUTORIZACION = Autoriza;
                //model.FECHACAD_DOC = FechaCadDoc;
                model.USER_CREA = Session["Usuario"].ToString();
                model.FECHA_CREA = DateTime.Now;
                db.IN_COMPRAS.Add(model);

                int NumFila = 1;
                //foreach (var item in order)
                //{
                //    IN_DETCOMPRA O = new IN_DETCOMPRA();
                //    O.ID_INSTITUCION = IdInst;
                //    O.PERI_CODIGO = PeriCod;
                //    O.NUM_COMPRA = 1;
                //    O.NUM_FILA = NumFila;
                //    //O.COD_ITEM = item.COD_ITEM;
                //    //O.APLICA_IVA = item.APLICA_IVA;
                //    //O.CANTIDAD = item.CANTIDAD;
                //    //O.COSTO_UNIT = item.COSTO_UNIT;
                //    //O.SUBTOTAL = item.SUBTOTAL;
                //    O.COSTO_TOTAL = O.COSTO_TOTAL;
                //    //O.VAL_IVA = item.VAL_IVA;
                //    db.IN_DETCOMPRA.Add(O);
                //}
                db.SaveChanges();
                result = VariablesGlobales.MsgSuccess;
                //}
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }

        public JsonResult LLenaTiposRet(int TipoRet)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var TiposRet = db.CO_CLASIFRETEN.Where(p => p.ID_INSTITUCION == IdInst && 
                        p.PERI_CODIGO==PeriCod && p.TIPO_RET==TipoRet).ToList();
            List<CO_CLASIFRETEN> Items = new List<CO_CLASIFRETEN>();
            foreach (var item in TiposRet)
            {
                Items.Add(new CO_CLASIFRETEN()
                {
                    CODIGO_RET = item.CODIGO_RET,
                    DESCRIP_RET = item.CODIGO_RET+" "+ item.DESCRIP_RET
                });
            }

            //Items.Add(new Periodos { PERI_CODIGO = 0, PROG_NOMBRE = "[Seleccione un Programa]" });
            return Json(Items.OrderBy(o => o.CODIGO_RET), JsonRequestBehavior.AllowGet);
        }

        public JsonResult LLenaValoresXFac(int NumTran)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);

            var TiposRet = db.CO_VALORESXFACTURA.Where(p => p.ID_INSTITUCION == IdInst &&
                        p.PERI_CODIGO == PeriCod && p.NUM_TRANSAC == NumTran).ToList();
            List<CO_VALORESXFACTURA> Items = new List<CO_VALORESXFACTURA>();
            foreach (var item in TiposRet)
            {
                Items.Add(new CO_VALORESXFACTURA()
                {
                    TIPO_BS = item.TIPO_BS,
                    VALOR_DOC = item.VALOR_DOC
                });
            }

            //Items.Add(new Periodos { PERI_CODIGO = 0, PROG_NOMBRE = "[Seleccione un Programa]" });
            return Json(Items.OrderBy(o => o.TIPO_BS), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveValoresRet(int NumTran,int NumFila,int IvaPorc, CO_VALORESXFACTURA[] order)
        {
            int IdInst = Convert.ToInt32(Session["IdInst"]);
            int PeriCod = Convert.ToInt32(Session["IdPeriodo"]);
            string User = Session["Usuario"].ToString();
            
            var CtasPPag = db.CO_CTASPPAGAR.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod 
            && f.NUM_TRANSAC == NumTran).FirstOrDefault();

            string CodProv = CtasPPag.COD_PROV;

            try
            {
           //     var allFondos = db.FIRMAS.Where(f => f.ID_INSTITUCION == IdInst && f.PERI_CODIGO == PeriCod &&
           //f.CODIGO_DOC == Codigo && f.USUA_FIRMA == User)
           //    .First(x => x.NUM_FILA == db.FIRMAS.Max(y => y.NUM_FILA));

                //NumFila = allFondos.NUM_FILA + 1;
            }
            catch
            {
                //NumFila = 1;
            }


            if (Session["Usuario"].ToString() != "")
            {
                string result = "Error! Order Is Not Complete!";
                //if (name != null && address != null && order != null)
                //{

                // Query the database for the rows to be deleted.
                var Reg = db.CO_VALORESXFACTURA.Where(d => d.ID_INSTITUCION == IdInst
                   && d.PERI_CODIGO == PeriCod && d.NUM_TRANSAC == NumTran);
                db.CO_VALORESXFACTURA.RemoveRange(Reg);

                db.SaveChanges();

                decimal TotIva;
                foreach (var item in order)
                {
                    CO_VALORESXFACTURA O = new CO_VALORESXFACTURA();
                    O.ID_INSTITUCION = IdInst;
                    O.PERI_CODIGO = PeriCod;
                    O.NUM_TRANSAC = NumTran;
                    O.COD_PROV = CodProv;
                    O.NUM_FILA =NumFila.ToString();
                    O.TIPO_BS = item.TIPO_BS;
                    O.VALOR_DOC = item.VALOR_DOC;
                    O.IVA_PORC = IvaPorc;
                    TotIva = item.VALOR_DOC * IvaPorc / 100;
                    O.TOT_IVA = TotIva;
                    O.TOTAL = item.VALOR_DOC + TotIva;
                    db.CO_VALORESXFACTURA.Add(O);
                }
                db.SaveChanges();
                result = "Los Datos se Guardaron Satisfatoriamente!";
                //}
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }
    }
}