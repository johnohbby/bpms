using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;
using System.Data.Entity;

namespace CodeProject.Data.EntityFramework
{
    /// <summary>
    /// CodeProject Entity Framework Database Context
    /// </summary>
    public class CodeProjectDatabase : DbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<user> User { get; set; }
        public DbSet<CodeProject.Business.Entities.Action> Actions { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }
      
        /// <summary>
        /// Model Creation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().ToTable("dbo.Customers");
            modelBuilder.Entity<Product>().ToTable("dbo.Products");
            modelBuilder.Entity<user>().ToTable("dbo.User");
            modelBuilder.Entity<CodeProject.Business.Entities.Action>().ToTable("dbo.Actions");
            modelBuilder.Entity<ActionType>().ToTable("dbo.ActionTypes");
         


        }
    }
}
