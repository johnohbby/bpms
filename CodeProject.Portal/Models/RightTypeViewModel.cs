﻿using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class RightTypeViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ContentRight> ContentRights { get; set; }
        public List<RightType> RightTypes { get; set; }
    }
}