using Egsp.Core;

namespace Alive.Behaviours
{
    /// <summary>
    /// Определяет тип поведения. Игровое, анимационное и т.п..
    /// </summary>
    public abstract partial class Behaviour<TEntity> where TEntity : BehaviourEntity<TEntity>
    {
        /// <summary>
        /// Текущий носитель поведения.
        /// </summary>
        public readonly TEntity Owner;
        
        /// <summary>
        /// Пробудилось ли данное поведение.
        /// </summary>
        public bool AlreadyAwake { get; protected set; }
        
        /// <summary>
        /// Текущее состояние поведения.
        /// </summary>
        protected Option<State<TEntity>> State { get; private set; }

        public Behaviour(NotNull<TEntity> owner)
        {
            Owner = owner.Value;
        }

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

        /// <summary>
        /// Пробуждает состояние в нужной форме.
        /// </summary>
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

        /// <summary>
        /// Обновляет состояние в нужной форме.
        /// </summary>
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
        
        /// <summary>
        /// Заменяет текущее состояние поведения.
        /// </summary>
        public virtual void SetState(State<TEntity> newState)
        {
            State = newState;
        } 
    }
}