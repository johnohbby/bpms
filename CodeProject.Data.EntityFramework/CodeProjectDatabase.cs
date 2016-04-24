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
        public DbSet<User> Users { get; set; }
        public DbSet<CodeProject.Business.Entities.Action> Actions { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<ContentRight> ContentRights { get; set; }
        public DbSet<ContentType> ContentTypes { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupType> GroupTypes { get; set; }
        public DbSet<RightType> RightTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusTranslation> StatusTranslations { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<WorkflowType> WorkflowTypes { get; set; }
        public DbSet<WorkflowFolder> WorkflowFolders { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormField> FormFields { get; set; }
        public DbSet<ContentFormMap> ContentFormMaps { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<MailTemplate> MailTemplates { get; set; }
        public DbSet<MailTemplateMap> MailTemplateMaps { get; set; }

        /// <summary>
        /// Model Creation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().ToTable("dbo.Customers");
            modelBuilder.Entity<Product>().ToTable("dbo.Products");
            modelBuilder.Entity<User>().ToTable("dbo.Users");
            modelBuilder.Entity<CodeProject.Business.Entities.Action>().Ignore(p => p.DelegatedName).Ignore(p => p.ActionTypeName).Ignore(p => p.CreatedByName).Ignore(p => p.OrdinalNo).Ignore(p => p.Forms).Ignore(p => p.HasDocuments).ToTable("dbo.Actions");
            modelBuilder.Entity<ActionType>().ToTable("dbo.ActionTypes");
            modelBuilder.Entity<ContentRight>().ToTable("dbo.ContentRights");
            modelBuilder.Entity<ContentType>().ToTable("dbo.ContentTypes");
            modelBuilder.Entity<Group>().ToTable("dbo.Groups");
            modelBuilder.Entity<GroupType>().ToTable("dbo.GroupTypes");
            modelBuilder.Entity<RightType>().ToTable("dbo.RightTypes");
            modelBuilder.Entity<Status>().ToTable("dbo.Statuses");
            modelBuilder.Entity<StatusTranslation>().ToTable("dbo.StatusTranslations");
            modelBuilder.Entity<UserGroup>().ToTable("dbo.UserGroups");
            modelBuilder.Entity<Workflow>().ToTable("dbo.Workflows");
            modelBuilder.Entity<WorkflowType>().ToTable("dbo.WorkflowTypes");
            modelBuilder.Entity<Form>().ToTable("dbo.Forms");
            modelBuilder.Entity<FormField>().ToTable("dbo.FormFields");
            modelBuilder.Entity<ContentFormMap>().ToTable("dbo.ContentFormMaps");
            modelBuilder.Entity<Folder>().ToTable("dbo.Folders");
            modelBuilder.Entity<Document>().ToTable("dbo.Documents");
            modelBuilder.Entity<MailTemplate>().ToTable("dbo.MailTemplates");
            modelBuilder.Entity<MailTemplateMap>().ToTable("dbo.MailTemplateMaps");

        }
    }
}
