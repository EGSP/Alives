using System;
using Egsp.Core;
using Egsp.CSharp;
using UnityEngine;
using Component = Egsp.CSharp.Component;

namespace Alive
{
    public abstract partial class Entity : DecoratorBase
    {
        protected Entity()
        {
            AddComponent(new PositionComponent());
        }
        
        // Groups of components.
        private ComponentGroup<IAwakeComponent> _awakeGroup;
        private ComponentGroup<IUpdateComponent> _updateGroup;
        
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

        protected override void OnAddComponentInternal(IComponent component)
        {
            if (component is VisualShellComponent visualShellComponent)
            {
                if (_visualShellComponent != null)
                    ThrowTwoInstanceComponent(typeof(VisualShellComponent));

                VisualShellComponent = visualShellComponent;
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

        protected override void OnGroupComponent(IComponent component)
        {
            var groupAwake = 
                Group<IAwakeComponent>(component);

            var groupUpdate =
                Group<IUpdateComponent>(component);
            
            if (groupAwake.IsSome)
                _awakeGroup = groupAwake.Value;

            if (groupUpdate.IsSome)
                _updateGroup = groupUpdate.Value;
        }

        protected void ThrowTwoInstanceComponent(Type type) => throw new InvalidOperationException(
            $"У объекта не может быть два {type.Name} компонента.");
    }

    public sealed class PositionComponent : Component
    {
        public Vector3 Position;
    }
}