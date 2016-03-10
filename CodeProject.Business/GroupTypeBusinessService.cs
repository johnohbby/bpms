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
    public class GroupTypeBusinessService
    {
        private IGroupTypeDataService _groupTypeDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public GroupTypeBusinessService(IGroupTypeDataService groupTypeDataService)
        {
            _groupTypeDataService = groupTypeDataService;
        }

        /// <summary>
        /// Create Group Type
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public GroupType CreateGroupType(GroupType groupType, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                GroupTypeBusinessRules groupTypeBusinessRules = new GroupTypeBusinessRules();
                ValidationResult results = groupTypeBusinessRules.Validate(groupType);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return groupType;
                }

                _groupTypeDataService.CreateSession();
                _groupTypeDataService.BeginTransaction();
                _groupTypeDataService.CreateGroupType(groupType);
                _groupTypeDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Group Type successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _groupTypeDataService.CloseSession();
            }

            return groupType;


        }

        /// <summary>
        /// Update Action Type
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void UpdateGroupType(GroupType groupType, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                GroupTypeBusinessRules groupTypeBusinessRules = new GroupTypeBusinessRules();
                ValidationResult results = groupTypeBusinessRules.Validate(groupType);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _groupTypeDataService.CreateSession();
                _groupTypeDataService.BeginTransaction();

                GroupType existingGroupType = _groupTypeDataService.GetGroupType(groupType.Id);

                existingGroupType.Id = groupType.Id;
                existingGroupType.Name = groupType.Name;

                _groupTypeDataService.UpdateGroupType(groupType);
                _groupTypeDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Group Type was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _groupTypeDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Group Types
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<GroupType> GetGroupTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<GroupType> groupTypes = new List<GroupType>();

            try
            {
                int totalRows;

                _groupTypeDataService.CreateSession();
                groupTypes = _groupTypeDataService.GetGroupTypes(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _groupTypeDataService.CloseSession();

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
                _groupTypeDataService.CloseSession();
            }

            return groupTypes;

        }

        /// <summary>
        /// Get Group Type
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public GroupType GetGroupType(int id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            GroupType groupType = new GroupType();

            try
            {

                _groupTypeDataService.CreateSession();
                groupType = _groupTypeDataService.GetGroupType(id);
                _groupTypeDataService.CloseSession();      
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
                _groupTypeDataService.CloseSession();
            }

            return groupType;

        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            GroupType groupType = new GroupType();

            try
            {

                _groupTypeDataService.CreateSession();
                _groupTypeDataService.BeginTransaction();
                _groupTypeDataService.CommitTransaction(true);
                _groupTypeDataService.CloseSession();

                _groupTypeDataService.CreateSession();
                _groupTypeDataService.BeginTransaction();
                _groupTypeDataService.CommitTransaction(true);
                _groupTypeDataService.CloseSession();

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
                _groupTypeDataService.CloseSession();
            }           

        }




    }
}
