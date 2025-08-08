using WhiteWorld.Domain;
using ZLinq;
using ZLinq.Linq;

namespace WhiteWorld.Data.Runtime.Data
{
    public class MasterDataRepository<T> : IMasterDataRepository<T> where T : class, IIdHolder
    {
        public ValueEnumerable<FromArray<T>, T> Entities => _entities.AsValueEnumerable();
        protected T[] _entities;

        public T Find(string id) =>
            _entities
                .AsValueEnumerable()
                .FirstOrDefault(x => x.Id == id);

        public MasterDataRepository(string binaryPath, string tableName)
            => _entities = BinaryLoader.Load<T>(binaryPath, tableName).GetRawDataUnsafe();
    }
}