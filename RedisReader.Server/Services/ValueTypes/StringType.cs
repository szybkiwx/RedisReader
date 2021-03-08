using StackExchange.Redis;

namespace RedisReader.Server.Services.ValueTypes
{
    public class StringType : IType
    {
        public string Value { get; }
        
        public StringType(RedisValue str)
        {
            Value = str;
        }
    }
}