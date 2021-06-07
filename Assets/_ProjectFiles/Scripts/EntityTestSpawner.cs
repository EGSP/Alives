using Alive.Entities;
using Alive.Mono;
using UnityEngine;

namespace Alive
{
    public class EntityTestSpawner : MonoBehaviour
    {
        [SerializeField] private VisualShellMono shipVisualShellMono;
        private EntityManager _entityManager;

        public void Awake()
        {
            _entityManager = EntityManager.CreateInstance();
            
            var shell = Instantiate(shipVisualShellMono);
            var spaceShip = new SpaceShip(3);

            spaceShip.Position = new Vector3(10, 10, 10);
            spaceShip.VisualShell.Mono = shell;
            
            _entityManager.Awake();
        }

        public void Update()
        {
            _entityManager.Update();
        }
    }
}