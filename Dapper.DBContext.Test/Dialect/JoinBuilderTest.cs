﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper.DBContext.Dialect;
using Dapper.DBContext.Test.Domain;
using System.Linq.Expressions;
using System.Dynamic;
namespace Dapper.DBContext.Test.Dialect
{
      [TestClass]
   public class JoinBuilderTest 
    {

        IQuery _query;
          [TestInitialize]
          public void Init()
          {

              this._query = new QueryService("O2O");
            
          }
          [TestMethod]
          public void FindPage_test()
          {
              var result = this._query.FindPage<Order>(1, 10).InnerJoin<OrderItem>().Where<Order, OrderItem>(o=>o.Code.Like("12%")&&o.Id == 12);
             Assert.AreEqual(0, result.Any());
          }


    }
}
