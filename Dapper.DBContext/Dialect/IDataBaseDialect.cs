﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.DBContext.Dialect
{
    public interface IDataBaseDialect
    {
        DataBaseEnum DataBaseType { get; }
         string WrapFormat { get; }
         string PageFormat { get; }
         string IdentityFromat { get; }
       
    }
}