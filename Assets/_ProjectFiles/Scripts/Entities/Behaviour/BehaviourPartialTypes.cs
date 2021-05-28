namespace Alive.Behaviour
{
    public abstract partial class Behaviour
    {
        /// <summary>
        /// Обычное игровое поведение сущности. Без локов анимаций и т.п.
        /// Каждый кадр вызывает Update. И при появлении Awake.
        /// </summary>
        public class CommonBehaviour : Behaviour
        {
            public override void Awake()
            {
                if (State.IsSome)
                    State.Value.Awake();
            }

            public override void Update(in UpdateData u)
            {
                if (State.IsSome)
                    State.Value.Update(in u);
            }
        }
        
        public class AnimationBehaviour : Behaviour
        {
        
        }
    }
}