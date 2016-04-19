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
    public class GroupDataService : EntityFrameworkService, IGroupDataService
    {

        /// <summary>
        /// Update Group 
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateGroup(Group group)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GroupUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = group.Id;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = group.Name;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = group.Email;
                    cmd.Parameters.Add("@groupTypeId", SqlDbType.VarChar).Value = group.GroupTypeId;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Create Group Type
        /// </summary>
        /// <param name="groupType"></param>
        public void CreateGroup(Group group)
        {
            dbConnection.Groups.Add(group);
        }

        /// <summary>
        /// Get Group
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Group GetGroup(long groupID)
        {
            Group group = dbConnection.Groups.Where(a => a.Id == groupID).FirstOrDefault();
            return group;
        }

        /// <summary>
        /// Get Groups
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<Group> GetGroups(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<Group> groups = dbConnection.Groups.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return groups;
        }

        /// <summary>
        /// Delete Group
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteGroup(Group group)
        {
            dbConnection.Groups.Attach(group);
            dbConnection.Groups.Remove(group);
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

