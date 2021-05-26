using System;
using System.Collections;
using System.Collections.Generic;
using Egsp.Core;
using Egsp.Extensions.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Alive
{
    public class WorldManager : Singleton<WorldManager>
    {
        [SerializeField] private int entitiesCount;
        
        private LinkedList<EntityWrap> _entities = new LinkedList<EntityWrap>();
        private List<IRealZone> _realZones = new List<IRealZone>();

        public void Update()
        {
            UpdateZones();
            UpdateEntities();
            ClearDestroyedEntities();
        }

        private void UpdateZones()
        {
            if (_entities.Count <= 0)
                return;
            
            // Проход по всем зонам.
            for (var i = 0; i < _realZones.Count; i++)
            {
                _realZones[i].CheckEntities(_entities);
            }

            // Проход по всем сущностям.
            foreach (var entityWrap in _entities)
            {
                entityWrap.InRealZone.Result();
                UpdateToZone(entityWrap);
            }
        }

        private void UpdateToZone(EntityWrap entityWrap)
        {
            if (entityWrap.InRealZone.State == BoolState.Same)
                return;
            
            if(entityWrap.InRealZone.Value == false)
                UpdateToVirtual(entityWrap);
            else
                UpdateToReal(entityWrap);
        }

        private void UpdateToVirtual(EntityWrap entityWrap)
        {
            Debug.Log($"{entityWrap.Entity.Position} object virtualized.");
        }

        private void UpdateToReal(EntityWrap entityWrap)
        {
            
        }

        private void UpdateEntities()
        {
            foreach (var entityWrap in _entities)
            {
                if (!entityWrap.Destroyed)
                    entityWrap.Entity.UpdateRaw();
                
                entityWrap.CheckForDestroy();
            }
        }
        
        private void ClearDestroyedEntities()
        {
            _entities.RemoveAll(x => x.Destroyed);
            UpdateCounter();
        }

        public void RegisterEntity(IWorldEntity worldEntity)
        {
            worldEntity.CustomUpdateLoopUse = true;
            _entities.AddLast(new EntityWrap(worldEntity));

            UpdateCounter();
        }

        public void UnregisterEntity(IWorldEntity worldEntity)
        {
            worldEntity.CustomUpdateLoopUse = false;
            _entities.Remove(x => x.Entity == worldEntity);

            UpdateCounter();
        }

        private void UpdateCounter()
        {
            entitiesCount = _entities.Count;
        }

        public void AddZone(IRealZone zone) => _realZones.Add(zone);
        public void RemoveZone(IRealZone zone) => _realZones.Remove(zone);

        public class EntityWrap
        {
            public bool Destroyed { get; private set; }

            public CountedBool InRealZone;

            public IWorldEntity Entity { get; set; }

            public EntityWrap(IWorldEntity entity)
            {
                Entity = entity;
            }

            public void CheckForDestroy()
            {
                var unityObj = Entity as Object;
                Destroyed = unityObj == null;
                
                Debug.Log($"Destroyed {Destroyed}");
            }
        }

        public struct CountedBool
        {
            /// <summary>
            /// Количество голосов True.
            /// </summary>
            public int Count;

            public bool Value { get; private set; }
            private bool PreviousValue { get; set; }

            public BoolState State => Value == PreviousValue ? BoolState.Same : BoolState.Changed;
            
            public void True() => Count++;
            
            /// <summary>
            /// Сбрасывает голоса и возвращает полученный результат.
            /// </summary>
            /// <param name="min">Минимум голосов для True.</param>
            public (bool,BoolState) Result(int min = 0)
            {
                Value = Count > min;
                Count = 0;
                
                Debug.Log($"{Value} - {Count}");
                
                UpdateState();
                return (Value,State);
            }

            private void UpdateState()
            {
                PreviousValue = Value;
            }
        }

        public enum BoolState
        {
            Changed,
            Same
        }
    }
}
