﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.DBContext.Dialect
{
   public class JoinBuilderContext
    {

       public JoinBuilderContext()
       {
           this.IsPage = false;
           this.JoinTables = new List<JoinArgument>();
       }
       /// <summary>
       /// is page
       /// </summary>
       public bool IsPage { get; private set; }
       public int PageIndex { get; set; }
       public int PageSize { get; set; }
       /// <summary>
       /// all join table object
       /// </summary>
       public IList<JoinArgument> JoinTables { get; private set; }

       public void SetPageInfo(int pageIndex, int pageSize)
       {
           this.IsPage = true;
           this.PageIndex = pageIndex;
           this.PageSize = pageSize;
       }

       public void Add(Type entityType,string joinMethod = "")
       {
            JoinTables.Add(new JoinArgument(entityType,joinMethod,this.JoinTables.Count));
       }
       
    }
}
