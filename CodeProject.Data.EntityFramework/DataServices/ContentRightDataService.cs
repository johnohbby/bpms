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
    /// Content Right Data Service
    /// </summary>
    public class ContentRightDataService : EntityFrameworkService, IContentRightDataService
    {

        /// <summary>
        /// Update Content Right
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateContentRight(ContentRight contentRight)
        {

        }

        /// <summary>
        /// Create Content Right
        /// </summary>
        /// <param name="customer"></param>
        public void CreateContentRight(ContentRight contentRight)
        {
            dbConnection.ContentRights.Add(contentRight);
        }

        /// <summary>
        /// Get Content Right
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public ContentRight GetContentRight(long contentRightID)
        {
            ContentRight contentRight = dbConnection.ContentRights.Where(a => a.Id == contentRightID).FirstOrDefault();
            return contentRight;
        }

        /// <summary>
        /// Get Content Rights
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<ContentRight> GetContentRights(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<ContentRight> contentRights = dbConnection.ContentRights.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return contentRights;
        }

        /// <summary>
        /// Delete Content Right
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteContentRight(ContentRight contentRight)
        {
            dbConnection.ContentRights.Attach(contentRight);
            dbConnection.ContentRights.Remove(contentRight);
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

