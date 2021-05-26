using System.Collections.Generic;
using UnityEngine;

namespace Alive
{
    public abstract class RealZone : MonoBehaviour, IRealZone
    {
        private void Awake()
        {
            WorldManager.Instance.AddZone(this);
        }

        public abstract void CheckEntities(IEnumerable<WorldManager.EntityWrap> entityWraps);
    }
}