using System.Collections.Generic;
using ZLinq;
using ZLinq.Linq;

namespace WhiteWorld.Domain
{
    public interface IMasterDataRepository<T> where T : IIdHolder
    {
        ValueEnumerable<FromArray<T>, T> Entities { get; }
        T Find(string id);

    }
}