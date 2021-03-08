using System;

namespace RedisReader.Server.Services
{
    public class KeyStateContainer
    {
        public string Key { get; private set; }
        
        public event Action OnChange;
        
        public void SetKey(string key)
        {
            Key = key;
            NotifyStateChanged();
        }
        
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}