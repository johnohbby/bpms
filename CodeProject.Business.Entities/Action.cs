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


    public class Action
    {

        public long Id { get; set; }
        public long ActionTypeId { get; set; }
        public long WorkflowId { get; set; }
        public System.DateTime Created { get; set; }
        public long CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public long DelegatedTo { get; set; }
        public String DelegatedName { get; set; }
        public String ActionTypeName { get; set; }
        public String CreatedByName { get; set; }
        public String OrdinalNo { get; set; }

        

    }
}
