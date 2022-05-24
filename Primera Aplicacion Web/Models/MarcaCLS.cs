using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/* 
 * El modelo Marca hace el mapeo de lo que esta en la base de datos a la clase modelo.
 * El tag [Display] muestra el nombre que llevará la variable en las vistas.
 * El tag [Required] hace la variable necesaria para el formulario.
 * El tag [StringLength()] indica la longitud maxima de la variable string.
 */

namespace Primera_Aplicacion_Web.Models {
    public class MarcaCLS {
        [Display(Name = "ID Marca")]
        public int iidmarca { get; set; }

        [Required]
        [Display(Name = "Nombre Marca")]
        [StringLength(100,ErrorMessage ="La longitud máxima son 100 caracteres.")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Descripcion Marca")]
        [StringLength(200, ErrorMessage = "La longitud máxima son 200 caracteres.")]
        public string descripcion { get; set; }

        public int bhabilitado { get; set; }
    }
}