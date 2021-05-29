using Egsp.Core;

namespace Alive.Behaviour
{
    public abstract partial class Behaviour<TEntity> where TEntity : BehaviourEntity<TEntity>
    {
        /// <summary>
        /// Обычное игровое поведение сущности. Без локов анимаций и т.п.
        /// Каждый кадр вызывает Update. И при появлении Awake.
        /// </summary>
        public class CommonBehaviour<TEntity> : Behaviour<TEntity> where TEntity : BehaviourEntity<TEntity>
        {
            public CommonBehaviour(NotNull<BehaviourEntity<TEntity>> owner) : base(owner)
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

            public override void SetState(BehaviourState<TEntity> newState)
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
        
        public class AnimationBehaviour<TEntity> : Behaviour<TEntity> where TEntity : BehaviourEntity<TEntity>
        {
            public AnimationBehaviour(NotNull<BehaviourEntity<TEntity>> owner) : base(owner)
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