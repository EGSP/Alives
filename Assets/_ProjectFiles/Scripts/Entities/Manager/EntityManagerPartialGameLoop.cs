using System;
using Egsp.Core;
using UnityEngine;

namespace Alive
{
    public partial class EntityManager
    {
        /// <summary>
        /// Запустился ли игровой мир.
        /// </summary>
        private bool _awaked = false;

        public void Awake()
        {
            if (_awaked)
                throw new InvalidOperationException("Second EntityManager Awake call.");
            
            _awaked = true;
            
            // Awake all existing entities
            foreach (var pair in _entities)
            {
                pair.Value.ExternalAwake();
            }
        }

        private void EntityAwakeInternal(NotNull<Entity> entity)
        {
            if(_awaked)
                entity.Value.ExternalAwake();
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;
            var updateData = new UpdateData(deltaTime);

            foreach (var pair in _entities)
            {
                pair.Value.ExternalUpdate(in updateData);
            }
         
            // Уничтожаем все сущности, запросившие уничтожение.
            DestroyAllEntitiesInternal();
        }
    }
}