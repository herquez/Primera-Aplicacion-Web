//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Primera_Aplicacion_Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sucursal
    {
        public Sucursal()
        {
            this.Bus = new HashSet<Bus>();
        }
    
        public int IIDSUCURSAL { get; set; }
        public string NOMBRE { get; set; }
        public string DIRECCION { get; set; }
        public string TELEFONO { get; set; }
        public string EMAIL { get; set; }
        public Nullable<System.DateTime> FECHAAPERTURA { get; set; }
        public Nullable<int> BHABILITADO { get; set; }
    
        public virtual ICollection<Bus> Bus { get; set; }
    }
}
