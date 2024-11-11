using SIGEFINew.Models;
using SIGEFINew.Models.Presupuesto;
using System.Collections.Generic;
using System.Linq;


namespace SIGEFINew.Clases
{
    public class Funciones
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public decimal MontoxDirec(int IdInst, int PeriCod, int OrgCodigo)
        {
            var Total2 = (from x in db.TECHOSPRESUPs
                          where x.ID_INSTITUCION == IdInst && x.PERI_CODIGO == PeriCod
&& x.ORG_CODIGO == OrgCodigo
                          select x.VALOR_CANTIDAD);
            try
            {
                decimal Difer = Total2.Sum();
                string totc = Difer.ToString("n2");
                return Difer;
            }
            catch
            {
                return 0;
            }

        }

        public List<PR_PARTEGRESOS> FillPartidasEgre(int IdInst, int PeriCod)
        {
            List<PR_PARTEGRESOS> Items = new List<PR_PARTEGRESOS>();
            var mPolit = (from Item in db.PR_PARTEGRESOS
                          where Item.ID_INSTITUCION == IdInst
                          && Item.PERI_CODIGO == PeriCod
                          select new { Item.PAEG_CODIGO, Item.PAEG_CLAVE, PAEG_NOMBRE = Item.PAEG_CLAVE + " " + Item.PAEG_NOMBRE }).OrderBy(O => O.PAEG_CLAVE).ToList();

            foreach (var Polit in mPolit)
            {
                Items.Add(new PR_PARTEGRESOS()
                {
                    PAEG_CODIGO = Polit.PAEG_CODIGO,
                    PAEG_CLAVE = Polit.PAEG_CLAVE,
                    PAEG_NOMBRE = Polit.PAEG_NOMBRE
                });
            }
            return Items;
        }

        public List<TiposDoc> FillTiposDoc()
        {
            var mPolit = (from Item in db.TiposDocs
                          select Item).ToList();
            return mPolit;
        }
    }
}