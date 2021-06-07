using System;
using Alive.Mono;
using Egsp.Core;
using Egsp.Core.Ui;
using UnityEngine;

namespace Alive
{
    /// <summary>
    /// Хранит ссылку на визуальную оболочку объекта.
    /// </summary>
    public class VisualShellComponent : EntityComponent
    {
        /// <summary>
        /// Вызывается полсе установки визуальной оболочки.
        /// </summary>
        public event Action OnMono = delegate { };

        /// <summary>
        /// Текущая реальная оболочка объекта.
        /// </summary>
        private VisualShellMono _mono;

        /// <summary>
        /// Существует ли оболочка на данный момент.
        /// </summary>
        public bool HasMono => _mono == null;

        /// <summary>
        /// Текущая реальная оболочка объекта.
        /// </summary>
        public VisualShellMono Mono
        {
            get => _mono;
            set
            {
                if (_mono != null)
                    throw new InvalidOperationException();
                
                _mono = value;
                if (_mono != null)
                    OnMono();
            }
        }

        protected override void OnEntitySetInternal()
        {
            Mono.Owner = new WeakReference<Entity>(Parent);
            Mono.transform.position = Parent.Position;
        }
    }
}