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
    
    public partial class Sexo
    {
        public Sexo()
        {
            this.Cliente = new HashSet<Cliente>();
            this.Empleado = new HashSet<Empleado>();
        }
    
        public int IIDSEXO { get; set; }
        public string NOMBRE { get; set; }
        public Nullable<int> BHABILITADO { get; set; }
    
        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
    }
}
