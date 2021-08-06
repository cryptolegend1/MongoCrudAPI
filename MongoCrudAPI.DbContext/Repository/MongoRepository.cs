using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoCrudAPI.DbContext.Attributes;
using MongoCrudAPI.DbContext.Document;
using MongoCrudAPI.DbContext.Provider;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoCrudAPI.DbContext.Repository
{
    public class MongoRepository<TMongoDocument> : IMongoRepository<TMongoDocument> where TMongoDocument : IMongoDocument
    {
        private readonly IMongoCollection<TMongoDocument> _collection;

        public MongoRepository(IMongoDatabaseProvider provider)
        {
            _collection = new MongoClient(provider.ConnectionString)
                                .GetDatabase(provider.DatabaseName)
                                .GetCollection<TMongoDocument>(GetCollectionName(typeof(TMongoDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((MongoCollectionAttribute)documentType.GetCustomAttributes(typeof(MongoCollectionAttribute), true).FirstOrDefault())?.CollectionName;
        }

        public IMongoCollection<TMongoDocument> GetCollection()
        {
            return _collection;
        }

        public virtual IEnumerable<TMongoDocument> GetAll()
        {
            return _collection.Find<TMongoDocument>(document => true).ToList();
        }

        public virtual IEnumerable<TMongoDocument> FilterBy(Expression<Func<TMongoDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TMongoDocument> FilterBy(FilterDefinition<TMongoDocument> filter)
        {
            return _collection.Find(filter).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TMongoDocument, bool>> filterExpression,
            Expression<Func<TMongoDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual TMongoDocument FindOne(Expression<Func<TMongoDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<TMongoDocument> FindOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual TMongoDocument FindById(string id)
        {
            var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, ObjectId.Parse(id));
            return _collection.Find(filter).SingleOrDefault();
        }

        public virtual Task<TMongoDocument> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, ObjectId.Parse(id));
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual void InsertOne(TMongoDocument document)
        {
            _collection.InsertOne(document);
        }

        public virtual async Task<TMongoDocument> InsertOneAsync(TMongoDocument document)
        {
            await _collection.InsertOneAsync(document);
            return document;
        }

        public void UpdateOne(Expression<Func<TMongoDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public void UpdateById(string id, TMongoDocument document)
        {
            var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, ObjectId.Parse(id));
            _collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task UpdateByIdAsync(string id, TMongoDocument document)
        {
            var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, ObjectId.Parse(id));
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteOne(Expression<Func<TMongoDocument, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public void DeleteById(string id)
        {
            var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, ObjectId.Parse(id));
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, ObjectId.Parse(id));
                _collection.FindOneAndDeleteAsync(filter);
            });
        }
    }
}
