using System;
using Egsp.Core;
using UnityEngine;

namespace Alive.Mono
{
    public class VisualShellMono : MonoBehaviour
    {
        /// <summary>
        /// Владелец данной оболочки.
        /// </summary>
        public WeakReference<Entity> Owner { get; set; }

        // Проверяем существование сущности-носителя.
        public void LateUpdate()
        {
            if (Owner == null)
                return;

            // Если сущности нет, то уничтожаем визуальную оболочку.
            if (Owner.TryGetTarget(out _) == false)
            {
                Destroy();
            }
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}