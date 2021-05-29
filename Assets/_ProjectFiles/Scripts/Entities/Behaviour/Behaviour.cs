using Egsp.Core;

namespace Alive.Behaviour
{
    /// <summary>
    /// Определяет тип поведения. Игровое, анимационное и т.п..
    /// </summary>
    public abstract partial class Behaviour<TEntity> where TEntity : BehaviourEntity<TEntity>
    {
        /// <summary>
        /// Текущий носитель поведения.
        /// </summary>
        public readonly BehaviourEntity<TEntity> Owner;
        
        protected bool AlreadyAwake;

        public Behaviour(NotNull<BehaviourEntity<TEntity>> owner)
        {
            Owner = owner.Value;
        }
        
        /// <summary>
        /// Текущая логика поведения.
        /// </summary>
        protected Option<BehaviourState<TEntity>> State { get; private set; }

        public void Awake()
        {
            if (AlreadyAwake)
                return;

            AlreadyAwake = true;

            if (Owner.Form == EntityForm.Real)
            {
                AwakeInternal();
            }
            else
            {
                AwakeVirtualInternal();
            }
        }

        public void Update(in UpdateData u)
        {
            if (Owner.Form == EntityForm.Real)
            {
                UpdateInternal(in u);
            }
            else
            {
                UpdateVirtualInternal(in u);
            }
        }
        
        protected virtual void AwakeInternal(){}
        protected virtual void UpdateInternal(in UpdateData u){}
        
        protected virtual void AwakeVirtualInternal() {}
        protected virtual void UpdateVirtualInternal(in UpdateData u) {}

        protected void AwakeState()
        {
            if (State.IsSome)
            {
                if (Owner.Form == EntityForm.Real)
                {
                    State.Value.Awake();
                }
                else
                {
                    State.Value.AwakeVirtual();
                }
            }
        }

        protected void UpdateState(in UpdateData u)
        {
            if (State.IsSome)
            {
                if (Owner.Form == EntityForm.Real)
                {
                    State.Value.Update(in u);
                }
                else
                {
                    State.Value.UpdateVirtual(in u);
                }
            };
        }
        
        public virtual void SetState(BehaviourState<TEntity> newState)
        {
            State = newState;
        } 
    }
}