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
    public class FolderBusinessService
    {
        private IFolderDataService _folderDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public FolderBusinessService(IFolderDataService FolderDataService)
        {
            _folderDataService = FolderDataService;
        }

        /// <summary>
        /// Create Folder
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Folder CreateFolder(Folder folder, long userId, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                FolderBusinessRules folderBusinessRules = new FolderBusinessRules();
                ValidationResult results = folderBusinessRules.Validate(folder);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return folder;
                }

                _folderDataService.CreateSession();
                _folderDataService.BeginTransaction();
                _folderDataService.CreateFolder(folder, userId);
                _folderDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Folder successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _folderDataService.CloseSession();
            }

            return folder;


        }


        /// <summary>
        /// Get Folders
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<Folder> GetFolders(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<Folder> folders = new List<Folder>();

            try
            {
                int totalRows;

                _folderDataService.CreateSession();
                folders = _folderDataService.GetFolders(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _folderDataService.CloseSession();

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
                _folderDataService.CloseSession();
            }

            return folders;

        }

        /// <summary>
        /// Get Folder
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Folder GetFolder(int id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            Folder folder = new Folder();

            try
            {

                _folderDataService.CreateSession();
                folder = _folderDataService.GetFolder(id);
                _folderDataService.CloseSession();      
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
                _folderDataService.CloseSession();
            }

            return folder;

        }

        /// <summary>
        /// Delete Folder
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void DeleteFolder(Folder folder, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                FolderBusinessRules folderBusinessRules = new FolderBusinessRules();
                ValidationResult results = folderBusinessRules.Validate(folder);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _folderDataService.CreateSession();
                _folderDataService.BeginTransaction();

                _folderDataService.DeleteFolder(folder);
                _folderDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Folder was successfully deleted.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _folderDataService.CloseSession();
            }


        }

        
             /// <summary>
        /// Get Folders for User
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<Folder> GetFoldersForUser(long userId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<Folder> folders = new List<Folder>();

            try
            {
                int totalRows;

                _folderDataService.CreateSession();
                folders = _folderDataService.GetFoldersForUser(userId, currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _folderDataService.CloseSession();

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
                _folderDataService.CloseSession();
            }

            return folders;

        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            Folder Folder = new Folder();

            try
            {

                _folderDataService.CreateSession();
                _folderDataService.BeginTransaction();
                _folderDataService.CommitTransaction(true);
                _folderDataService.CloseSession();

                _folderDataService.CreateSession();
                _folderDataService.BeginTransaction();
                _folderDataService.CommitTransaction(true);
                _folderDataService.CloseSession();

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
                _folderDataService.CloseSession();
            }           

        }




    }
}
