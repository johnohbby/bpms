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
    public class UserGroupBusinessService
    {
        private IUserGroupDataService _userGroupDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserGroupBusinessService(IUserGroupDataService userGroupDataService)
        {
            _userGroupDataService = userGroupDataService;
        }

        /// <summary>
        /// Create User Group
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public UserGroup CreateUserGroup(UserGroup userGroup, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                UserGroupBusinessRules userGroupBusinessRules = new UserGroupBusinessRules();
                ValidationResult results = userGroupBusinessRules.Validate(userGroup);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return userGroup;
                }

                _userGroupDataService.CreateSession();
                _userGroupDataService.BeginTransaction();
                _userGroupDataService.CreateUserGroup(userGroup);
                _userGroupDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("User Group successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _userGroupDataService.CloseSession();
            }

            return userGroup;


        }

        /// <summary>
        /// Update User Group
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void UpdateUserGroup(UserGroup userGroup, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                UserGroupBusinessRules userGroupBusinessRules = new UserGroupBusinessRules();
                ValidationResult results = userGroupBusinessRules.Validate(userGroup);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _userGroupDataService.CreateSession();
                _userGroupDataService.BeginTransaction();

                UserGroup existingUserGroup = _userGroupDataService.GetUserGroup(userGroup.Id);

                existingUserGroup.Id = userGroup.Id;
                existingUserGroup.UserId = userGroup.UserId;
                existingUserGroup.GroupId = userGroup.GroupId;

                _userGroupDataService.UpdateUserGroup(userGroup);
                _userGroupDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("User Group was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _userGroupDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get User Groups
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<UserGroup> GetUserGroups(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<UserGroup> userGroups = new List<UserGroup>();

            try
            {
                int totalRows;

                _userGroupDataService.CreateSession();
                userGroups = _userGroupDataService.GetUserGroups(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _userGroupDataService.CloseSession();

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
                _userGroupDataService.CloseSession();
            }

            return userGroups;

        }

        /// <summary>
        /// Get User Group
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public UserGroup GetUserGroup(int id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            UserGroup userGroup = new UserGroup();

            try
            {

                _userGroupDataService.CreateSession();
                userGroup = _userGroupDataService.GetUserGroup(id);
                _userGroupDataService.CloseSession();      
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
                _userGroupDataService.CloseSession();
            }

            return userGroup;

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

                _userGroupDataService.CreateSession();
                _userGroupDataService.BeginTransaction();
                _userGroupDataService.CommitTransaction(true);
                _userGroupDataService.CloseSession();

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
                _userGroupDataService.CloseSession();
            }           

        }




    }
}
