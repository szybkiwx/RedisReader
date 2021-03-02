using System;

namespace RedisReader.Server.Models
{
    public class RedisConnection
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public bool UseTsl { get; set; }
        public string Endpoint { get; set; }
    }
}