namespace Alive
{
    /// <summary>
    /// Структура идентификатора с диапазоном от 0 до 4,294,967,295.
    /// </summary>
    public readonly struct UInt32Id
    {
        public readonly uint Value;

        private UInt32Id(uint value)
        {
            Value = value;
        }

        /// <summary>
        /// Возвращает следующий идентификатор. (Value+1)
        /// </summary>
        /// <returns></returns>
        public UInt32Id NextId() => Value + 1;
        
        public static UInt32Id Create(uint id) => new UInt32Id(id);

        public static implicit operator UInt32Id(uint value) => new UInt32Id(value);

        public static bool operator ==(UInt32Id a, UInt32Id b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(UInt32Id a, UInt32Id b)
        {
            return !(a.Value == b.Value);
        }

        public override bool Equals(object obj) => Value.Equals(obj);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}