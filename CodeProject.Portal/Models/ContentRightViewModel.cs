﻿using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class ContentRightViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public long ContentTypeId { get; set; }
        public long ContentId { get; set; }
        public long RightId { get; set; }

        public ContentType ContentType { get; set; }
        public Group Group { get; set; }
        public RightType RightType { get; set; }
    }
}