using System;
using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Data
{
    public class MasterDataRepository<T> : IMasterDataRepository<T> where T : class, IIdHolder
    {
        public ReadOnlyMemory<T> Entities { get; }

        public T Find(string id)
        {
            var entities = Entities.Span;
            var index = Entities.Span.BinarySearch(new TempIdHolder(id));
            return index >= 0 ? entities[index] : null;
        }

        public MasterDataRepository(string binaryPath, string tableName)
        {
            var entities = BinaryLoader.Load<T>(binaryPath, tableName).GetRawDataUnsafe();
            Array.Sort(entities);
            Entities = entities;
        }
    }
}