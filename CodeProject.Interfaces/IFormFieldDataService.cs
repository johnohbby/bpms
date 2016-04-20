using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    /// Form Data Service
    /// </summary>
    public interface IFormFieldDataService : IDataRepository, IDisposable
    {
        void CreateFormField(FormField formField);
        void UpdateFormField(FormField formField);
        void DeleteFormField(FormField formField);
        FormField GetFormField(long formFieldID);
        List<FormField> GetFormFields(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        List<FormField> GetFormFieldsForForm(long formId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
    }
}

