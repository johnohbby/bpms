using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class StatusTranslationViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public long StatusIdFrom { get; set; }
        public long StatusIdTo { get; set; }

        public virtual List<ActionType> ActionTypes { get; set; }
        public virtual Status Status { get; set; }
        public virtual Status Status1 { get; set; }
    }
}