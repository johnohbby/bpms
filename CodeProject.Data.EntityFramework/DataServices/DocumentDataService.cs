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
using System.Data;
using System.Configuration;

namespace CodeProject.Data.EntityFramework
{
    /// <summary>
    /// Account Data Service
    /// </summary>
    public class DocumentDataService : EntityFrameworkService, IDocumentDataService
    {

        /// <summary>
        /// Create Document
        /// </summary>
        /// <param name="customer"></param>
        public long CreateDocument(Document document)
        {
            //dbConnection.Workflows.Add(workflow);
            long retunvalue = -1;
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                sqlcon.Open();      
                using (SqlCommand cmd = new SqlCommand("dbo.Document_Create", sqlcon))
                {
                    if (document.ContentId == null) document.ContentId = -1;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 255).Value = document.Name;
                    cmd.Parameters.Add("@nameOnServer", SqlDbType.NVarChar, 255).Value = document.NameOnServer;
                    cmd.Parameters.Add("@extension", SqlDbType.NVarChar, 255).Value = document.Extension;
                    cmd.Parameters.Add("@contentId", SqlDbType.BigInt).Value = document.ContentId;
                    cmd.Parameters.Add("@parentDocumentId", SqlDbType.BigInt).Value = document.ParentDocumentId;
                    cmd.Parameters.Add("@contentTypeName", SqlDbType.NVarChar, 255).Value = document.ContentTypeName;
                    

                    SqlParameter pvNewId = new SqlParameter();
                    pvNewId.ParameterName = "@insertedId";
                    pvNewId.DbType = DbType.Int64;
                    pvNewId.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pvNewId);
                    cmd.ExecuteNonQuery();
                    retunvalue = (long)cmd.Parameters["@insertedId"].Value;
                }

                sqlcon.Close();
            }

            return retunvalue;
        }

        /// <summary>
        /// Get Document
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Document GetDocument(long documentID)
        {
            Document Document = dbConnection.Documents.Where(a => a.Id == documentID).FirstOrDefault();
            return Document;
        }

        /// <summary>
        /// Get Documents
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<Document> GetDocuments(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {

            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<Document> documents = dbConnection.Documents.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return documents;
        }

        /// <summary>
        /// Delete Document
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteDocument(Document document)
        {
            dbConnection.Documents.Attach(document);
            dbConnection.Documents.Remove(document);
        }

        public List<Document> GetDocumentsForContent(long contentTypeId, long contentId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDocumentsForContent", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@contentId", SqlDbType.BigInt).Value = contentId;
                    cmd.Parameters.Add("@contentTypeId", SqlDbType.BigInt).Value = contentTypeId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<Document> documents = dt.ToList<Document>().ToList<Document>();
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Documents.Count();

            return documents;
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

