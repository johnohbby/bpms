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
    public interface IMailTemplateMapDataService : IDataRepository, IDisposable
    {
        void CreateMailTemplateMap(MailTemplateMap mailTemplateMap);
        void DeleteMailTemplateMap(MailTemplateMap mailTemplateMap);
        List<MailTemplateMap> GetMapsForMailTemplate(long mailTemplateId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
          
    }
}

