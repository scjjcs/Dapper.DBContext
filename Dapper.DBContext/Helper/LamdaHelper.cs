﻿using Dapper.DBContext.Dialect;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.DBContext.Helper
{
   public class LamdaHelper
    {

       public static List<QueryArgument> GetWhere<T>(Expression<Func<T, bool>> where)
       {
           var bExpr = GetBinaryExpression(where.Body);
           List<QueryArgument> sqls = new List<QueryArgument>();
           GetWhere(bExpr, sqls);

            return sqls;
       }

       public static void GetWhere(BinaryExpression body,  List<QueryArgument> queryProperties,string link="")
       {
          
           if (body.NodeType != ExpressionType.AndAlso && body.NodeType != ExpressionType.OrElse)
           {
               var propertyName = GetPropertyName(body);

               // 检查字段查询中，是否存在 基础属性
              
               var propertyValue = GetValue(body.Right);
               var opr = GetSqlOperator(body.NodeType);
              

               if (body.Left.NodeType == ExpressionType.Call)
               {
                   MethodCallExpression callExp = body.Left as MethodCallExpression;
                   ConstantExpression pvExp = null;
                   switch (callExp.Method.Name)
                   {
                       case "Like":
                           pvExp = callExp.Arguments[1] as ConstantExpression;
                           propertyValue = pvExp.Value;
                           opr = "Like";
                           break;                      
                       default:
                           throw new Exception(string.Format("sql不支持此方法[{0}]", callExp.Method.Name));

                   }
               }
               // 同一个属性，多次查询时，改变属性变量名
                int index = 1;
                while (queryProperties.Exists(n => n.Name.Contains(propertyName)))
                {
                    propertyName += string.Format("_{0}", index);
                    index += 1;
                }
                queryProperties.Add(new QueryArgument(propertyName,propertyValue,opr,link));

                //if (propertyValue is string || propertyValue is DateTime)
                //{
                //    queryProperties.Add(string.Format("{0} {1} '{2}'", propertyName, opr, propertyValue));
                //}
                //else {
                //    queryProperties.Add(string.Format("{0} {1} {2}", propertyName, opr, propertyValue));
                //}              
           }
           else
           {
               //递归解析
               GetWhere(GetBinaryExpression(body.Left),  queryProperties, GetSqlOperator(body.NodeType));
           
               GetWhere(GetBinaryExpression(body.Right),  queryProperties);

           }
       }

       public static string GetSqlOperator(ExpressionType type)
       {
           switch (type)
           {
               case ExpressionType.Equal:
                   return "=";

               case ExpressionType.NotEqual:
                   return "!=";

               case ExpressionType.LessThan:
                   return "<";

               case ExpressionType.LessThanOrEqual:
                   return "<=";

               case ExpressionType.GreaterThan:
                   return ">";

               case ExpressionType.GreaterThanOrEqual:
                   return ">=";

               case ExpressionType.AndAlso:
               case ExpressionType.And:
                   return "AND";

               case ExpressionType.Or:
               case ExpressionType.OrElse:
                   return "OR";

               case ExpressionType.Default:
                   return string.Empty;

               default:
                   throw new NotImplementedException();
           }
       }

       public static object GetValue(Expression member)
       {
           var objectMember = Expression.Convert(member, typeof(object));
           var getterLambda = Expression.Lambda<Func<object>>(objectMember);
           var getter = getterLambda.Compile();
           return getter();
       }

       public static BinaryExpression GetBinaryExpression(Expression expression)
       {
           var binaryExpression = expression as BinaryExpression;
           var body = binaryExpression ?? Expression.MakeBinary(ExpressionType.Equal, expression, Expression.Constant(true));
           return body;
       }

       /// <summary>
       /// Gets the name of the property.
       /// </summary>
       /// <param name="body">The body.</param>
       /// <returns>The property name for the property expression.</returns>
       public static string GetPropertyName(BinaryExpression body)
       {
           string propertyName = body.Left.ToString().Split('.')[1];

           if (body.Left.NodeType == ExpressionType.Convert)
           {
               // remove the trailing ) when convering.
               propertyName = propertyName.Replace(")", string.Empty);
           }

           return propertyName;
       }

      
    }
}
