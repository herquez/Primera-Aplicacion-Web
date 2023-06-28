using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Primera_Aplicacion_Web.Models {
    public class ClienteCLS {
        [Display(Name = "ID Cliente")]
        public int iidcliente { get; set; }

        [Required]
        [Display(Name = "Nombre Cliente")]
        [StringLength(100, ErrorMessage = "La longitud máxima son 100 caracteres.")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Apellido Paterno")]
        [StringLength(150, ErrorMessage = "La longitud máxima son 150 caracteres.")]
        public string appaterno { get; set; }

        [Required]
        [Display(Name = "Apellido Materno")]
        [StringLength(150, ErrorMessage = "La longitud máxima son 150 caracteres.")]
        public string apmaterno { get; set; }

        [Required]
        [Display(Name = "e-mail Cliente")]
        [StringLength(200, ErrorMessage = "La longitud máxima son 200 caracteres.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Ingresa un e-mail valido.")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Dirección Cliente")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "La longitud máxima son 200 caracteres.")]
        public string direccion { get; set; }

        [Required]
        [Display(Name = "Sexo Cliente")]
        public int iidsexo { get; set; }

        [Required]
        [Display(Name = "Teléfono Fijo")]
        [StringLength(10, ErrorMessage = "La longitud máxima son 10 caracteres.")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Ingrese un número de teléfono.")]
        public string telefonofijo { get; set; }

        [Required]
        [Display(Name = "Teléfono Celular")]
        [StringLength(10, ErrorMessage = "La longitud máxima son 10 caracteres.")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Ingrese un número de teléfono.")]
        public string telefonocelular { get; set; }

        public int bhabilitado { get; set; }

        //Aditional propeties
        public string errorMessage { get; set; }

        public string searchName { get; set; }
    }
}