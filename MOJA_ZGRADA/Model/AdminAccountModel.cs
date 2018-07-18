﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Model
{
    public class AdminAccountModel
    {
        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date_Of_Birth { get; set; }

        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [StringLength(13, MinimumLength = 13, ErrorMessage = "Must have 13 numbers.")]
        public string JMBG { get; set; }

        public string Address { get; set; }

    }
}