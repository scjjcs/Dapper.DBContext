﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Dapper.DBContext.Dialect
{
    public interface ISqlBuilder
    {
        string BuildInsert(Type modelType);
        string BuildUpdate(Type modelType);
        string BuildDelete(Type modelType);
        string buildSelectById<TEntity>();
        string buildSelect<TEntity>();
        string BuildSelectByLamda<TEntity>(Expression<Func<TEntity, bool>> expression, out object arguments, string columns = "");

    }
}
