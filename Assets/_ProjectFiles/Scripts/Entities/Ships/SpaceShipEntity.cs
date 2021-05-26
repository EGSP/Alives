namespace Alive
{
    public class SpaceShipEntity : Entity
    {
        public float Scale { get; private set; }

        public SpaceShipEntity(float scale) : base()
        {
            Scale = scale;
        }
    }
}