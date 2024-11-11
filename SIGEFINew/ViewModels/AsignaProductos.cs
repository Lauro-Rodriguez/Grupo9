namespace SIGEFINew.ViewModels
{
    public class AsignaProductos
    {
        public int NUM_FILA { get; set; }
        public string CAT_CODIGO { get; set; }
        public string COD_ITEM { get; set; }
        public bool APLICA_IVA { get; set; }
        public int PORC_IVA { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal COSTO_UNIT { get; set; }
        public decimal SUBTOTAL { get; set; }
        public decimal COSTO_TOTAL { get; set; }
        public decimal VAL_IVA { get; set; }

    }
}