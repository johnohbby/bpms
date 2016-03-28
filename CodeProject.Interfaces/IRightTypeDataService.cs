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
    public interface IRightTypeDataService : IDataRepository, IDisposable
    {
        void CreateRightType(RightType rightType);
        void UpdateRightType(RightType rightType);
        RightType GetRightType(long rightTypeID);
        List<RightType> GetRightTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        void DeleteRightType(RightType rightType);

    }
}

