using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.ComponentModel.DataAnnotations;
using MOJA_ZGRADA.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MOJA_ZGRADA.Context;

namespace MOJA_ZGRADA.Context
{
    public class MyDbContext : IdentityDbContext<Account, MyRoleManager, string>
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }


        public DbSet<Account> Accounts { get; set; }


        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<Tenant> Tenants { get; set; }

        public virtual DbSet<Building> Buildings { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<Cleaning_Plan> Cleaning_Plans { get; set; }

        public virtual DbSet<Post> Posts { get; set; }
        
        public virtual DbSet<Handles> Handleses { get; set; }

        public virtual DbSet<Issued_Invoice> Issued_Invoices { get; set; }

        public virtual DbSet<Created_Cleaning_Plan> Created_Cleaning_Plans { get; set; }

        public virtual DbSet<Lives_In> Lives_Ins { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasAlternateKey(c => new { c.Email, c.JMBG });


        }

    }
}
