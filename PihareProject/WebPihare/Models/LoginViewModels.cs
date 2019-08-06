using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPihare.Models
{

    public class LoginViewModels
    {
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El campo correo electrónico es obligatorio.")]
            [EmailAddress(ErrorMessage = "El correo electrónico no es una dirección de correo electrónico válida.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "El campo contraseña es obligatorio.")]
            [StringLength(100, ErrorMessage = "El número de caracteres del {0} debe ser al menos {2}.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

        }
    }
}
