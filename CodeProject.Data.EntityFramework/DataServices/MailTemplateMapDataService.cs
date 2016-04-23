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
    public class MailTemplateMapDataService : EntityFrameworkService, IMailTemplateMapDataService
    {

        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="customer"></param>
        public void CreateMailTemplateMap(MailTemplateMap mailTemplateMap)
        {
            dbConnection.MailTemplateMaps.Add(mailTemplateMap);
        }


        /// <summary>
        /// Get Mail Templates
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<MailTemplateMap> GetMapsForMailTemplate(long mailTemplateId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {

            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetMapsForMailTemplate", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@mailTemplateId", SqlDbType.BigInt).Value = mailTemplateId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<MailTemplateMap> mailTemplateMaps = dt.ToList<MailTemplateMap>().ToList<MailTemplateMap>();
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.MailTemplateMaps.Count();

            return mailTemplateMaps;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteMailTemplateMap(MailTemplateMap mailTemplateMap)
        {
            dbConnection.MailTemplateMaps.Attach(mailTemplateMap);
            dbConnection.MailTemplateMaps.Remove(mailTemplateMap);
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

