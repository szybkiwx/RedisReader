/*using System;
using System.Collections.Generic;
using System.IO;
using LiteDB;
using RedisReader.Server.Models;

namespace RedisReader.Server.Services
{
    public interface IStoreConnections
    {
        List<RedisConnection> GetConnections();
        void AddConnection(RedisConnection connection);
        void DeleteConnection(Guid id);
        RedisConnection GetConnection(Guid id);
        void UpdateConnection(RedisConnection connection);
    }
    
    public class ConnectionStore : IStoreConnections
    {
        private readonly string _dbPath = Path.Join(AppContext.BaseDirectory, "Data.db");
        private const string ConnectionsCollectionName = "connections";

        public List<RedisConnection> GetConnections()
        {
            using var db = new LiteDatabase(_dbPath);
            return db.GetCollection<RedisConnection>(ConnectionsCollectionName).Query().ToList();
        }

        public void AddConnection(RedisConnection connection)
        {
            using var db = new LiteDatabase(_dbPath);
            var collection = db.GetCollection<RedisConnection>(ConnectionsCollectionName);
            collection.Insert(connection);
        }

        public void DeleteConnection(Guid id)
        {
            using var db = new LiteDatabase(_dbPath);
            var collection = db.GetCollection<RedisConnection>(ConnectionsCollectionName);
            collection.Delete(id);
        }

        public RedisConnection GetConnection(Guid id)
        {
            using var db = new LiteDatabase(_dbPath);
            var collection = db.GetCollection<RedisConnection>(ConnectionsCollectionName);
            return collection.FindById(id);
        }

        public void UpdateConnection(RedisConnection connection)
        {
            using var db = new LiteDatabase(_dbPath);
            var collection = db.GetCollection<RedisConnection>(ConnectionsCollectionName);
            collection.Update(connection);
        }
    }
}*/