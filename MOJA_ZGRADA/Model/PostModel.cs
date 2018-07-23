using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Model
{
    public class PostModel
    {
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

        public string Post_Picture_URL { get; set; }

        public string Post_Picture_FileName { get; set; }

        public virtual IFormFile Post_Image { get; set; }

        public virtual IFormFile Post_PDF { get; set; }
    }
}
