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

        public string Post_Name { get; set; }

        public string Post_Priority { get; set; }

        public DateTime Post_Creation_DateTime { get; set; }

        public DateTime Post_Update_DateTime { get; set; }

        public DateTime Post_LifeTime_DateTime { get; set; }

        public string Text { get; set; }

        public string Attachment { get; set; }

    }
}
