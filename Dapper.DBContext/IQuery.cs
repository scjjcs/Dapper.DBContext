﻿using Dapper.DBContext.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.DBContext
{
    public interface IQuery
    {
        TEntity Find<TEntity>(int Id) where TEntity : IEntity;
        TEntity Find<TEntity>(string Id) where TEntity : IEntity;
        TEntity Find<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : IEntity;
        IEnumerable<TEntity> Find<TEntity>(string[] Ids) where TEntity : IEntity;
        IEnumerable<TEntity> Find<TEntity>(int[] Ids) where TEntity : IEntity;
        IEnumerable<TEntity> FindAll<TEntity>() where TEntity : IEntity;
        IEnumerable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : IEntity;
        bool Exists<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : IEntity;
        /// <summary>
        /// join query
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IJoinQuery FindJoin<TEntity>() where TEntity : IEntity;
        /// <summary>
        ///  find by page
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IJoinQuery FindPage<TEntity>(int pageIndex , int pageSize) where TEntity : IEntity;
    }
}
