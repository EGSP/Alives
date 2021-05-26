using System;
using Egsp.Core;
using Egsp.CSharp;

namespace Alive
{
    public class EntityComponent : Component
    {
        /// <summary>
        /// Родительская сущность.
        /// </summary>
        public Entity Entity { get; private set; }

        protected override void OnDecoratorSetInternal()
        {
            if (Decorator is Entity entity)
            {
                Entity = entity;
                OnEntitySetInternal();
            }
            else
            {
                throw new InvalidOperationException(
                    "Компонент сущности может быть добавлен только на сущность.");
            }
        }

        /// <summary>
        /// Вызывается после установки родительской сущности.
        /// </summary>
        protected virtual void OnEntitySetInternal()
        {
        }
    }
}