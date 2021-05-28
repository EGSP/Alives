using System;
using UnityEngine;

namespace Alive
{
    /// <summary>
    /// Хранит ссылку на визуальную оболочку объекта.
    /// </summary>
    public class VisualShellComponent : EntityComponent
    {
        /// <summary>
        /// Текущая оболочка объекта.
        /// </summary>
        public readonly EntityVisualShell Shell;

        /// <summary>
        /// Вызывается при удалении визуальной оболочки.
        /// </summary>
        public event Action<VisualShellComponent> OnVisualShellDestroyed = delegate { };

        public VisualShellComponent(EntityVisualShell shell)
        {
            Shell = shell;
        }

        protected override void OnEntitySetInternal()
        {
            Debug.Log("SetOwner");
            Shell.Owner = new WeakReference<Entity>(Parent);
            Shell.transform.position = Parent.Position;
        }

        public void OnShellDestroy()
        {
            OnVisualShellDestroyed(this);
            Destroy();
        }
    }
}