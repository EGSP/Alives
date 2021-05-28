using System;
using Egsp.Core;
using Egsp.CSharp;
using UnityEngine;

namespace Alive
{
    public abstract partial class Entity : DecoratorBase
    {
        public readonly UInt32Id Id;
        
        private PositionComponent _positionComponent;
        
        private VisualShellComponent _visualShellComponent;

        private VisualShellComponent VisualShellComponent
        {
            get => _visualShellComponent;
            set
            {
                _visualShellComponent = value;
                _visualShellComponent.OnVisualShellDestroyed += (shell) => _visualShellComponent = null;
            }
        }

        /// <summary>
        /// Текущая позиция сущности в мире.
        /// </summary>
        public Vector3 Position
        {
            get => _positionComponent.Position;
            set => _positionComponent.Position = value;
        }
        
        /// <summary>
        /// Привязана ли визуальная оболочка к сущности.
        /// </summary>
        public Option<VisualShellComponent> HasVisual =>
            VisualShellComponent == null ? Option<VisualShellComponent>.None : VisualShellComponent;
        
        protected Entity(UInt32Id id)
        {
            Id = id;
            AddComponent(new PositionComponent());
            EntityManager.Instance.ProcessEntity(this);
        }

        protected override void OnAddComponentInternal(IComponent component)
        {
            if (component is VisualShellComponent visualShellComponent)
            {
                if (_visualShellComponent != null)
                    ThrowTwoInstanceComponent(typeof(VisualShellComponent));

                VisualShellComponent = visualShellComponent;
                OnAddVisualShell(VisualShellComponent);
                return;
            }

            if (component is PositionComponent positionComponent)
            {
                if (_positionComponent != null)
                    ThrowTwoInstanceComponent(typeof(PositionComponent));

                _positionComponent = positionComponent;
                return;
            }
        }
        
        protected virtual void OnAddVisualShell(NotNull<VisualShellComponent> visualShell){}

        protected void ThrowTwoInstanceComponent(Type type) => throw new InvalidOperationException(
            $"У объекта не может быть два {type.Name} компонента.");

        public EntityEqualsType EqualsType(Entity entity)
        {
            if (Equals(entity))
            {
                return EntityEqualsType.EqualInstance;
            }
            else
            {
                if (entity == null)
                    return EntityEqualsType.NotEqual;
                
                if (entity.Id == this.Id)
                    return EntityEqualsType.EqualId;   
            }

            return EntityEqualsType.NotEqual;
        }
    }


    public enum EntityEqualsType
    {
        /// <summary>
        /// Одна и та же сущность.
        /// </summary>
        EqualInstance,
        /// <summary>
        /// Разные сущности с одинаковым идентификатором.
        /// </summary>
        EqualId,
        /// <summary>
        /// Разные сущности.
        /// </summary>
        NotEqual
    }
}