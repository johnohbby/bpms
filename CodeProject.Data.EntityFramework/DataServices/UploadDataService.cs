using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Interfaces;
using CodeProject.Business.Entities;
using CodeProject.Business.Common;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CodeProject.Data.EntityFramework
{
    /// <summary>
    /// User Data Service
    /// </summary>
    public class UploadDataService : EntityFrameworkService, IUploadDataService
    {

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="customer"></param>
       public void Upload() { }

    }

}

