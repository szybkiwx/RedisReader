using System;

namespace RedisReader.Server.Services
{
    public class ConnectionStateContainer
    {
        public event Action OnChange;

        public void NotifyStateChanged() => OnChange?.Invoke();
    }
}