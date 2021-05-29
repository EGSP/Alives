using Egsp.Core;
using UnityEngine;

namespace Alive.Entities
{
    public class SpaceShip : BehaviourEntity<SpaceShip>
    {
        public float Scale { get; private set; }

        public SpaceShip(UInt32Id id, float scale) : base(id)
        {
            Scale = scale;
        }

        protected override void OnAddVisualShell(NotNull<VisualShellComponent> visualShell)
        {
            visualShell.Value.Shell.transform.localScale = new Vector3(Scale, Scale, Scale);
        }
    }
}