using System;

namespace WhiteWorld.Domain.Entity
{
    /// <summary>
    /// マス目移動量
    /// </summary>
    public readonly struct SpaceAmount : IEquatable<SpaceAmount>, IComparable<SpaceAmount>
    {
        /// <summary>
        /// 移動量
        /// </summary>
        private readonly int _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceAmount"/> struct.
        /// </summary>
        /// <param name="value">移動量.</param>
        public SpaceAmount(int value) => _value = value;

        /// <inheritdoc/>
        public bool Equals(SpaceAmount other) => _value == other._value;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is SpaceAmount other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() => _value;

        /// <inheritdoc/>
        public override string ToString() => _value.ToString();

        public static explicit operator int(in SpaceAmount amount) => amount._value;
        public static explicit operator SpaceAmount(in int value) => new SpaceAmount(value);

        public static bool operator ==(in SpaceAmount left, in SpaceAmount right) => left.Equals(right);
        public static bool operator !=(in SpaceAmount left, in SpaceAmount right) => !left.Equals(right);

        public int CompareTo(SpaceAmount other) => _value.CompareTo(other._value);
        public static bool operator >(in SpaceAmount left, in SpaceAmount right) => left._value > right._value;
        public static bool operator <(in SpaceAmount left, in SpaceAmount right) => left._value < right._value;
        public static bool operator >=(in SpaceAmount left, in SpaceAmount right) => left._value >= right._value;
        public static bool operator <=(in SpaceAmount left, in SpaceAmount right) => left._value <= right._value;
    }
}