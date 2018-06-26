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
    public class Created_Cleaning_Plan
    {
        [ForeignKey("Cleaning_Plan")]
        public int Cleaning_Plan_Id { get; set; }

        [ForeignKey("Building")]
        public int Building_Id { get; set; }

        [ForeignKey("Admin")]
        public int Admin_Id { get; set; }

        public DateTime Cleaning_Issued_DateTime { get; set; }

        public DateTime Cleaning_DateTime { get; set; }


    }
}
