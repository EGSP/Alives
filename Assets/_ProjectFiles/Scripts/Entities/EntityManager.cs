using System;
using System.Collections.Generic;
using Egsp.Core;

namespace Alive
{
    [LazyInstance(false)]
    public partial class EntityManager : SingletonRaw<EntityManager>
    {
        private UInt32IdGenerator _idGenerator = new UInt32IdGenerator();

        // Все сущности.
        private Dictionary<UInt32Id, Entity> _entities = new Dictionary<UInt32Id, Entity>();
        
        // Сущности, которые должны быть уничтожены.
        private Queue<Entity> _entitiesToDestroy = new Queue<Entity>();

        public UInt32Id RequestEntityId()
        {
            return _idGenerator.GetNextId();
        }

        /// <summary>
        /// Добавляет сущность на дальнейшую обработку игрового цикла.
        /// </summary>
        public void ProcessEntity(Option<Entity> entity)
        {
            if (entity.IsNone)
                return;

            var entityValue = entity.Value;
            
            PreventCollision(entityValue); // throws exception if collision exist.
            EntityAddInternal(entityValue);
            EntityAwakeInternal(entityValue);
        }

        private void EntityAddInternal(NotNull<Entity> entity)
        {
            _entities.Add(entity.Value.Id, entity.Value);
        }
        
        /// <summary>
        /// Уничтожает сущность.
        /// Если сущность была в добавлена ранее, то также будет освобожден идентификатор (полезно).
        /// </summary>
        public void DestroyEntity(Option<Entity> entity)
        {
            if (entity.IsNone)
                return;
            
            if (ExistInCollection(entity.Value))
            {
                _entities.Remove(entity.Value.Id);
                // Освобождаем идентификатор.
                _idGenerator.PoolId(entity.Value.Id);
            }

            _entitiesToDestroy.Enqueue(entity.Value);
        }

        private void DestroyEntityInternal(NotNull<Entity> entity)
        {
            entity.Value.Destroy();
        }

        private void DestroyAllEntitiesInternal()
        {
            while (_entitiesToDestroy.Count > 0)
            {
                var dequeued = _entitiesToDestroy.Dequeue();
                dequeued.Destroy();
            }
        }

        private bool ExistInCollection(Entity entity)
        {
            if (_entities.ContainsKey(entity.Id))
            {
                var collectionEntity = _entities[entity.Id];
                return entity.Equals(collectionEntity);
                
            }
            return false;
        }

        /// <summary>
        /// Вызывает исключение при коллизии.
        /// </summary>
        /// <exception cref="IdCollisionException"></exception>
        /// <exception cref="EntityCollisionException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        private void PreventCollision(Entity newEntity)
        {
            if (_entities.ContainsKey(newEntity.Id))
            {
                var oldEntity = _entities[newEntity.Id];
                switch (newEntity.EqualsType(oldEntity))
                {
                    case EntityEqualsType.EqualInstance: 
                        throw new EntityCollisionException(newEntity);
                    case EntityEqualsType.EqualId:
                        throw new IdCollisionException(newEntity, oldEntity);
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        private class IdCollisionException : Exception
        {
            public readonly Entity NewEntity;
            public readonly Entity OldEntity;

            public IdCollisionException(Entity newEntity, Entity oldEntity)
                : base($"Entity ID collision - new: {newEntity.Id}, old: {oldEntity.Id}")
            {
                NewEntity = newEntity;
                OldEntity = oldEntity;
            }
        }

        private class EntityCollisionException : Exception
        {
            public readonly Entity Entity;

            public EntityCollisionException(Entity entity)
                : base("Same entity register 2 times!")
            {
                Entity = entity;
            }
        }
    }
}