using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ToothCrystal.Classes
{
    public class AsyncDataDocumentSession : IAsyncDataDocumentSession
    {
        private readonly IAsyncDocumentSession _documentSession;
        public AsyncDataDocumentSession(IAsyncDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        #region Delgating members to inner document session
        public void Dispose()
        {
            _documentSession.Dispose();
        }

        public IAsyncLoaderWithInclude<object> Include(string path)
        {
            return _documentSession.Include(path);
        }

        public IAsyncLoaderWithInclude<T> Include<T>(Expression<Func<T, object>> path)
        {
            return _documentSession.Include(path);
        }

        public IAsyncLoaderWithInclude<T> Include<T, TInclude>(Expression<Func<T, object>> path)
        {
            return _documentSession.Include<T, TInclude>(path);
        }

        public Task StoreAsync(object entity, Etag etag)
        {
            return _documentSession.StoreAsync(entity, etag);
        }

        public Task StoreAsync(object entity)
        {
            return _documentSession.StoreAsync(entity);
        }

        public Task StoreAsync(object entity, Etag etag, string id)
        {
            return _documentSession.StoreAsync(entity, etag, id);
        }

        public Task StoreAsync(object entity, string id)
        {
            return _documentSession.StoreAsync(entity, id);
        }

        public void Delete<T>(T entity)
        {
            _documentSession.Delete(entity);
        }

        public Task<T> LoadAsync<TTransformer, T>(string id) where TTransformer : AbstractTransformerCreationTask, new()
        {
            return _documentSession.LoadAsync<TTransformer, T>(id);
        }

        public Task<T> LoadAsync<TTransformer, T>(string id, Action<ILoadConfiguration> configure) where TTransformer : AbstractTransformerCreationTask, new()
        {
            return _documentSession.LoadAsync<TTransformer, T>(id, configure);
        }

        public Task<TResult[]> LoadAsync<TTransformer, TResult>(IEnumerable<string> ids, Action<ILoadConfiguration> configure) where TTransformer : AbstractTransformerCreationTask, new()
        {
            return _documentSession.LoadAsync<TTransformer, TResult>(ids, configure);
        }

        public Task<T> LoadAsync<T>(string id)
        {
            return _documentSession.LoadAsync<T>(id);
        }

        public Task<T[]> LoadAsync<T>(params string[] ids)
        {
            return _documentSession.LoadAsync<T>(ids);
        }

        public Task<T[]> LoadAsync<TTransformer, T>(params string[] ids) where TTransformer : AbstractTransformerCreationTask, new()
        {
            return _documentSession.LoadAsync<TTransformer, T>(ids);
        }

        public Task<T[]> LoadAsync<T>(IEnumerable<string> ids)
        {
            return _documentSession.LoadAsync<T>(ids);
        }

        public Task<T> LoadAsync<T>(ValueType id)
        {
            return _documentSession.LoadAsync<T>(id);
        }

        public Task<T[]> LoadAsync<T>(params ValueType[] ids)
        {
            return _documentSession.LoadAsync<T>(ids);
        }

        public Task<T[]> LoadAsync<T>(IEnumerable<ValueType> ids)
        {
            return _documentSession.LoadAsync<T>(ids);
        }

        public Task SaveChangesAsync()
        {
            return _documentSession.SaveChangesAsync();
        }

        public IRavenQueryable<T> Query<T>(string indexName, bool isMapReduce = false)
        {
            return _documentSession.Query<T>(indexName, isMapReduce);
        }

        public IRavenQueryable<T> Query<T>()
        {
            return _documentSession.Query<T>();
        }

        public IRavenQueryable<T> Query<T, TIndexCreator>() where TIndexCreator : AbstractIndexCreationTask, new()
        {
            return _documentSession.Query<T, TIndexCreator>();
        }

        public IAsyncAdvancedSessionOperations Advanced
        {
            get { return _documentSession.Advanced; }
        }
        #endregion
    }
}