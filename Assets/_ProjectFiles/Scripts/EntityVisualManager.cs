using System;
using UnityEngine;

namespace Alive
{
    public class EntityVisualManager : MonoBehaviour
    {
        [SerializeField] private EntityVisualShell shipVisualShell;
        private EntityManager _entityManager;

        public void Awake()
        {
            _entityManager = EntityManager.CreateInstance();

            var id = _entityManager.RequestEntityId();
            
            var spaceShip = new SpaceShipEntity(id,1);
            var shell = Instantiate(shipVisualShell);

            spaceShip.Position = new Vector3(10, 10, 10);
            spaceShip.AddComponent(new VisualShellComponent(shell));
            
            _entityManager.Awake();
        }

        public void Update()
        {
            _entityManager.Update();
        }
    }
}