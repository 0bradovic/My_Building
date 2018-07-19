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
    [Table("tbl_Post")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Admin")]
        public int Admin_Id { get; set; }

        [ForeignKey("Building")]
        public int Building_Id { get; set; }
        
        public string Post_Name { get; set; }
        
        public string Post_Priority { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Post_Creation_DateTime { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Post_Update_DateTime { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Post_LifeTime_DateTime { get; set; }
        
        public string Text { get; set; }

        public string File_URL { get; set; }


        public Admin Admin { get; set; }

        public Building Building { get; set; }

    }
}
