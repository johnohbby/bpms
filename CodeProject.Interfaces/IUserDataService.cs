using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    ///User Data Service
    /// </summary>
    public interface IUserDataService : IDataRepository, IDisposable
    {
        void CreateUser(User user);
        void UpdateUser(User user);
        User GetUser(long userID);
        User GetUserByCredientials(string username, string password);
        List<User> GetUsers(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        void DeleteUser(User user);
          
    }
}

