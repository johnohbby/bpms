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


    public partial class userGroup
    {
    
        public long id { get; set; }
        public long user_id { get; set; }
        public long group_id { get; set; }
    
        public virtual group group { get; set; }
        public virtual user user { get; set; }
    }
}
