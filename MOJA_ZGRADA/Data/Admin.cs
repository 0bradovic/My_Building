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
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string First_Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string Last_Name { get; set; }

        [Display(Name = "Date Of Birth"), DataType(DataType.Date)]
        public DateTime Date_Of_Birth { get; set; }

        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone no. is required")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string Phone { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Must have 13 numbers.")]
        public int JMBG { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Max 50 numbers.")]
        public string Address { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_-]{3,15}$", ErrorMessage = "Min 3 Max 15 characters, can contain only characters, numbers and _ or - ")]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_-]{3,15}$", ErrorMessage = "Min 3 Max 15 characters, can contain only characters, numbers and _ or - ")]
        public string Password { get; set; }





    }
}
