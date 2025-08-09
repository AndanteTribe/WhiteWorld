using System;
using Csv.Annotations;
using MasterMemory;
using MessagePack;

namespace WhiteWorld.Domain.Entity
{
    [CsvObject]
    [MemoryTable("KeywordData"), MessagePackObject(true)]
    public partial record KeywordModel : IIdHolder
    {
        [PrimaryKey]
        [Column(0)]
        public string Id { get; init; }

        [Column(1)]
        public string KeywordText { get; init; }

        [MessagePack.IgnoreMember]
        [Csv.Annotations.IgnoreMember]
        public ReadOnlyMemory<DummyModel> DummyData { get; set; }
    }
}