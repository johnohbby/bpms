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
    /// Status Translation Service
    /// </summary>
    public class StatusTranslationDataService : EntityFrameworkService, IStatusTranslationDataService
    {

        /// <summary>
        /// Update Status Translation
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateStatusTranslation(StatusTranslation statusTranslation)
        {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("statusTranslationUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = statusTranslation.Id;
                    cmd.Parameters.Add("@statusIdFrom", SqlDbType.VarChar).Value = statusTranslation.StatusIdFrom;
                    cmd.Parameters.Add("@statusIdTo", SqlDbType.BigInt).Value = statusTranslation.StatusIdTo;
                    cmd.Parameters.Add("@actionTypeId", SqlDbType.BigInt).Value = statusTranslation.ActionTypeId;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Create Status Translation
        /// </summary>
        /// <param name="customer"></param>
        public void CreateStatusTranslation(StatusTranslation statusTranslation)
        {
            dbConnection.StatusTranslations.Add(statusTranslation);
        }

        /// <summary>
        /// Get Status
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public StatusTranslation GetStatusTranslation(long statusTranslationID)
        {
            StatusTranslation statusTranslation = dbConnection.StatusTranslations.Where(a => a.Id == statusTranslationID).FirstOrDefault();
            return statusTranslation;
        }

        /// <summary>
        /// Get Status Translations
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<StatusTranslation> GetStatusTranslations(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.StatusTranslations.Count();

            List<StatusTranslation> statusTranslations = dbConnection.StatusTranslations.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return statusTranslations;
        }


        /// <summary>
        /// Delete Status Translation
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteStatusTranslation(StatusTranslation statusTranslation)
        {
            dbConnection.StatusTranslations.Attach(statusTranslation);
            dbConnection.StatusTranslations.Remove(statusTranslation);
        }

        
             /// <summary>
        /// Get Status Translations For Action Type
        /// </summary>
        /// <param name="customer"></param>
        public List<StatusTranslation> GetStatusTranslationsForActionType(long actionTypeId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("getStatusTranslationsForActionType", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@actionTypeId", SqlDbType.BigInt).Value = actionTypeId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<StatusTranslation> statusTranslations = dt.ToList<StatusTranslation>().ToList<StatusTranslation>();
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.StatusTranslations.Count();

            return statusTranslations;
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

