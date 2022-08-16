using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Primera_Aplicacion_Web.Models {
    public class BusCLS {
        [Display(Name ="ID Bus")]
        public int iidbus { get; set; }

        [Required]
        [Display(Name ="Sucursal")]
        public int iidsucursal { get; set; }

        [Required]
        [Display(Name ="Tipo de Bus")]
        public int iidtipobus { get; set; }

        [Required]
        [Display(Name ="Placa")]
        [StringLength(100, ErrorMessage = "La longitud máxima son 100 caracteres.")]
        public string placa { get; set; }

        [Required]
        [Display(Name = "Fecha de Compra")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechacompra { get; set; }

        [Required]
        [Display(Name = "Modelo")]
        public int iidmodelo { get; set; }

        [Required]
        [Display(Name = "Número de Filas")]
        public int numerofilas { get; set; }

        [Required]
        [Display(Name = "Número de Columnas")]
        public int numerocolumnas { get; set; }

        public int bhabilitado { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "La longitud máxima son 200 caracteres.")]
        public string descripcion { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "La longitud máxima son 200 caracteres.")]
        public string observacion { get; set; }

        [Required]
        [Display(Name = "Marca")]
        public int iidmarca { get; set; }

        //PROPIEDADES ADICIONALES
        [Display(Name = "Sucursal")]
        public string nombresucursal { get; set; }

        [Display(Name = "Tipo de Bus")]
        public string nombretipobus { get; set; }

        [Display(Name = "Modelo")]
        public string nombremodelo { get; set; }

        [Display(Name = "Marca")]
        public string nombremarca { get; set; }

        public string errorMessage { get; set; }
    }
}