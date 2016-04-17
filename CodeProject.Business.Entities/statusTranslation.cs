
namespace CodeProject.Business.Entities
{
    using System;
    using System.Collections.Generic;

    public class StatusTranslation
    {
        
        public long Id { get; set; }
        public long StatusIdFrom { get; set; }
        public long StatusIdTo { get; set; }
        public long actionTypeId { get; set; }
 
    }
}
