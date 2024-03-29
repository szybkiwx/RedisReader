using System;
using System.Collections.Generic;
using StackExchange.Redis;

namespace RedisReader.Server.Services
{
    public interface IManageRedisConnections
    {
        void Connect(Guid id);
        void CloseConnection(Guid id);
        ConnectionMultiplexer GetConnection(Guid id);
        bool IsConnected(Guid id);
    }
    
    public class ManageRedisConnections : IManageRedisConnections
    {
        private readonly Dictionary<Guid, ConnectionMultiplexer> _activeConnections = new();
        private readonly IStoreConnections _connectionsStore;

        public ManageRedisConnections(IStoreConnections connectionsStore)
        {
            _connectionsStore = connectionsStore;
        }

        public void Connect(Guid id)
        {
            if (_activeConnections.ContainsKey(id))
            {
                _activeConnections[id].Dispose();
                _activeConnections.Remove(id);
            }
            
            var connectionDetails = _connectionsStore.GetConnection(id);
            var mux = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                Password = connectionDetails.Password,
                Ssl = connectionDetails.UseTsl,
                EndPoints = { connectionDetails.Endpoint },
                AllowAdmin = false,
                ConnectTimeout = 1500
            });

            _activeConnections[id] = mux;
        }

        public void CloseConnection(Guid id)
        {
            _activeConnections[id].Close();
            _activeConnections.Remove(id);
        }

        public ConnectionMultiplexer GetConnection(Guid id)
        {
            return _activeConnections[id];
        }

        public bool IsConnected(Guid id)
        {
            return _activeConnections.ContainsKey(id);
        }
    }
}