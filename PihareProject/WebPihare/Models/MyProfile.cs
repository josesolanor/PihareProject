using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebPihare.Entities;

namespace WebPihare.Models
{
    public class MyProfile
    {
        public Commisioner Commisioner { get; set; }

        [Required(ErrorMessage = "Password actual es requerido")]
        [DisplayFormat(NullDisplayText = "Ingrese su password actual...")]
        [DataType(DataType.Password)]
        public string ActualPassword { get; set; }
        [Required(ErrorMessage = "Password nuevo es requerido")]
        [DisplayFormat(NullDisplayText = "Ingrese su nuevo password...")]
        [DataType(DataType.Password)]
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,50}$",
        //                        ErrorMessage = "La password debe ser almenos de 8 caracteres y contener de 3 a 4 de los siguientes: Mayusculas, minusculas, numeros y caracteres especiales (e.g. !@#$%^&*)")]

        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirmar password es requerido")]
        [DisplayFormat(NullDisplayText = "Confirme el nuevo Password...")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }

    }
}
