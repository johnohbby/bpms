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
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Created { get; set; }
        public string Created_by { get; set; }

        public List<CodeProject.Business.Entities.Action> Actions { get; set; }
        public List<UserGroup> UserGroups { get; set; }
        public List<Workflow> Workflows { get; set; }
    }
}