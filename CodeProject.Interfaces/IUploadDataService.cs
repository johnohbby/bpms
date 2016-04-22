using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    /// Product Data Service
    /// </summary>
    public interface IUploadDataService : IDataRepository, IDisposable
    {
        void Upload();
      
    }
}

