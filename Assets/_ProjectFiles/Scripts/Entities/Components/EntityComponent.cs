using System;
using Egsp.Core;
using Egsp.CSharp;

namespace Alive
{
    public class EntityComponent : Component
    {
        public Entity Parent { get; private set; }

        protected override void OnDecoratorSetInternal()
        {
            if (Decorator is Entity entity)
            {
                Parent = entity;
                OnEntitySetInternal();
            }
            else
            {
                ThrowInvalidDecorator();
            }
        }

        /// <summary>
        /// Вызывается после установки родительской сущности.
        /// </summary>
        protected virtual void OnEntitySetInternal()
        {
        }
        
        protected void ThrowInvalidDecorator() => throw new InvalidOperationException(
            "Компонент сущности может быть добавлен только на сущность.");
    }

    public class EntityComponent<TEntity> : EntityComponent
    {
        public new TEntity Parent { get; private set; }

        protected override void OnDecoratorSetInternal()
        {
            if (Decorator is TEntity entity)
            {
                Parent = entity;
                OnEntitySetInternal();
            }
            else
            {
                ThrowInvalidTypeDecorator();
            }
        }
        
        protected void ThrowInvalidTypeDecorator() => throw new InvalidOperationException(
                $"Компонент сущности может быть добавлен только на сущность типа {typeof(TEntity)}.");
    }
}