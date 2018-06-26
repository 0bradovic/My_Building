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
    public class Building
    {
        [Key]
        public int Id { get; set; }

        public string Nickname { get; set; }

        public DateTime Date_Of_Creation { get; set; }

        public string Address { get; set; }

        public int Number_Of_Apartments { get; set; }

        public int Number_Of_Tenants { get; set; }

        public int Number_Of_Parking_Places { get; set; }

        public int Number_Of_Basements { get; set; }

        public int Number_Of_Entrances { get; set; }

    }
}
