using Csv.Annotations;
using MasterMemory;
using MessagePack;

namespace WhiteWorld.Domain.Entity.Runtime.Domain.Entity
{
    [CsvObject]
    [MemoryTable("DummyData"), MessagePackObject(true)]
    public partial record DummyModel : IIdHolder
    {
        [PrimaryKey]
        [Column(0)]
        public string Id { get; init; }

        [Column(1)]
        public string DummyText { get; init; }
    }
}