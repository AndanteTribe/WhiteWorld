using System;
using System.Collections;
using System.Collections.Generic;
using Csv.Annotations;
using MasterMemory;
using MessagePack;

namespace WhiteWorld.Domain.Entity
{
    [CsvObject]
    [MemoryTable("MessageData"), MessagePackObject(true)]
    public partial record MessageModel : IIdHolder
    {
        [PrimaryKey]
        [Column(0)]
        public string Id { get; init; }

        [Column(1)]
        public string Name { get; init; }

        [Column(2)]
        public string Message { get; init; }
    }
}