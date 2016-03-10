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
    public class RightTypeDataService : EntityFrameworkService, IRightTypeDataService
    {

        /// <summary>
        /// Update Right Type
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateRightType(RightType rightType)
        {

        }

        /// <summary>
        /// Create Right Type
        /// </summary>
        /// <param name="groupType"></param>
        public void CreateRightType(RightType rightType)
        {
            dbConnection.RightTypes.Add(rightType);
        }

        /// <summary>
        /// Get Right Type
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public RightType GetRightType(long rightTypeID)
        {
            RightType rightType = dbConnection.RightTypes.Where(a => a.Id == rightTypeID).FirstOrDefault();
            return rightType;
        }

        /// <summary>
        /// Get Right Types
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<RightType> GetRightTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<RightType> rightTypes = dbConnection.RightTypes.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return rightTypes;
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

