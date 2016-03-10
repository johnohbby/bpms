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
    public class GroupBusinessService
    {
        private IGroupDataService _groupDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public GroupBusinessService(IGroupDataService groupDataService)
        {
            _groupDataService = groupDataService;
        }

        /// <summary>
        /// Create Group
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Group CreateGroup(Group group, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                GroupBusinessRules groupBusinessRules = new GroupBusinessRules();
                ValidationResult results = groupBusinessRules.Validate(group);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return group;
                }

                _groupDataService.CreateSession();
                _groupDataService.BeginTransaction();
                _groupDataService.CreateGroup(group);
                _groupDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Group successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _groupDataService.CloseSession();
            }

            return group;


        }

        /// <summary>
        /// Update Group
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void UpdateGroup(Group group, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                GroupBusinessRules groupBusinessRules = new GroupBusinessRules();
                ValidationResult results = groupBusinessRules.Validate(group);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _groupDataService.CreateSession();
                _groupDataService.BeginTransaction();

                Group existingGroup = _groupDataService.GetGroup(group.Id);

                existingGroup.Id = group.Id;
                existingGroup.Name = group.Name;

                _groupDataService.UpdateGroup(group);
                _groupDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Group was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _groupDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Group
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<Group> GetGroups(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<Group> groups = new List<Group>();

            try
            {
                int totalRows;

                _groupDataService.CreateSession();
                groups = _groupDataService.GetGroups(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _groupDataService.CloseSession();

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
                _groupDataService.CloseSession();
            }

            return groups;

        }

        /// <summary>
        /// Get Group
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Group GetGroup(int id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            Group group = new Group();

            try
            {

                _groupDataService.CreateSession();
                group = _groupDataService.GetGroup(id);
                _groupDataService.CloseSession();      
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
                _groupDataService.CloseSession();
            }

            return group;

        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            Group group = new Group();

            try
            {

                _groupDataService.CreateSession();
                _groupDataService.BeginTransaction();
                _groupDataService.CommitTransaction(true);
                _groupDataService.CloseSession();

                _groupDataService.CreateSession();
                _groupDataService.BeginTransaction();
                _groupDataService.CommitTransaction(true);
                _groupDataService.CloseSession();

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
                _groupDataService.CloseSession();
            }           

        }




    }
}
