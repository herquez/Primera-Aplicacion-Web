using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Primera_Aplicacion_Web.Models {
    public class SucursalCLS {
        [Display(Name = "ID Sucursal")]
        public int iidsucursal { get; set; }

        [Required]
        [Display(Name = "Nombre Sucursal")]
        [StringLength(100, ErrorMessage = "La longitud máxima son 100 caracteres.")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Teléfono Sucursal")]
        [StringLength(10, ErrorMessage = "La longitud máxima son 10 caracteres.")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage ="Ingrese un número de teléfono.")]
        public string telefono { get; set; }

        [Required]
        [Display(Name = "Dirección Sucursal")]
        [StringLength(200, ErrorMessage = "La longitud máxima son 200 caracteres.")]
        public string direccion { get; set; }

        [Required]
        [Display(Name = "e-mail Sucursal")]
        [StringLength(100, ErrorMessage = "La longitud máxima son 100 caracteres.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Ingresa un e-mail valido.")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Fecha de apertura")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime fechaapertura { get; set; }

        public int bhabilitado { get; set; }
    }
}