using System;
using Egsp.Core;
using Egsp.CSharp;
using UnityEngine;

namespace Alive
{
    // Здесь определены базовые компоненты всех сущностей.
    // COMPONENTS
    public abstract partial class Entity
    {
        [Obsolete("Данный компонент лучше изменить на полноценный transform, имеющий данные о вращении." +
                  " Однако, если не учитывать вращение, то данного компонента достаточно.")]
        private PositionComponent _positionComponent;
        
        /// <summary>
        /// Компонент визульной оболочки сущности.
        /// </summary>
        private VisualShellComponent _visualShell;

        /// <summary>
        /// Привязана ли визуальная оболочка к сущности.
        /// </summary>
        public bool HasVisual => _visualShell.HasMono;

        /// <summary>
        /// Компонент визульной оболочки сущности.
        /// </summary>
        public VisualShellComponent VisualShell
        {
            get => _visualShell;
            private set
            {
                if (value == null)
                    throw new NotImplementedException();
                
                _visualShell = value;
                _visualShell.OnMono += () => OnAddVisualShell(VisualShell);
                // Если у визуальной оболочки уже есть реальное отображение.
                if (_visualShell.HasMono)
                {
                    OnAddVisualShell(_visualShell);
                }
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

        private void AddBaseComponents()
        {
            AddComponent(new PositionComponent());
            AddComponent(new VisualShellComponent());
        }

        /// <summary>
        /// Получаем ссылки на нужные нам компоненты.
        /// </summary>
        protected override void OnAddComponentInternal(IComponent component)
        {
            // Данный компонент должен быть добавлен только однажды.
            if (component is VisualShellComponent visualShellComponent)
            {
                VisualShell = visualShellComponent;
                return;
            }

            if (component is PositionComponent positionComponent)
            {
                _positionComponent = positionComponent;
                return;
            }
        }

        /// <summary>
        /// Вызывается при добавлении визуальной оболочки.
        /// </summary>
        protected virtual void OnAddVisualShell(NotNull<VisualShellComponent> visualShell){}
    }
}