using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Roberto.DataStore
{
    public interface IDatabase
    {

        Task<T> QueryAsync<T>(IQuery<T> query);
        Task<T> CommandAsync<T>(ICommand<T> command);
    }
}