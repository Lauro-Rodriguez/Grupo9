//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIGEFINew.Clases
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cliente
    {
        public int Codigo { get; set; }
        public string CI { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    
        public virtual Cliente Cliente1 { get; set; }
        public virtual Cliente Cliente2 { get; set; }
    }
}
