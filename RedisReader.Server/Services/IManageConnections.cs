using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RedisReader.Server.Models;

namespace RedisReader.Server.Services
{
    public interface IManageConnections
    {
        List<RedisConnection> GetConnections();
        void AddConnection(RedisConnection connection);
        void DeleteConnection(Guid id);
        RedisConnection GetConnection(Guid id);
        void UpdateConnection(RedisConnection connection);
    }
}