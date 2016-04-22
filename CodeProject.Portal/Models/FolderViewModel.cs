using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class FolderViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentFolderId { get; set; }
        public long UserId { get; set; }

        public List<Folder> Folders { get; set; }
    }
}