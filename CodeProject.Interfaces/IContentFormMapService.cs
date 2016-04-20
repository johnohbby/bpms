using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    /// Content Type Data Service
    /// </summary>
    public interface IContentFormMapDataService : IDataRepository, IDisposable
    {
        void CreateContentFormMap(ContentFormMap contentFormMap);
        void UpdateContentFormMap(ContentFormMap contentFormMap);
        void DeleteContentFormMap(ContentFormMap contentFormMap);
        ContentFormMap GetContentFormMap(long contentFormMapID);
        List<ContentFormMap> GetContentFormMaps(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);

    }
}

