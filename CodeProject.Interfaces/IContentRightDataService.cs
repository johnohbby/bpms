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
    public interface IContentRightDataService : IDataRepository, IDisposable
    {
        void CreateContentRight(ContentRight contentRight);
        void UpdateContentRight(ContentRight contentRight);
        void DeleteContentRight(ContentRight contentRight);
        ContentRight GetContentRight(long contentRightID);
        List<ContentRight> GetContentRights(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);

    }
}

