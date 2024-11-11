namespace SIGEFINew.ViewModels
{
    public class AssignedFondosData
    {
        public int ID_INSTITUCION { get; set; }
        public int PERI_CODIGO { get; set; }
        public int COD_PLAN { get; set; }
        public int ORG_CODIGO { get; set; }
        public string COD_PLANOBJTVO { get; set; }
        public int ID_FONDO { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal VALOR { get; set; }
        public bool Assigned { get; set; }
    }
}