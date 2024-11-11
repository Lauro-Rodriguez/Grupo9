namespace SIGEFINew.ViewModels
{
    public class AsignarDetDispPresup
    {
        public int ID_INSTITUCION { get; set; }
        public int PERI_CODIGO { get; set; }
        public int CODIGO_DOC { get; set; }
        public int ORG_CODIGO { get; set; }
        public int NUM_SOLIC { get; set; }
        public string CAT_CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal VALOR_REC { get; set; }
        public bool Assigned { get; set; }
    }
}