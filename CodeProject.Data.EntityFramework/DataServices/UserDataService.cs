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
    /// User Data Service
    /// </summary>
    public class UserDataService : EntityFrameworkService, IUserDataService
    {

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateUser(User user)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UserUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = user.Id;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = user.Name;
                    cmd.Parameters.Add("@surname", SqlDbType.VarChar).Value = user.Surname;
                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = user.Username;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = user.Password;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = user.Email;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = user.IsActive;
                    cmd.Parameters.Add("@created", SqlDbType.Date).Value = user.Created;
                    cmd.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = user.CreatedBy;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="customer"></param>
        public void CreateUser(User user)
        {
            dbConnection.Users.Add(user);
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public User GetUser(long userId)
        {
            User user = dbConnection.Users.Where(a => a.Id == userId).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<User> GetUsers(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<User> users = dbConnection.Users.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return users;
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteUser(User user)
        {
            dbConnection.Users.Attach(user);
            dbConnection.Users.Remove(user);
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

        public User GetUserByCredientials(string username, string password)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UserGetUserByCredientials", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar, 255).Value = password;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<Business.Entities.User> user = dt.ToList<Business.Entities.User>().ToList<Business.Entities.User>();
            if (user.Count > 0) return user[0];
            return null;
            
        }

        public List<User> GetUsersForFolderShare(long folderId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetUsersForFolderShare", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@folderId", SqlDbType.BigInt).Value = folderId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<User> users = dt.ToList<User>().ToList<User>();
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Users.Count();

            return users;
        }

        public List<User> GetUsersSharedFolder(long folderId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetUsersSharedFolder", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@folderId", SqlDbType.BigInt).Value = folderId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<User> users = dt.ToList<User>().ToList<User>();
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Users.Count();

            return users;
        }


    }

}

