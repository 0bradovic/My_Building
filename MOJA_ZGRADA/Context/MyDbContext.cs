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


        //Fluent API
        //Initializing unique collumns, composite keys
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Admin unique collumns
            modelBuilder.Entity<Admin>().HasIndex(adm => adm.Email).IsUnique(true);

            modelBuilder.Entity<Admin>().HasIndex(adm => adm.JMBG).IsUnique(true);


            //Tenant unique collumns
            modelBuilder.Entity<Tenant>().HasIndex(ten => ten.Email).IsUnique(true);

            modelBuilder.Entity<Tenant>().HasIndex(ten => ten.JMBG).IsUnique(true);
            
            

            //Building unique collumns
            modelBuilder.Entity<Building>().HasIndex(bld => bld.Address).IsUnique(true);
            

            //Invoice unique collumns
            modelBuilder.Entity<Invoice>().HasIndex(inv => inv.Invoice_Type).IsUnique(true);

            modelBuilder.Entity<Invoice>().HasIndex(inv => inv.Invoice_Amount).IsUnique(true);
            

            //Cleaning_Plan  unique collumns
            modelBuilder.Entity<Cleaning_Plan>().HasIndex(cp => cp.Cleaning_Type).IsUnique(true);

            modelBuilder.Entity<Cleaning_Plan>().HasIndex(cp => cp.Cleaning_Price).IsUnique(true);
            
            
            //Composite Keys
            modelBuilder.Entity<Created_Cleaning_Plan>().HasKey(ccp => new { ccp.Cleaning_Plan_Id, ccp.Building_Id, ccp.Admin_Id });

            modelBuilder.Entity<Issued_Invoice>().HasKey(iiv => new { iiv.Invoice_Id, iiv.Tenant_Id, iiv.Admin_Id, iiv.Building_Id });

            modelBuilder.Entity<Handles>().HasKey(han => new { han.Admin_Id, han.Building_Id });

            modelBuilder.Entity<Lives_In>().HasKey(lin => new { lin.Tenant_Id, lin.Building_Id });


        }

    }
}
