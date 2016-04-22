using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class UserViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Created { get; set; }
        public String CreatedBy { get; set; }

        public List<CodeProject.Business.Entities.Action> Actions { get; set; }
        public List<UserGroup> UserGroups { get; set; }
        public List<Workflow> Workflows { get; set; }
        public List<User> Users { get; set; }
        public long FolderId { get; set; }

        
    }
}