using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Primera_Aplicacion_Web.Models {
    public class UserTypeCLS {
        [Display(Name = "ID Tipo de Usuario")]
        public int iidtipousuario { get; set; }

        [Required]
        [Display(Name = "Nombre Tipo de Usuario")]
        [StringLength(150, ErrorMessage = "La longitud máxima son 150 caracteres.")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Descripción Tipo de Usuario")]
        [StringLength(250, ErrorMessage = "La longitud máxima son 200 caracteres.")]
        public string descripcion { get; set; }

        public int bhabilitado { get; set; }

        //Aditional propeties
        public string errorMessage { get; set; }
    }
}