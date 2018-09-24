using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Replen.DataStore.Extentions
{
    public static class DapperExtentions
    {
        static DapperExtentions()
        {
            SqlMapperExtensions.TableNameMapper = (type) => {
                //Dapper uses plural table names by default. Use singluar.
                return type.Name;
            };
        }

    }
}
