using System.Collections.Generic;
namespace SIGEFINew.ViewModels
{
    public class SolicDispPresOrder
    {
        public Models.Nomina.RP_EMPLEADOS RP_EMPLEADOS { get; set; }
        public Models.POA.POA_PLANOBJETIVOS POA_PLANOBJETIVOS { get; set; }
        public List<AsignarDetDispPresup> AsignarDetDispPresup { get; set; }

    }
}