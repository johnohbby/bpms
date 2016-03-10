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
    public interface IContentTypeDataService : IDataRepository, IDisposable
    {
        void CreateContentType(ContentType contentType);
        void UpdateContentType(ContentType contentType);
        ContentType GetContentType(long contentTypeID);
        List<ContentType> GetContentTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);

    }
}

