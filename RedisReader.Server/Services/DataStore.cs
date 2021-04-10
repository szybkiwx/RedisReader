using System;
using System.Collections.Generic;
using System.IO;
using LiteDB;

namespace RedisReader.Server.Services
{
    public interface IStoreData<T> where T : class
    {
        List<T> GetAll();
        void Add(T connection);
        void Delete(Guid id);
        T Get(Guid id);
        void Update(T connection);
    }
    
    public class DataStore<T> : IStoreData<T> where T : class
    {
        private readonly string _dbPath = Path.Join(AppContext.BaseDirectory, "Data.db");

        private readonly string _connectionsCollectionName = typeof(T).Name + "Collection";
        
        public List<T> GetAll()
        {
            using var db = new LiteDatabase(_dbPath);
            return db.GetCollection<T>(_connectionsCollectionName).Query().ToList();
        }

        public void Add(T connection)
        {
            using var db = new LiteDatabase(_dbPath);
            var collection = db.GetCollection<T>(_connectionsCollectionName);
            collection.Insert(connection);
        }

        public void Delete(Guid id)
        {
            using var db = new LiteDatabase(_dbPath);
            var collection = db.GetCollection<T>(_connectionsCollectionName);
            collection.Delete(id);
        }

        public T Get(Guid id)
        {
            using var db = new LiteDatabase(_dbPath);
            var collection = db.GetCollection<T>(_connectionsCollectionName);
            return collection.FindById(id);
        }

        public void Update(T connection)
        {
            using var db = new LiteDatabase(_dbPath);
            var collection = db.GetCollection<T>(_connectionsCollectionName);
            collection.Update(connection);
        }
    }
}