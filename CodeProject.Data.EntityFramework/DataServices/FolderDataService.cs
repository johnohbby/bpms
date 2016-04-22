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
    public class FolderDataService : EntityFrameworkService, IFolderDataService
    {

        /// <summary>
        /// Create Folder
        /// </summary>
        /// <param name="customer"></param>
        public void CreateFolder(Folder folder, long userId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CreateFolder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = folder.Id;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = folder.Name;
                    cmd.Parameters.Add("@parentFolderId", SqlDbType.BigInt).Value = folder.ParentFolderId;
                    cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = userId;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get Folder
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Folder GetFolder(long folderID)
        {
            Folder folder = dbConnection.Folders.Where(a => a.Id == folderID).FirstOrDefault();
            return folder;
        }

        /// <summary>
        /// Get Folders
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<Folder> GetFolders(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {

            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Folders.Count();

            List<Folder> folders = dbConnection.Folders.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return folders;
        }

        /// <summary>
        /// Delete Folder
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteFolder(Folder folder)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteFolder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = folder.Id;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Folder> GetFoldersForUser(long userId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetFoldersForUser", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = userId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<Folder> folders = dt.ToList<Folder>().ToList<Folder>();
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Folders.Count();

            return folders;
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

