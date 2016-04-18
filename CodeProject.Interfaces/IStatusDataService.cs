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
    public interface IStatusDataService : IDataRepository, IDisposable
    {
        void CreateStatus(Status status);
        void UpdateStatus(Status status);
        void DeleteStatus(Status status);
        Status GetStatus(long statusID);
        List<Status> GetStatuses(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
          
    }
}

