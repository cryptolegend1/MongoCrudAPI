
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoCrudAPI.DbContext.Document;
using MongoDB.Bson;

namespace MongoCrudAPI.DbContext.Repository
{
    public interface IMongoRepository<TMongoDocument> where TMongoDocument : IMongoDocument
    {
        IEnumerable<TMongoDocument> GetAll();

        IEnumerable<TMongoDocument> FilterBy(FilterDefinition<TMongoDocument> filter);

        IEnumerable<TMongoDocument> FilterBy(Expression<Func<TMongoDocument, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TMongoDocument, bool>> filterExpression,
            Expression<Func<TMongoDocument, TProjected>> projectionExpression);

        TMongoDocument FindOne(Expression<Func<TMongoDocument, bool>> filterExpression);

        Task<TMongoDocument> FindOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression);

        TMongoDocument FindById(string id);

        Task<TMongoDocument> FindByIdAsync(string id);

        void InsertOne(TMongoDocument document);

        Task<TMongoDocument> InsertOneAsync(TMongoDocument document);

        void UpdateOne(Expression<Func<TMongoDocument, bool>> filterExpression);

        Task UpdateOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression);

        void UpdateById(string id, TMongoDocument document);

        Task UpdateByIdAsync(string id, TMongoDocument document);

        void DeleteOne(Expression<Func<TMongoDocument, bool>> filterExpression);

        Task DeleteOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression);

        void DeleteById(string id);

        Task DeleteByIdAsync(string id);
    }
}
