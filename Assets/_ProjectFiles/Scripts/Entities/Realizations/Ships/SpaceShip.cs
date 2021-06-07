using Alive.Behaviours;
using Egsp.Core;
using UnityEngine;

namespace Alive.Entities
{
    public class SpaceShip : BehaviourEntity<SpaceShip>
    {
        public float Scale { get; private set; }

        public SpaceShip(float scale) : base()
        {
            Scale = scale;
        }

        protected override void OnAddVisualShell(NotNull<VisualShellComponent> visualShell)
        {
            visualShell.Value.Mono.transform.localScale = new Vector3(Scale, Scale, Scale);
        }
    }
}