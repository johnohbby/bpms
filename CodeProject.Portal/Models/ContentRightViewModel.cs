using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class ContentRightViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public long? GroupId { get; set; }
        public long ContentTypeId { get; set; }
        public long ContentId { get; set; }
        public long RightTypeId { get; set; }
        public long? UserId { get; set; }

        public ContentType ContentType { get; set; }
        public Group Group { get; set; }
        public RightType RightType { get; set; }
        public string GroupName { get; set; }
        public string ContentTypeName { get; set; }
        public string ContentName { get; set; }
        public string RightName { get; set; }
        public List<ContentRight> ContentRights { get; set; }
    }
}