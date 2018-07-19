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

        #region DbSets
        public virtual DbSet<Account> Accounts { get; set; }
        
        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<Tenant> Tenants { get; set; }

        public virtual DbSet<Building> Buildings { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<Cleaning_Plan> Cleaning_Plans { get; set; }

        public virtual DbSet<Post> Posts { get; set; }
        
        public virtual DbSet<Handles> Handleses { get; set; }

        public virtual DbSet<Issued_Invoice> Issued_Invoices { get; set; }

        public virtual DbSet<Created_Cleaning_Plan> Created_Cleaning_Plans { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }
        #endregion


        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Unique Collumns
            //Admin unique collumns
            modelBuilder.Entity<Admin>().HasIndex(adm => adm.Email).IsUnique(true);

            modelBuilder.Entity<Admin>().HasIndex(adm => adm.JMBG).IsUnique(true);

            modelBuilder.Entity<Admin>().HasIndex(adm => adm.UserName).IsUnique(true);


            //Tenant unique collumns
            modelBuilder.Entity<Tenant>().HasIndex(ten => ten.Email).IsUnique(true);

            modelBuilder.Entity<Tenant>().HasIndex(ten => ten.JMBG).IsUnique(true);

            modelBuilder.Entity<Tenant>().HasIndex(ten => ten.Building_Id).IsUnique(false);

            modelBuilder.Entity<Tenant>().HasIndex(ten => ten.UserName).IsUnique(true);


            //Building unique collumns
            modelBuilder.Entity<Building>().HasIndex(bld => bld.Address).IsUnique(true);
            

            //Invoice unique collumns
            modelBuilder.Entity<Invoice>().HasIndex(inv => inv.Invoice_Type).IsUnique(true);

            modelBuilder.Entity<Invoice>().HasIndex(inv => inv.Invoice_Amount).IsUnique(true);
            

            //Cleaning_Plan unique collumns
            modelBuilder.Entity<Cleaning_Plan>().HasIndex(cp => cp.Cleaning_Type).IsUnique(true);

            modelBuilder.Entity<Cleaning_Plan>().HasIndex(cp => cp.Cleaning_Price).IsUnique(true);
            #endregion

            #region Composite Keys
            modelBuilder.Entity<Created_Cleaning_Plan>().HasKey(ccp => new { ccp.Cleaning_Plan_Id, ccp.Building_Id, ccp.Admin_Id });

            modelBuilder.Entity<Issued_Invoice>().HasKey(iiv => new { iiv.Invoice_Id, iiv.Tenant_Id, iiv.Admin_Id, iiv.Building_Id });

            modelBuilder.Entity<Handles>().HasKey(han => new { han.Admin_Id, han.Building_Id });
            #endregion
            
            #region Relationships
            //Handles
            modelBuilder.Entity<Handles>().HasOne(h => h.Admin).WithMany(h => h.Handleses).HasForeignKey(h => h.Admin_Id);

            modelBuilder.Entity<Handles>().HasOne(h => h.Building).WithMany(h => h.Handleses).HasForeignKey(h => h.Building_Id);

            //Created_Cleaning_Plan
            modelBuilder.Entity<Created_Cleaning_Plan>().HasOne(ccp => ccp.Cleaning_Plan).WithMany(ccp => ccp.Created_Cleaning_Plans).HasForeignKey(ccp => ccp.Cleaning_Plan_Id);

            modelBuilder.Entity<Created_Cleaning_Plan>().HasOne(ccp => ccp.Building).WithMany(ccp => ccp.Created_Cleaning_Plans).HasForeignKey(ccp => ccp.Building_Id);

            modelBuilder.Entity<Created_Cleaning_Plan>().HasOne(ccp => ccp.Admin).WithMany(ccp => ccp.Created_Cleaning_Plans).HasForeignKey(ccp => ccp.Admin_Id);

            //Issued_Invoice
            modelBuilder.Entity<Issued_Invoice>().HasOne(ii => ii.Invoice).WithMany(ii => ii.Issued_Invoices).HasForeignKey(ii => ii.Invoice_Id);

            modelBuilder.Entity<Issued_Invoice>().HasOne(ii => ii.Tenant).WithMany(ii => ii.Issued_Invoices).HasForeignKey(ii => ii.Tenant_Id);

            modelBuilder.Entity<Issued_Invoice>().HasOne(ii => ii.Admin).WithMany(ii => ii.Issued_Invoices).HasForeignKey(ii => ii.Admin_Id);

            modelBuilder.Entity<Issued_Invoice>().HasOne(ii => ii.Building).WithMany(ii => ii.Issued_Invoices).HasForeignKey(ii => ii.Building_Id);

            //Message
            modelBuilder.Entity<Message>().HasOne(ii => ii.Admin).WithMany(ii => ii.Messages).HasForeignKey(ii => ii.Admin_Id);

            modelBuilder.Entity<Message>().HasOne(ii => ii.Tenant).WithMany(ii => ii.Messages).HasForeignKey(ii => ii.Tenant_Id);

            //Post
            modelBuilder.Entity<Post>().HasOne(p => p.Admin).WithMany(p => p.Posts).HasForeignKey(p => p.Admin_Id);

            modelBuilder.Entity<Post>().HasOne(p => p.Building).WithMany(p => p.Posts).HasForeignKey(p => p.Building_Id);
            #endregion
        }

    }
}
