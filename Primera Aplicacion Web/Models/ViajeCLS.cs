using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Primera_Aplicacion_Web.Models {
    public class ViajeCLS {
        [Display(Name = "ID Viaje")]
        public int iidviaje { get; set; }

        [Required]
        [Display(Name ="Lugar de Origen")]
        public int iidlugarorigen { get; set; }

        [Required]
        [Display(Name = "Lugar de Destino")]
        public int iidlugardestino { get; set; }

        [Required]
        [Display(Name = "Precio")]
        public decimal precio { get; set; }

        [Required]
        [Display(Name = "Fecha del Viaje")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaviaje { get; set; }

        [Required]
        [Display(Name = "Bus")]
        public int iidbus { get; set; }

        [Display(Name = "Numero de asientos disponibles")]
        public int numeroasientosdisponibles { get; set; }

        public int bhabilitado { get; set; }

        // PROPIEDADES ADICIONALES:
        [Display(Name = "Lugar de Origen")]
        public string nombrelugarorigen { get; set; }

        [Display(Name = "Lugar de Destino")]
        public string nombrelugardestino { get; set; }

        [Display(Name = "Placa del Bus")]
        public string placabus { get; set; }

        public string errorMessage { get; set; }
    }
}