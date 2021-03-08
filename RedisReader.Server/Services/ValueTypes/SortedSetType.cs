using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace RedisReader.Server.Services.ValueTypes
{
    public class SortedSetType : IType
    {
        public IEnumerable<string> Value { get; }

        public SortedSetType(IEnumerable<SortedSetEntry> sortedSetScan)
        {
            Value = sortedSetScan.Select(x => (string)x.Element);
        }
    }
}