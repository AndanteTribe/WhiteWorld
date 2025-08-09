using System;
using System.Runtime.CompilerServices;

namespace WhiteWorld.Domain.Entity
{
    public interface IIdHolder : IComparable<IIdHolder>
    {
        string Id { get; }

        int IComparable<IIdHolder>.CompareTo(IIdHolder other)
        {
            if (other == null)
            {
                return 1;
            }
            return (ExtractDigit(Id), ExtractDigit(other.Id)) switch
            {
                var (a, b) when a < b => -1,
                var (a, b) when a > b => 1,
                _ => 0
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int ExtractDigit(in ReadOnlySpan<char> id)
        {
            var digit = id;
            for (var i = id.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(id[i]))
                {
                    digit = id[(i + 1)..];
                    break;
                }
            }
            // ここで例外条件は全てエラーをはく;空文字ID or 数字が語尾から含まれないID
            return int.Parse(digit);
        }
    }
}