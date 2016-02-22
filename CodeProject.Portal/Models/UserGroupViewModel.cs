using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class UserGroupViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long GroupId { get; set; }

        public Group Group { get; set; }
        public User User { get; set; }
    }
}