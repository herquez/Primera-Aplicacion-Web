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
    
    public partial class Cliente
    {
        public Cliente()
        {
            this.VENTA = new HashSet<VENTA>();
        }
    
        public int IIDCLIENTE { get; set; }
        public string NOMBRE { get; set; }
        public string APPATERNO { get; set; }
        public string APMATERNO { get; set; }
        public string EMAIL { get; set; }
        public string DIRECCION { get; set; }
        public Nullable<int> IIDSEXO { get; set; }
        public string TELEFONOFIJO { get; set; }
        public string TELEFONOCELULAR { get; set; }
        public Nullable<int> BHABILITADO { get; set; }
        public Nullable<int> bTieneUsuario { get; set; }
        public string TIPOUSUARIO { get; set; }
    
        public virtual Sexo Sexo { get; set; }
        public virtual TIPOUSUARIOREGISTRO TIPOUSUARIOREGISTRO { get; set; }
        public virtual ICollection<VENTA> VENTA { get; set; }
    }
}
