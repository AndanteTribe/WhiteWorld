using Csv.Annotations;
using MasterMemory;
using MessagePack;
using WhiteWorld.Domain;

namespace WhiteWorld.Data.Runtime.Data
{
    [CsvObject]
    [MemoryTable("PlayerData"), MessagePackObject(true)]
    public partial class PlayerRepository : IPlayerRepository
    {
        [PrimaryKey]
        [Column(0)]
        public string Name { get; init; }
        [Column(1)]
        public int Age { get; init; }
    }
}