using System;

namespace WhiteWorld.Domain.Entity
{
    /// <summary>
    /// 一時的なIDを保持する構造体.
    /// ジェネリックメソッド以外で<see cref="IIdHolder"/>として使うとアロケーションすることに注意.
    /// </summary>
    public readonly struct TempIdHolder : IIdHolder
    {
        public string Id { get; }

        /// <summary>
        /// Initialize a new instance of <see cref="TempIdHolder"/> with the specified ID.
        /// </summary>
        /// <param name="id"></param>
        public TempIdHolder(in string id) => Id = id;

        /// <inheritdoc/>
        int IComparable<IIdHolder>.CompareTo(IIdHolder other) // アロケーション回避のためしっかり実装.
        {
            if (other == null)
            {
                return 1;
            }
            return (IIdHolder.ExtractDigit(Id), IIdHolder.ExtractDigit(other.Id)) switch
            {
                var (a, b) when a < b => -1,
                var (a, b) when a > b => 1,
                _ => 0
            };
        }
    }
}