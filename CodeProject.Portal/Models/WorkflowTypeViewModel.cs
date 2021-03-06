﻿using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class WorkflowTypeViewModel :  TransactionalInformation
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public List<WorkflowType> WorkflowTypes { get; set; }
    }
}