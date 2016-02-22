
namespace CodeProject.Business.Entities
{
    using System;
    using System.Collections.Generic;

 
    public class ContentRight
    {
     
        public long Id { get; set; }
        public long GroupId { get; set; }
        public long ContentTypeId { get; set; }
        public long ContentId { get; set; }
        public long RightId { get; set; }
    
        public virtual contentType contentType { get; set; }
        public virtual Group group { get; set; }
        public virtual RightType rightType { get; set; }
    }
}
