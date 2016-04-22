using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    /// Action Type Data Service
    /// </summary>
    public interface IFolderDataService : IDataRepository, IDisposable
    {
        void CreateFolder(Folder folder, long userId);
        Folder GetFolder(long folderID);
        void DeleteFolder(Folder folder);
        List<Folder> GetFolders(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        List<Folder> GetFoldersForUser(long userId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);  
    }
}

