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
    /// Content Type Data Service
    /// </summary>
    public class ContentTypeDataService : EntityFrameworkService, IContentTypeDataService
    {

        /// <summary>
        /// Update Content Type
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateContentType(ContentType contentType)
        {

        }

        /// <summary>
        /// Create Content Type
        /// </summary>
        /// <param name="customer"></param>
        public void CreateContentType(ContentType contentType)
        {
            dbConnection.ContentTypes.Add(contentType);
        }

        /// <summary>
        /// Get Content Type
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public ContentType GetContentType(long contentTypeID)
        {
            ContentType contentType = dbConnection.ContentTypes.Where(a => a.Id == contentTypeID).FirstOrDefault();
            return contentType;
        }

        /// <summary>
        /// Get Content Types
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<ContentType> GetContentTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<ContentType> contentTypes = dbConnection.ContentTypes.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return contentTypes;
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

