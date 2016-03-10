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
    public interface IGroupTypeDataService : IDataRepository, IDisposable
    {
        void CreateGroupType(GroupType groupType);
        void UpdateGroupType(GroupType groupType);
        GroupType GetGroupType(long groupTypeID);
        List<GroupType> GetGroupTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);

    }
}

