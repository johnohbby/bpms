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
    public class GroupTypeDataService : EntityFrameworkService, IGroupTypeDataService
    {

        /// <summary>
        /// Update Group Type
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateGroupType(GroupType groupType)
        {

        }

        /// <summary>
        /// Create Group Type
        /// </summary>
        /// <param name="groupType"></param>
        public void CreateGroupType(GroupType groupType)
        {
            dbConnection.GroupTypes.Add(groupType);
        }

        /// <summary>
        /// Get Group Type
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public GroupType GetGroupType(long groupTypeID)
        {
            GroupType groupType = dbConnection.GroupTypes.Where(a => a.Id == groupTypeID).FirstOrDefault();
            return groupType;
        }

        /// <summary>
        /// Get Group Types
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<GroupType> GetGroupTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<GroupType> groupTypes = dbConnection.GroupTypes.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return groupTypes;
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

