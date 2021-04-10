using System;

namespace RedisReader.Server.Models
{
    public class ProtoDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}