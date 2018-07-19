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
    [Table("tbl_Admin")]
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string First_Name { get; set; }
        
        public string Last_Name { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Date_Of_Birth { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Must have 13 numbers.")]
        public string JMBG { get; set; }
        
        public string Address { get; set; }
        
        public string UserName { get; set; }


        public ICollection<Handles> Handleses { get; set; }

        public ICollection<Created_Cleaning_Plan> Created_Cleaning_Plans { get; set; }

        public ICollection<Issued_Invoice> Issued_Invoices { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<Post> Posts { get; set; }


    }
}
