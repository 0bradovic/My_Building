using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Model
{
    public class LoginModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_-]{3,15}$", ErrorMessage = "Min 3 Max 15 characters, can contain only characters, numbers , _ and - ")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_-]{3,15}$", ErrorMessage = "Min 3 Max 15 characters, can contain only characters, numbers , _ and - ")]
        public string Password { get; set; }
    }
}
