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
    public class Role_Of_Admin
    {
        [ForeignKey("Admin")]
        public int Admin_Id { get; set; }

        [ForeignKey("Admin_Role")]
        public int Admin_Role_Id { get; set; }
        
        [Required]
        [StringLength(15, ErrorMessage = "Max 15 characters")]
        public string Role_Name { get; set; }

    }
}
