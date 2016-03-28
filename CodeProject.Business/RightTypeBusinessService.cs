using System;
using System.Collections.Generic;
using CodeProject.Business.Entities;
using CodeProject.Interfaces;
using CodeProject.Business.Common;
using FluentValidation.Results;

namespace CodeProject.Business
{
    public class RightTypeBusinessService
    {
        private IRightTypeDataService _rightTypeDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public RightTypeBusinessService(IRightTypeDataService rightTypeDataService)
        {
            _rightTypeDataService = rightTypeDataService;
        }

        /// <summary>
        /// Create Right Type
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public RightType CreateRightType(RightType rightType, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                RightTypeBusinessRules rightTypeBusinessRules = new RightTypeBusinessRules();
                ValidationResult results = rightTypeBusinessRules.Validate(rightType);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return rightType;
                }

                _rightTypeDataService.CreateSession();
                _rightTypeDataService.BeginTransaction();
                _rightTypeDataService.CreateRightType(rightType);
                _rightTypeDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Right type successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _rightTypeDataService.CloseSession();
            }

            return rightType;


        }

        /// <summary>
        /// Update Right type
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void UpdateRightType(RightType rightType, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                RightTypeBusinessRules rightTypeBusinessRules = new RightTypeBusinessRules();
                ValidationResult results = rightTypeBusinessRules.Validate(rightType);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _rightTypeDataService.CreateSession();
                _rightTypeDataService.BeginTransaction();

                RightType existingRightType = _rightTypeDataService.GetRightType(rightType.Id);

                existingRightType.Id = rightType.Id;
                existingRightType.Code = rightType.Code;
                existingRightType.Name = rightType.Name;
                existingRightType.Description = rightType.Description;


                _rightTypeDataService.UpdateRightType(rightType);
                _rightTypeDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Right type was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _rightTypeDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Right types
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<RightType> GetRightTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<RightType> rightType = new List<RightType>();

            try
            {
                int totalRows;

                _rightTypeDataService.CreateSession();
                rightType = _rightTypeDataService.GetRightTypes(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _rightTypeDataService.CloseSession();

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
                _rightTypeDataService.CloseSession();
            }

            return rightType;

        }


        /// <summary>
        /// Get Right Type
        /// </summary>
        /// <param name="workflowTypeId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public RightType GetRightType(long rightTypeId, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            RightType rightType = new RightType();

            try
            {

                _rightTypeDataService.CreateSession();
                rightType = _rightTypeDataService.GetRightType(rightTypeId);
                _rightTypeDataService.CloseSession();      
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
                _rightTypeDataService.CloseSession();
            }

            return rightType;

        }


        /// <summary>
        /// Delete Right type
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void DeleteRightType(RightType rightType, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                RightTypeBusinessRules rightTypeBusinessRules = new RightTypeBusinessRules();
                ValidationResult results = rightTypeBusinessRules.Validate(rightType);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _rightTypeDataService.CreateSession();
                _rightTypeDataService.BeginTransaction();

                _rightTypeDataService.DeleteRightType(rightType);
                _rightTypeDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Right type was successfully deleted.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _rightTypeDataService.CloseSession();
            }


        }



    }
}

