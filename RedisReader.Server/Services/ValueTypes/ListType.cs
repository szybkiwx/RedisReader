using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace RedisReader.Server.Services.ValueTypes
{
    public class ListType : IType
    {
        public List<string> Value { get; }
     
        public ListType(RedisValue[] listRange)
        {
            Value = listRange.Select(x => (string)x).ToList();
        }
    }
}