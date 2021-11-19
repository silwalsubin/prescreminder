using System;

namespace contracts.Persistence
{
    public interface IGenericRecord
    {
        DateTime CreatedDateUtc { get; set; }
        DateTime ModifiedDateUtc { get; set; }
    }
}
