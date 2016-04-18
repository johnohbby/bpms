
namespace CodeProject.Business.Entities
{
    using System;
    using System.Collections.Generic;


    public class ActionType
    {
       
        public long Id { get; set; }
        public string Name { get; set; }
        public long WorkflowTypeId { get; set; }
    
    }
}
