using UnityEngine;

namespace Alive
{
    public class CubeEntityFactory : MonoBehaviour
    {
        [SerializeField] private CubeEntity cubeEntity;

        [SerializeField] private Vector3 firstCorner;
        [SerializeField] private Vector3 secondCorner;

        [SerializeField] private float height;
        [SerializeField] private float heightOffset;

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                SpawnRandomPosition();
        }

        public void Spawn(Vector3 position)
        {
            var instance = Instantiate(cubeEntity, position, Quaternion.identity);

            WorldManager.Instance.RegisterEntity(instance);
        }

        public void SpawnRandomPosition()
        {
            var x = Mathf.Lerp(firstCorner.x, secondCorner.x, Random.Range(0f, 1f));
            var z = Mathf.Lerp(firstCorner.z, secondCorner.z, Random.Range(0f, 1f));
            var spawnPos = new Vector3(x, height + heightOffset, z);

            Spawn(spawnPos);
        }
    }
}