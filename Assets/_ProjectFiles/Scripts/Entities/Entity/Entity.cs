using System;
using Egsp.Core;
using Egsp.CSharp;
using UnityEngine;

namespace Alive
{
    /// <summary>
    /// Базовый класс для игровых сущностей.
    /// </summary>
    public abstract partial class Entity : DecoratorBase
    {
        /// <summary>
        /// Идентификатор сущности. 
        /// </summary>
        public readonly UInt32Id Id;

        /// <summary>
        /// Текущая форма обновления сущности. 
        /// </summary>
        public EntityForm Form { get; set; } = EntityForm.Real;

        protected Entity()
        {
            Id = RequestId();

            AddBaseComponents();
            ApplyManager();
        }

        /// <summary>
        /// Запрашиваем идентификатор у менеджера.
        /// </summary>
        /// <returns></returns>
        private UInt32Id RequestId()
        {
            return EntityManager.Instance.RequestEntityId();
        }

        /// <summary>
        /// Сообщаем менеджеру о своем существовании.
        /// </summary>
        private void ApplyManager()
        {
            EntityManager.Instance.ProcessEntity(this); 
        }

        /// <summary>
        /// Возвращает тип равенства двух сущностей: текущей и переданной.
        /// </summary>
        public EntityEquality EqualityType(Entity entity)
        {
            if (Equals(entity))
            {
                return EntityEquality.Instance;
            }
            else
            {
                if (entity == null)
                    return EntityEquality.No;
                
                if (entity.Id == this.Id)
                    return EntityEquality.Id;   
            }

            return EntityEquality.No;
        }
    }
    
    public enum EntityEquality
    {
        /// <summary>
        /// Одна и та же сущность.
        /// </summary>
        Instance,
        /// <summary>
        /// Разные сущности с одинаковым идентификатором.
        /// </summary>
        Id,
        /// <summary>
        /// Разные сущности.
        /// </summary>
        No
    }
}