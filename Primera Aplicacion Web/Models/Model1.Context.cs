﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BDPasajeEntities : DbContext
    {
        public BDPasajeEntities()
            : base("name=BDPasajeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Asiento> Asiento { get; set; }
        public DbSet<Bus> Bus { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<DETALLEVENTA> DETALLEVENTA { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Lugar> Lugar { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Modelo> Modelo { get; set; }
        public DbSet<Pagina> Pagina { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<RolPagina> RolPagina { get; set; }
        public DbSet<Sexo> Sexo { get; set; }
        public DbSet<Sucursal> Sucursal { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<TipoBus> TipoBus { get; set; }
        public DbSet<TipoContrato> TipoContrato { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<TIPOUSUARIOREGISTRO> TIPOUSUARIOREGISTRO { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<VENTA> VENTA { get; set; }
        public DbSet<Viaje> Viaje { get; set; }
    }
}
