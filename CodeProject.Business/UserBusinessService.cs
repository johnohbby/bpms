using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;
using CodeProject.Interfaces;
using CodeProject.Business.Common;

using FluentValidation;
using FluentValidation.Results;
using System.Configuration;
using System.Web;
using System.IO;
using System.Net.Mail;

namespace CodeProject.Business
{
    public class UserBusinessService
    {
        private IUserDataService _userDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserBusinessService(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public User CreateUser(User user, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                UserBusinessRules userBusinessRules = new UserBusinessRules();
                ValidationResult results = userBusinessRules.Validate(user);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return user;
                }

                _userDataService.CreateSession();
                _userDataService.BeginTransaction();
                _userDataService.CreateUser(user);
                _userDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("User successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _userDataService.CloseSession();
            }

            return user;


        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void UpdateUser(User user, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                UserBusinessRules userBusinessRules = new UserBusinessRules();
                ValidationResult results = userBusinessRules.Validate(user);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _userDataService.CreateSession();
                _userDataService.BeginTransaction();

                User existingUser = _userDataService.GetUser(user.Id);

                existingUser.Id = user.Id;
                existingUser.Name = user.Name;
                existingUser.Password = user.Password;
                existingUser.Surname = user.Surname;
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                existingUser.IsActive = user.IsActive;

                _userDataService.UpdateUser(user);
                _userDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("User was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _userDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<User> GetUsers(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<User> users = new List<User>();

            try
            {
                int totalRows;

                _userDataService.CreateSession();
                users = _userDataService.GetUsers(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _userDataService.CloseSession();

                transaction.TotalPages = CodeProject.Business.Common.Utilities.CalculateTotalPages(totalRows, pageSize);
                transaction.TotalRows = totalRows;

                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _userDataService.CloseSession();
            }

            return users;

        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public User GetUser(long id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            User user = new User();

            try
            {

                _userDataService.CreateSession();
                user = _userDataService.GetUser(id);
                _userDataService.CloseSession();      
                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _userDataService.CloseSession();
            }

            return user;

        }
        public User GetUserByCredientials(string username, string password, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            User user = new User();

            try
            {

                _userDataService.CreateSession();
                user = _userDataService.GetUserByCredientials(username, password);
                _userDataService.CloseSession();
                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _userDataService.CloseSession();
            }

            return user;

        }

        /// Delete User
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void DeleteUser(User user, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                UserBusinessRules userBusinessRules = new UserBusinessRules();
                ValidationResult results = userBusinessRules.Validate(user);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _userDataService.CreateSession();
                _userDataService.BeginTransaction();

                _userDataService.DeleteUser(user);
                _userDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("User was successfully deleted.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _userDataService.CloseSession();
            }


        }

        public List<User> GetUsersForFolderShare(long folderId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<User> users = new List<User>();

            try
            {
                int totalRows;

                _userDataService.CreateSession();
                users = _userDataService.GetUsersForFolderShare(folderId, currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _userDataService.CloseSession();

                transaction.TotalPages = CodeProject.Business.Common.Utilities.CalculateTotalPages(totalRows, pageSize);
                transaction.TotalRows = totalRows;

                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _userDataService.CloseSession();
            }

            return users;
        }


        public List<User> GetUsersSharedFolder(long folderId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<User> users = new List<User>();

            try
            {
                int totalRows;

                _userDataService.CreateSession();
                users = _userDataService.GetUsersSharedFolder(folderId, currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _userDataService.CloseSession();

                transaction.TotalPages = CodeProject.Business.Common.Utilities.CalculateTotalPages(totalRows, pageSize);
                transaction.TotalRows = totalRows;

                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _userDataService.CloseSession();
            }

            return users;
        }



        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            User user = new User();

            try
            {

                _userDataService.CreateSession();
                _userDataService.BeginTransaction();
                _userDataService.CommitTransaction(true);
                _userDataService.CloseSession();

                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _userDataService.CloseSession();
            }           

        }




    }
}
