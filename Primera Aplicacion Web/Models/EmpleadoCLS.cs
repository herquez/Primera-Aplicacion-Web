using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Primera_Aplicacion_Web.Models {
    public class EmpleadoCLS {
        [Display(Name ="ID Empleado")]
        public int iidempleado { get; set; }

        [Required]
        [Display(Name ="Nombre Empleado")]
        [StringLength(100, ErrorMessage = "La longitud máxima son 100 caracteres.")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Apellido Paterno")]
        [StringLength(200, ErrorMessage = "La longitud máxima son 200 caracteres.")]
        public string appaterno { get; set; }

        [Required]
        [Display(Name = "Apellido Materno")]
        [StringLength(200, ErrorMessage = "La longitud máxima son 200 caracteres.")]
        public string apmaterno { get; set; }

        public string searchName { get; set; }

        [Required]
        [Display(Name = "Fecha de contrato")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechacontrato { get; set; }

        [Required]
        [Display(Name = "Sueldo")]
        [DataType(DataType.Currency)]
        [Range(0,100000, ErrorMessage ="Fuera de rango.")]
        public decimal sueldo { get; set; }

        [Display(Name ="Tipo de Usuario")]
        public int iidtipousuario { get; set; }

        [Display(Name = "Tipo de Contrato")]
        public int iidtipocontrato { get; set; }

        [Display(Name = "Sexo")]
        public int iidsexo { get; set; }

        public int bhabilitado { get; set; }

        // PROPIEDADES ADICIONALES:
        [Display(Name ="Tipo de Usuario")]
        public string nombretipousuario { get; set; }

        [Display(Name = "Tipo de Contrato")]
        public string nombretipocontrato { get; set; }

        public string errorMessage { get; set; }
    }
}