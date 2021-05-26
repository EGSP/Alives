using System.Collections.Generic;
using UnityEngine;

namespace Alive
{
    public class SphereRealZone : RealZone
    {
        [SerializeField] private float radius;
        
        public override void CheckEntities(IEnumerable<WorldManager.EntityWrap> entityWraps)
        {
            foreach (var entityWrap in entityWraps)
            {
                var distance = (entityWrap.Entity.Position - transform.position).magnitude;

                if (distance <= radius)
                {
                    Debug.Log("Zone true");
                    entityWrap.InRealZone.True();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}