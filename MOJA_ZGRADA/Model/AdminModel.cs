﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Model
{
    public class AdminModel
    {
        [Required]
        [StringLength(15, ErrorMessage = "Max 15 characters")]
        public string First_Name { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Max 15 characters")]
        public string Last_Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date_Of_Birth { get; set; }

        [Required]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        //[RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Must have 13 numbers.")]
        public int JMBG { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Max 50 numbers.")]
        public string Address { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_-]{3,15}$", ErrorMessage = "Min 3 Max 15 characters, can contain only characters, numbers , _ and - ")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_-]{3,15}$", ErrorMessage = "Min 3 Max 15 characters, can contain only characters, numbers , _ and - ")]
        public string Password { get; set; }
    }
}