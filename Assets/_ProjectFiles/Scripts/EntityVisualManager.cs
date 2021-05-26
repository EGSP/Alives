using System;
using UnityEngine;

namespace Alive
{
    public class EntityVisualManager : MonoBehaviour
    {
        [SerializeField] private EntityVisualShell shipVisualShell;

        public void Awake()
        {
            var spaceShip = new SpaceShipEntity(1);
            var shell = Instantiate(shipVisualShell);

            spaceShip.Position = new Vector3(10, 10, 10);
            spaceShip.AddComponent(new VisualShellComponent(shell));
        }
    }
}