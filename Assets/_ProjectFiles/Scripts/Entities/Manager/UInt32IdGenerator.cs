using System.Collections.Generic;

namespace Alive
{
    /// <summary>
    /// Поддерживает 4,294,967,295 сущностей.
    /// </summary>
    public class UInt32IdGenerator
    {
        /// <summary>
        /// Все свободные идентификаторы.
        /// </summary>
        private Queue<UInt32Id> _freeIds = new Queue<UInt32Id>();
        
        /// <summary>
        /// Последний естественный использованный идентификатор. Получаемый, при увеличении числа.
        /// </summary>
        private UInt32Id _lastId = UInt32Id.Create(0);

        /// <summary>
        /// Возвращает доступный идентификатор.
        /// Может быть взят из пула идентификаторов или следующим числом.
        /// </summary>
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
        /// <para>Складывает переданный идентификатор в пул идентификаторов.</para>
        /// <para>Полезно использовать для недопущения переполнения идентификаторов.</para>
        /// </summary>
        public void PoolId(UInt32Id id) => _freeIds.Enqueue(id);

        /// <summary>
        /// Очищает пул идентификаторов и сбрасывает счетчик идентификаторов.
        /// </summary>
        public void Reset()
        {
            _freeIds.Clear();
            _lastId = UInt32Id.Create(1);
        }

        /// <summary>
        /// Возвращает объект-мементо данного генератора для последующего сохранения.
        /// </summary>
        public UInt32IdGeneratorMemento Mem()
        {
            return new UInt32IdGeneratorMemento(this);
        }

        /// <summary>
        /// Восстанавливает сохраненное состояние.
        /// </summary>
        public void Restore(UInt32IdGeneratorMemento memento)
        {
            Reset();

            _freeIds = new Queue<UInt32Id>(memento.FreeIds);
            _lastId = memento.LastId;
        }
        
        public readonly struct UInt32IdGeneratorMemento
        {
            public readonly UInt32Id[] FreeIds;
            public readonly UInt32Id LastId;

            public UInt32IdGeneratorMemento(UInt32Id[] freeIds, UInt32Id lastId)
            {
                FreeIds = freeIds;
                LastId = lastId;
            }

            public UInt32IdGeneratorMemento(UInt32IdGenerator generator)
            {
                FreeIds = generator._freeIds.ToArray();
                LastId = generator._lastId;
            }
        }
    }
}