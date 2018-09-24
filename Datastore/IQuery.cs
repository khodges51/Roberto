using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Replen.DataStore
{
    public interface IQuery<T>
    {
        Task<T> ExecuteAsync(IDbConnection db);
    }
}
