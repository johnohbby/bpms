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
    public interface IGroupDataService : IDataRepository, IDisposable
    {
        void CreateGroup(Group group);
        void UpdateGroup(Group group);
        void DeleteGroup(Group group);
        Group GetGroup(long groupID);
        List<Group> GetGroups(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);

    }
}

