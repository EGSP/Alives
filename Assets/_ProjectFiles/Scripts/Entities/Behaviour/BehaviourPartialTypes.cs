using Egsp.Core;

namespace Alive.Behaviours
{
    public abstract partial class Behaviour<TEntity> where TEntity : BehaviourEntity<TEntity>
    {
        /// <summary>
        /// Обычное игровое поведение сущности. Без локов анимаций и т.п.
        /// Каждый кадр вызывает Update. И при пробуждении Awake.
        /// </summary>
        public class CommonBehaviour : Behaviour<TEntity>
        {
            public CommonBehaviour(NotNull<TEntity> owner) : base(owner)
            {
            }
            
            protected override void AwakeInternal()
            {
                AwakeState();
            }

            protected override void UpdateInternal(in UpdateData u)
            {
                UpdateState(in u);
            }

            // Отличие данного метода от родительского в том, что идет вызов Awake.
            public override void SetState(State<TEntity> newState)
            {
                if (State.IsNone)
                {
                    State = newState;
                }
                else
                {
                    // Maybe dispose or stop call.
                    State = newState;
                }
                
                if(AlreadyAwake)
                    AwakeState();
            }
        }
        
        /// <summary>
        /// Поведение при анимации. Чаще всего при анимации ничего не должно просчитываться.
        /// </summary>
        public class AnimationBehaviour : Behaviour<TEntity>
        {
            public AnimationBehaviour(NotNull<TEntity> owner) : base(owner)
            {
            }
        }
    }

    public enum BehaviourType
    {
        Common,
        Animation
    }
}