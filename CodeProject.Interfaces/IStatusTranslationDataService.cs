using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    /// Status Translation Data Service
    /// </summary>
    public interface IStatusTranslationDataService : IDataRepository, IDisposable
    {
        void CreateStatusTranslation(StatusTranslation statusTranslation);
        void UpdateStatusTranslation(StatusTranslation statusTranslation);
        void DeleteStatusTranslation(StatusTranslation statusTranslation);
        StatusTranslation GetStatusTranslation(long statusTranslationID);
        List<StatusTranslation> GetStatusTranslations(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        List<StatusTranslation> GetStatusTranslationsForActionType(long actionTypeId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
          
    }
}

