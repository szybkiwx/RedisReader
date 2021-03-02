using System.Collections.Generic;

namespace RedisReader.Server.Services
{
    public interface IReadFromRedis
    {
        List<string> GetKeys();
    }

    public class RedisReader : IReadFromRedis
    {
    }
}