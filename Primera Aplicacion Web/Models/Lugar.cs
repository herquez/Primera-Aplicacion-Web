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
    
    public partial class Lugar
    {
        public Lugar()
        {
            this.Viaje = new HashSet<Viaje>();
            this.Viaje1 = new HashSet<Viaje>();
        }
    
        public int IIDLUGAR { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<int> BHABILITADO { get; set; }
    
        public virtual ICollection<Viaje> Viaje { get; set; }
        public virtual ICollection<Viaje> Viaje1 { get; set; }
    }
}