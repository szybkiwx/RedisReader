using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace RedisReader.Server.Services
{
    public interface IReadFromRedis
    {
        List<string> GetKeys(Guid connectionId, RedisValue pattern);
    }

    public class DataReader : IReadFromRedis
    {
        private readonly IManageRedisConnections _connections;

        public DataReader(IManageRedisConnections connections)
        {
            _connections = connections;
        }
        
        public List<string> GetKeys(Guid connectionId, RedisValue pattern = default)
        {
            var conn = _connections.GetConnection(connectionId);
            var endPoint = conn.GetEndPoints().First();
            var server = conn.GetServer(endPoint);
            return server.Keys(pattern: pattern).Select(x => x.ToString()).ToList();
        }
    }
}