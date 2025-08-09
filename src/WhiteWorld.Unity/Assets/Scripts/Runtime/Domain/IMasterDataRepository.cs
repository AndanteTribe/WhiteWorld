using System;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain
{
    public interface IMasterDataRepository<T> where T : IIdHolder
    {
        ReadOnlyMemory<T> Entities { get; }
        T Find(string id);
    }
}