﻿// Copyright (c) Avanade. Licensed under the MIT License. See https://github.com/Avanade/Beef

using Beef.Entities;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beef.Data.Cosmos
{
    /// <summary>
    /// Provides common <b>DocumentDb/CosmosDb</b> query capabilities.
    /// </summary>
    public abstract class CosmosDbQueryBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CosmosDbQueryBase"/> class.
        /// </summary>
        protected CosmosDbQueryBase() { }

        /// <summary>
        /// Gets a <see cref="PagingArgs"/> with a top/take of 1.
        /// </summary>
        internal static PagingArgs PagingTop1 { get; } = PagingArgs.CreateSkipAndTake(0, 1);

        /// <summary>
        /// Gets a <see cref="PagingArgs"/> with a top/take of 2.
        /// </summary>
        internal static PagingArgs PagingTop2 { get; } = PagingArgs.CreateSkipAndTake(0, 2);
    }

    /// <summary>
    /// Encapsulates a <b>DocumentDb/CosmosDb</b> query enabling all select-like capabilities.
    /// </summary>
    /// <typeparam name="T">The resultant <see cref="Type"/>.</typeparam>
    public class CosmosDbQuery<T> : CosmosDbQueryBase where T : class, new()
    {
        private readonly CosmosDbBase _db;
        private readonly Func<IOrderedQueryable<T>, IQueryable<T>> _query;

        /// <summary>
        /// Initializes a new instance of the <see cref="CosmosDbQuery{T}"/> class.
        /// </summary>
        /// <param name="db">The <see cref="CosmosDbBase"/>.</param>
        /// <param name="queryArgs">The <see cref="CosmosDbArgs"/>.</param>
        /// <param name="query">A function to modify the underlying <see cref="IQueryable{T}"/>.</param>
        internal CosmosDbQuery(CosmosDbBase db, CosmosDbArgs queryArgs, Func<IOrderedQueryable<T>, IQueryable<T>> query = null)
        {
            _db = Check.NotNull(db, nameof(db));
            QueryArgs = Check.NotNull(queryArgs, nameof(queryArgs));
            _query = query;
        }

        /// <summary>
        /// Gets the <see cref="CosmosDbArgs"/>.
        /// </summary>
        public CosmosDbArgs QueryArgs { get; private set; }

        /// <summary>
        /// Manages the DbContext and underlying query construction and lifetime.
        /// </summary>
        private void ExecuteQuery(Action<IQueryable<T>> execute)
        {
            CosmosDbInvoker.Default.Invoke(this, () =>
            {
                var q = _db.GetContainer(QueryArgs.ContainerId).GetItemLinqQueryable<T>(allowSynchronousQueryExecution: true, requestOptions: _db.GetQueryRequestOptions(QueryArgs));
                execute(_query(q));
            }, _db);
        }

        /// <summary>
        /// Manages the DbContext and underlying query construction and lifetime.
        /// </summary>
        private T ExecuteQuery(Func<IQueryable<T>, T> execute)
        {
            return CosmosDbInvoker.Default.Invoke(this, () =>
            {
                var q = _db.GetContainer(QueryArgs.ContainerId).GetItemLinqQueryable<T>(allowSynchronousQueryExecution: true, requestOptions: _db.GetQueryRequestOptions(QueryArgs));
                return execute(_query(q));
            }, _db);
        }

        /// <summary>
        /// Sets the paging from the <see cref="PagingArgs"/>.
        /// </summary>
        private IQueryable<T> SetPaging(IQueryable<T> query, PagingArgs paging)
        {
            var q = query;
            if (paging != null && paging.Skip > 0)
                q = q.Skip((int)paging.Skip);

            return q.Take((int)(paging == null ? PagingArgs.DefaultTake : paging.Take));
        }

        #region SelectSingle/SelectFirst

        /// <summary>
        /// Selects a single item.
        /// </summary>
        /// <returns>The single item.</returns>
        public T SelectSingle()
        {
            return ExecuteQuery(q =>
            {
                q = SetPaging(q, PagingTop2);
                return q.AsEnumerable().Single();
            });
        }

        /// <summary>
        /// Selects a single item or default.
        /// </summary>
        /// <returns>The single item or default.</returns>
        public T SelectSingleOrDefault()
        {
            return ExecuteQuery(q =>
            {
                q = SetPaging(q, PagingTop2);
                return q.AsEnumerable().SingleOrDefault();
            });
        }

        /// <summary>
        /// Selects first item.
        /// </summary>
        /// <returns>The first item.</returns>
        public T SelectFirst()
        {
            return ExecuteQuery(q =>
            {
                q = SetPaging(q, PagingTop1);
                return q.AsEnumerable().First();
            });
        }

        /// <summary>
        /// Selects first item or default.
        /// </summary>
        /// <returns>The single item or default.</returns>
        public T SelectFirstOrDefault()
        {
            return ExecuteQuery(q =>
            {
                q = SetPaging(q, PagingTop1);
                return q.AsEnumerable().FirstOrDefault();
            });
        }

        #endregion

        #region SelectQuery

        /// <summary>
        /// Executes the query command creating a resultant collection.
        /// </summary>
        /// <typeparam name="TColl">The collection <see cref="Type"/>.</typeparam>
        /// <returns>A resultant collection.</returns>
        public TColl SelectQuery<TColl>() where TColl : ICollection<T>, new()
        {
            var coll = new TColl();
            SelectQuery(coll);
            return coll;
        }

        /// <summary>
        /// Executes a query adding to the passed collection.
        /// </summary>
        /// <typeparam name="TColl">The collection <see cref="Type"/>.</typeparam>
        /// <param name="coll">The collection to add items to.</param>
        public void SelectQuery<TColl>(TColl coll) where TColl : ICollection<T>
        {
            ExecuteQuery(query => 
            {
                foreach (var item in SetPaging(query, QueryArgs.Paging).AsEnumerable())
                {
                    coll.Add(CosmosDbBase.GetResponseValue<T>(item));
                }

                if (QueryArgs.Paging != null && QueryArgs.Paging.IsGetCount)
                    QueryArgs.Paging.TotalCount = query.Count();
            });
        }

        #endregion
    }
}