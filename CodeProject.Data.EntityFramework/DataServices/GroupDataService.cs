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
    /// Account Data Service
    /// </summary>
    public class GroupDataService : EntityFrameworkService, IGroupDataService
    {

        /// <summary>
        /// Update Group 
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateGroup(Group group)
        {

        }

        /// <summary>
        /// Create Group Type
        /// </summary>
        /// <param name="groupType"></param>
        public void CreateGroup(Group group)
        {
            dbConnection.Groups.Add(group);
        }

        /// <summary>
        /// Get Group
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Group GetGroup(long groupID)
        {
            Group group = dbConnection.Groups.Where(a => a.Id == groupID).FirstOrDefault();
            return group;
        }

        /// <summary>
        /// Get Groups
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<Group> GetGroups(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<Group> groups = dbConnection.Groups.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return groups;
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

