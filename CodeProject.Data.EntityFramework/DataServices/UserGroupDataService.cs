using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Interfaces;
using CodeProject.Business.Entities;
using CodeProject.Business.Common;
using System.Linq.Dynamic;

namespace CodeProject.Data.EntityFramework
{
    /// <summary>
    /// User Group Data Service
    /// </summary>
    public class UserGroupDataService : EntityFrameworkService, IUserGroupDataService
    {

        /// <summary>
        /// Update User Group 
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateUserGroup(UserGroup userGroup)
        {

        }

        /// <summary>
        /// Create User Group
        /// </summary>
        /// <param name="groupType"></param>
        public void CreateUserGroup(UserGroup userGroup)
        {
            dbConnection.UserGroups.Add(userGroup);
        }

        /// <summary>
        /// Get Group
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public UserGroup GetUserGroup(long userGroupID)
        {
            UserGroup userGroup = dbConnection.UserGroups.Where(a => a.Id == userGroupID).FirstOrDefault();
            return userGroup;
        }

        /// <summary>
        /// Get User Groups
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<UserGroup> GetUserGroups(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<UserGroup> userGroups = dbConnection.UserGroups.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return userGroups;
        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        public void InitializeData()
        {

        }

        /// <summary>
        /// Load Data
        /// </summary>
        public void LoadData()
        {
        }


    }

}

