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
    public class MailTemplateDataService : EntityFrameworkService, IMailTemplateDataService
    {

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateMailTemplate(MailTemplate mailTemplate)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("MailTemplateUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = mailTemplate.Id;
                    cmd.Parameters.Add("@body", SqlDbType.VarChar).Value = mailTemplate.Body;
                    cmd.Parameters.Add("@subject", SqlDbType.VarChar).Value = mailTemplate.Subject;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }

        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="customer"></param>
        public void CreateMailTemplate(MailTemplate MailTemplate)
        {
            dbConnection.MailTemplates.Add(MailTemplate);
        }

        /// <summary>
        /// Get Action Type
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public MailTemplate GetMailTemplate(long mailTemplateID)
        {
            MailTemplate mailTemplate = dbConnection.MailTemplates.Where(a => a.Id == mailTemplateID).FirstOrDefault();
            return mailTemplate;
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
        public List<MailTemplate> GetMailTemplates(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {

            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.MailTemplates.Count();

            List<MailTemplate> mailTemplates = dbConnection.MailTemplates.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return mailTemplates;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteMailTemplate(MailTemplate mailTemplate)
        {
            dbConnection.MailTemplates.Attach(mailTemplate);
            dbConnection.MailTemplates.Remove(mailTemplate);
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

