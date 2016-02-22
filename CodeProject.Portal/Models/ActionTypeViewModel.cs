﻿using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class ActionTypeViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long StatusTranslationId { get; set; }
        public long WorkflowTypeId { get; set; }

        public List<CodeProject.Business.Entities.Action> Actions { get; set; }
        public StatusTranslation StatusTranslation { get; set; }
        public WorkflowType WorkflowType { get; set; }
    }
}