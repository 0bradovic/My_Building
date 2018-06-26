using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.ComponentModel.DataAnnotations;

namespace MOJA_ZGRADA.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public DateTime Date_Of_Birth { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int JMBG { get; set; }

        public string Address { get; set; }

        public string Apartment_Number { get; set; }

        public int Number_Of_Occupants { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

    }
}
