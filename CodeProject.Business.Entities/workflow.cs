//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeProject.Business.Entities
{
    using System;
    using System.Collections.Generic;


    public class Workflow
    {
        
        public long Id { get; set; }
        public String Name { get; set; }
        public String CaseNumber { get; set; }
        public long WorkflowTypeId { get; set; }
        public long LastActionId { get; set; }
        public System.DateTime Created { get; set; }
        public long CreatedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
