namespace Alive
{
    public class SpaceShipEntity : Entity
    {
        public float Scale { get; private set; }

        public SpaceShipEntity(UInt32Id id, float scale) : base(id)
        {
            Scale = scale;
        }
    }
}