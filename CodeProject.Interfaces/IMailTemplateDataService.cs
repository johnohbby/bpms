using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    /// Action Type Data Service
    /// </summary>
    public interface IMailTemplateDataService : IDataRepository, IDisposable
    {
        void CreateMailTemplate(MailTemplate mailTemplate);
        void UpdateMailTemplate(MailTemplate mailTemplate);
        MailTemplate GetMailTemplate(long mailTemplateID);
        void DeleteMailTemplate(MailTemplate mailTemplate);
        List<MailTemplate> GetMailTemplates(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        MailTemplate GetMailTemplateForAction(long actionId);  
    }
}

