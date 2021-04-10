using System;
using System.Collections.Generic;
using System.Linq;
using RedisReader.Server.Services.ValueTypes;
using StackExchange.Redis;

namespace RedisReader.Server.Services
{
    public interface IReadFromRedis
    {
        List<string> GetKeys(Guid connectionId, RedisValue pattern, int page, int pageSize);
        IType GetValue(Guid connectionId, string key);
    }

    public class RedisDataReader : IReadFromRedis
    {
        private readonly IManageRedisConnections _connections;

        public RedisDataReader(IManageRedisConnections connections)
        {
            _connections = connections;
        }
        
        public List<string> GetKeys(Guid connectionId, RedisValue pattern, int page, int pageSize)
        {
            var conn = _connections.GetConnection(connectionId);
            var endPoint = conn.GetEndPoints().First();
            var server = conn.GetServer(endPoint);
            
            return server
                .Keys(pattern: pattern)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => x.ToString()).ToList();
        }

        public IType GetValue(Guid connectionId, string key)
        {
            var db = _connections.GetConnection(connectionId).GetDatabase(0);

            return db.KeyType(key) switch
            {
                RedisType.List => new ListType(db.ListRange(key)),
                RedisType.Hash => new HashSetType(db.HashScan(key)),
                RedisType.Set => new SetType(db.SetMembers(key)),
                RedisType.String => new StringType(db.StringGet(key)),
                RedisType.SortedSet => new SortedSetType(db.SortedSetScan(key)),
                _ => new UnsupportedType()
            };
        }
    }
}