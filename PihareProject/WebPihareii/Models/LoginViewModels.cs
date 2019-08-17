using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPihareii.Models
{

    public class LoginViewModels
    {
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El campo cuenta es obligatorio.")]
            public string Username { get; set; }

            [Required(ErrorMessage = "El campo contraseña es obligatorio.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

        }
    }
}
