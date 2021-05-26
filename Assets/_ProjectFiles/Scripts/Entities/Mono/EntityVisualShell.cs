using Egsp.Core;
using UnityEngine;
using Component = Egsp.CSharp.Component;

namespace Alive
{
    public class EntityVisualShell : MonoBehaviour
    {
        public EventBus Bus { get; set; } = new EventBus();

        private void OnDestroy()
        {
            Bus.Raise<VisualShellComponent>(x=>x.OnShellDestroy());
        }
    }
}