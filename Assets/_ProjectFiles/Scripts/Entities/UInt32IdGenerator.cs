using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Alive
{
    /// <summary>
    /// Поддерживает 4,294,967,295 сущностей.
    /// </summary>
    public class UInt32IdGenerator
    {
        private Queue<UInt32Id> _freeIds = new Queue<UInt32Id>();
        
        private UInt32Id _lastId = UInt32Id.Create(0);

        public UInt32Id GetNextId()
        {
            // Если нет свободных идентификаторов, то создаем новый.
            if (_freeIds.Count == 0)
            {
                _lastId = _lastId.NextId();
                return _lastId;
            }
            // Иначе достаем первый свободный.
            else
            {
                return _freeIds.Dequeue();
            }
        }

        /// <summary>
        /// Складывает переданный идентификатор в пул идентификаторов.
        /// </summary>
        public void PoolId(UInt32Id id) => _freeIds.Enqueue(id);

        public void Reset()
        {
            _freeIds.Clear();
            _lastId = UInt32Id.Create(1);
        }
    }

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