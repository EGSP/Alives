using System;
using UnityEngine;

namespace Alive
{
    /// <summary>
    /// Хранит ссылку на визуальную оболочку объекта.
    /// </summary>
    public class VisualShellComponent : EntityComponent, IAwakeComponent
    {
        /// <summary>
        /// Текущая оболочка объекта.
        /// </summary>
        public readonly EntityVisualShell VisualShell;

        /// <summary>
        /// Вызывается при удалении визуальной оболочки.
        /// </summary>
        public event Action<VisualShellComponent> OnVisualShellDestroyed = delegate { };

        public VisualShellComponent(EntityVisualShell visualShell)
        {
            VisualShell = visualShell;
        }

        protected override void OnEntitySetInternal()
        {
            VisualShell.transform.position = Entity.Position;
        }

        public void OnShellDestroy()
        {
            OnVisualShellDestroyed(this);
            Destroy();
        }

        public void Awake()
        {
            Debug.Log("AWAKENED COMPONENT!");
        }
    }
}