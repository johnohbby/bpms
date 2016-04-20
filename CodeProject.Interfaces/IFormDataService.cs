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
    public interface IFormDataService : IDataRepository, IDisposable
    {
        void CreateForm(Form form);
        void UpdateForm(Form form);
        void DeleteForm(Form form);
        Form GetForm(long formID);
        List<Form> GetForms( int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        List<Form> GetFormByActionTypeId(long actionTypeId, out int totalRows);
        List<FormField> GetFormFieldsByFormId(long formId, out int totalRows); 
        void InsertData(long formId, string allValues, out int totalRows);
        void CreateTable(long formId, out int totalRows);
    }
}

