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

