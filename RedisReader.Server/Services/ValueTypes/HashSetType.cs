using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace RedisReader.Server.Services.ValueTypes
{
    public class HashSetType : IType
    {
        public Dictionary<string, string> Value { get; }
        
        public HashSetType(IEnumerable<HashEntry> hash)
        {
            Value = hash.ToDictionary(x => (string)x.Name, x => (string)x.Value);
        }
    }
}