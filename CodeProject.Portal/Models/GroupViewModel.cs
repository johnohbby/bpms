using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class GroupViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public long GroupTypeId { get; set; }

        public List<ContentRight> ContentRights { get; set; }
        public  GroupType GroupType { get; set; }
        public List<UserGroup> UserGroups { get; set; }
        public List<Group> Groups { get; set; }
    }
}