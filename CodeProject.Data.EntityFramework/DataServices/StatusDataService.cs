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
    /// Account Data Service
    /// </summary>
    public class StatusDataService : EntityFrameworkService, IStatusDataService
    {

        /// <summary>
        /// Update Status
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateStatus(CodeProject.Business.Entities.Status status)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("statusUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = status.Id;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = status.Name;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Create Status
        /// </summary>
        /// <param name="customer"></param>
        public void CreateStatus(CodeProject.Business.Entities.Status status)
        {
            dbConnection.Statuses.Add(status);
        }

        /// <summary>
        /// Get Status
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public CodeProject.Business.Entities.Status GetStatus(long statusID)
        {
            CodeProject.Business.Entities.Status status = dbConnection.Statuses.Where(a => a.Id == statusID).FirstOrDefault();
            return status;
        }

        /// <summary>
        /// Get Statuses
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<CodeProject.Business.Entities.Status> GetStatuses(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<CodeProject.Business.Entities.Status> statuses = dbConnection.Statuses.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return statuses;
        }


        /// <summary>
        /// Delete Status
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteStatus(Status status)
        {
            dbConnection.Statuses.Attach(status);
            dbConnection.Statuses.Remove(status);
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

