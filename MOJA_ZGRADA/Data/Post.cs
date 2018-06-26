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
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Admin")]
        public int Admin_Id { get; set; }

        [ForeignKey("Building")]
        public int Building_Id { get; set; }
        
        [Required]
        [StringLength(15, ErrorMessage = "Max 15 characters")]
        public string Post_Name { get; set; }
        
        [Required]
        [StringLength(15, ErrorMessage = "Max 15 characters")]
        public string Post_Priority { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Post_Creation_DateTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Post_Update_DateTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Post_LifeTime_DateTime { get; set; }

        [Required]
        public string Text { get; set; }

        public string FIle_URL { get; set; }

    }
}
