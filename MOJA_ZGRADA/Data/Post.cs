using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

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

        public int Post_Number_Of_Views { get; set; }

        public bool Post_Pinned { get; set; } = false;
        
        [DataType(DataType.Date)]
        public DateTime Post_Creation_DateTime { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Post_Update_DateTime { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Post_LifeTime_DateTime { get; set; }
        
        public string Post_Text { get; set; }

        public string Post_PDF_URL { get; set; }

        public string Post_PDF_FileName { get; set; }

        public string Post_Picture_URL { get; set; }

        public string Post_Picture_FileName { get; set; }

        [NotMapped]
        public virtual IFormFile Post_Image { get; set; }

        [NotMapped]
        public virtual IFormFile Post_PDF { get; set; }



        [JsonIgnore]
        public virtual Admin Admin { get; set; }

        [JsonIgnore]
        public virtual Building Building { get; set; }

    }
}
