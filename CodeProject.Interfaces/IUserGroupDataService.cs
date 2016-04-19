using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    /// User Group Data Service
    /// </summary>
    public interface IUserGroupDataService : IDataRepository, IDisposable
    {
        void CreateUserGroup(UserGroup userGroup);
        void UpdateUserGroup(UserGroup userGroup);
        void DeleteUserGroup(UserGroup userGroup);
        UserGroup GetUserGroup(long userGroupID);
        List<UserGroup> GetUserGroups(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);

    }
}

