using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ToothCrystal.Core.Classes
{
    public class DataDocumentSession : IDataDocumentSession
    {
        private readonly IDocumentSession _documentSession;
        public DataDocumentSession(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public ISyncAdvancedSessionOperation Advanced
        {
            get { return _documentSession.Advanced; }
        }

        public void Store(dynamic entity, string id)
        {
            _documentSession.Store(entity, id);
        }

        public void Store(dynamic entity)
        {
            _documentSession.Store(entity);
        }

        public void Store(object entity, Etag etag, string id)
        {
            _documentSession.Store(entity, etag, id);
        }

        public void Store(object entity, Etag etag)
        {
            _documentSession.Store(entity, etag);
        }

        public void SaveChanges()
        {
            _documentSession.SaveChanges();
        }

        public TResult[] Load<TTransformer, TResult>(IEnumerable<string> ids, Action<ILoadConfiguration> configure) where TTransformer : AbstractTransformerCreationTask, new()
        {
            return _documentSession.Load<TTransformer, TResult>(ids, configure);
        }

        public TResult[] Load<TTransformer, TResult>(params string[] ids) where TTransformer : AbstractTransformerCreationTask, new()
        {
            return _documentSession.Load<TTransformer, TResult>(ids);
        }

        public TResult Load<TTransformer, TResult>(string id, Action<ILoadConfiguration> configure) where TTransformer : AbstractTransformerCreationTask, new()
        {
            return _documentSession.Load<TTransformer, TResult>(id, configure);
        }

        public TResult Load<TTransformer, TResult>(string id) where TTransformer : AbstractTransformerCreationTask, new()
        {
            return _documentSession.Load<TTransformer, TResult>(id);
        }

        public ILoaderWithInclude<T> Include<T, TInclude>(Expression<Func<T, object>> path)
        {
            return _documentSession.Include<T, TInclude>(path);
        }

        public ILoaderWithInclude<T> Include<T>(Expression<Func<T, object>> path)
        {
            return _documentSession.Include(path);
        }

        public ILoaderWithInclude<object> Include(string path)
        {
            return _documentSession.Include(path);
        }

        public IRavenQueryable<T> Query<T, TIndexCreator>() where TIndexCreator : AbstractIndexCreationTask, new()
        {
            return _documentSession.Query<T, TIndexCreator>();
        }

        public IRavenQueryable<T> Query<T>()
        {
            return _documentSession.Query<T>();
        }

        public IRavenQueryable<T> Query<T>(string indexName, bool isMapReduce = false)
        {
            return _documentSession.Query<T>(indexName, isMapReduce);
        }

        public T[] Load<T>(IEnumerable<ValueType> ids)
        {
            return _documentSession.Load<T>(ids);
        }

        public T[] Load<T>(params ValueType[] ids)
        {
            return _documentSession.Load<T>(ids);
        }

        public T Load<T>(ValueType id)
        {
            return _documentSession.Load<T>(id);
        }

        public T[] Load<T>(IEnumerable<string> ids)
        {
            return _documentSession.Load<T>(ids);
        }

        public T[] Load<T>(params string[] ids)
        {
            return _documentSession.Load<T>(ids);
        }

        public T Load<T>(string id)
        {
            return _documentSession.Load<T>(id);
        }

        public void Delete<T>(T entity)
        {
            _documentSession.Delete(entity);
        }

        public void Dispose()
        {
            _documentSession.Dispose();
        }
    }
}