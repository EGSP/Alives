using UnityEngine;

namespace Alive
{
    public abstract class WorldEntity : MonoBehaviour, IWorldEntity
    {
        public Vector3 Position => transform.position;
        
        public bool CustomUpdateLoopUse { get; set; }

        protected virtual void Update()
        {
            if (CustomUpdateLoopUse)
                return;
            
            UpdateRaw();
        }

        public virtual void UpdateRaw()
        {
        }

        public virtual void ToReal()
        {
            
        }

        public virtual void ToVirtual()
        {
        }

        public void UseCustomUpdateLoop(bool use)
        {
            CustomUpdateLoopUse = use;
        }
    }
}