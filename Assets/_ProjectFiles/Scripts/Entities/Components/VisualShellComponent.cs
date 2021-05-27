using System;
using UnityEngine;

namespace Alive
{
    /// <summary>
    /// Хранит ссылку на визуальную оболочку объекта.
    /// </summary>
    public class VisualShellComponent : EntityComponent, IAwakeComponent, IUpdateComponent
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
            Debug.Log("SetOwner");
            VisualShell.Owner = new WeakReference<Entity>(Entity);
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

        public void Update(in UpdateData u)
        {
            Debug.Log("UPDATE COMPONENT!");
            VisualShell.transform.rotation *= Quaternion.Euler(0, 50 * u.deltaTime, 0);
        }
    }
}