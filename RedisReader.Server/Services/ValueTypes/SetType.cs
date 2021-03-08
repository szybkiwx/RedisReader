using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace RedisReader.Server.Services.ValueTypes
{
    public class SetType : IType
    {
        public HashSet<string> Value { get; }
        
        public SetType(RedisValue[] setMembers)
        {
            Value = setMembers.Select(x => (string)x).ToHashSet();
        }
    }
}