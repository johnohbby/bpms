using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Interfaces;
using CodeProject.Business.Entities;
using CodeProject.Business.Common;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CodeProject.Data.EntityFramework
{
    /// <summary>
    /// Content Form Map Data Service
    /// </summary>
    public class ContentFormMapDataService : EntityFrameworkService, IContentFormMapDataService
    {

        /// <summary>
        /// Update Content Form Map
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateContentFormMap(ContentFormMap contentFormMap)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ContentFormMapUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = contentFormMap.Id;
                    cmd.Parameters.Add("@formId", SqlDbType.BigInt).Value = contentFormMap.FormId;
                    cmd.Parameters.Add("@contentTypeId", SqlDbType.BigInt).Value = contentFormMap.ContentTypeId;
                    cmd.Parameters.Add("@contentId", SqlDbType.BigInt).Value = contentFormMap.ContentId;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Create Content Form Map
        /// </summary>
        /// <param name="customer"></param>
        public void CreateContentFormMap(ContentFormMap contentFormMap)
        {
            dbConnection.ContentFormMaps.Add(contentFormMap);
        }

        /// <summary>
        /// Get Content Form Map
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public ContentFormMap GetContentFormMap(long contentFormMapID)
        {
            ContentFormMap contentFormMap = dbConnection.ContentFormMaps.Where(a => a.Id == contentFormMapID).FirstOrDefault();
            return contentFormMap;
        }

        /// <summary>
        /// Get Content Form Maps
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<ContentFormMap> GetContentFormMaps(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<ContentFormMap> contentFormMaps = dbConnection.ContentFormMaps.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return contentFormMaps;
        }

        /// Delete Content Form Map
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteContentFormMap(ContentFormMap contentFormMap)
        {
            dbConnection.ContentFormMaps.Attach(contentFormMap);
            dbConnection.ContentFormMaps.Remove(contentFormMap);
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

