using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    ///User Data Service
    /// </summary>
    public interface IEmailDataService : IDataRepository, IDisposable
    {
        void SendEmail(Email email);
    }
}

