using UnityEngine;

namespace Alive
{
    public class CubeEntity : WorldEntity
    {
        [SerializeField] private float health = 10;

        public override void UpdateRaw()
        {
            base.UpdateRaw();

            transform.rotation *= Quaternion.Euler(0, 50 * Time.deltaTime, 0);
            
            ReduceHealth();
            
            Die();
        }

        private void ReduceHealth()
        {
            health -= 2 * Time.deltaTime;
        }

        private void Die()
        {
            if (health <= 0)
                DestroyImmediate(gameObject);
        }
    }
}