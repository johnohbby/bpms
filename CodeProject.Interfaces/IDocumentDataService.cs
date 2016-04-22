using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    /// Document Data Service
    /// </summary>
    public interface IDocumentDataService : IDataRepository, IDisposable
    {
        void CreateDocument(Document document);
        Document GetDocument(long documentID);
        void DeleteDocument(Document document);
        List<Document> GetDocuments(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        List<Document> GetDocumentsForFolder(long folderId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);  
    }
}

