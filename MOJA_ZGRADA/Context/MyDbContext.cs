using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.ComponentModel.DataAnnotations;
using MOJA_ZGRADA.Data;

namespace MOJA_ZGRADA.Models
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions options) : base(options) { }


        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Building> Buildings { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<Cleaning_Plan> Cleaning_Plans { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Admin_Role> Admin_Roles { get; set; }

        public virtual DbSet<Role_Of_Admin> Role_Of_Admins { get; set; }

        public virtual DbSet<Handles> Handleses { get; set; }

        public virtual DbSet<Issued_Invoice> Issued_Invoices { get; set; }

        public virtual DbSet<Created_Cleaning_Plan> Created_Cleaning_Plans { get; set; }

        public virtual DbSet<Lives_In> Lives_Ins { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }

        

    }
}
