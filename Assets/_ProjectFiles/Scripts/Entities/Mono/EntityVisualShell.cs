using System;
using Egsp.Core;
using UnityEngine;
using Component = Egsp.CSharp.Component;

namespace Alive
{
    public class EntityVisualShell : MonoBehaviour
    {
        public EventBus Bus { get; private set; } = new EventBus();

        public WeakReference<Entity> Owner { get; set; }
        private Entity _owner;
        
        public void LateUpdate()
        {
            // Virtual component is not initialized yet.
            if (Owner == null)
                return;
            
            if (Owner.TryGetTarget(out _owner) == false)
            {
                Debug.Log("No owner");
                Destroy();
            }
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
        
        private void OnDestroy()
        {
            Bus.Raise<VisualShellComponent>(x=>x.OnShellDestroy());
        }
    }
}