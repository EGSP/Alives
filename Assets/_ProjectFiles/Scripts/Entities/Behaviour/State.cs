namespace Alive.Behaviours
{
    /// <summary>
    /// Состояние поведения. Другими словами вся логика.
    /// </summary>
    public abstract class State<TEntity> where TEntity : BehaviourEntity<TEntity>
    {
        /// <summary>
        /// Текущее поведение данного состояния.
        /// </summary>
        protected readonly Behaviour<TEntity> Behaviour;

        /// <summary>
        /// Носитель поведения.
        /// </summary>
        protected readonly TEntity Owner;

        protected State(Behaviour<TEntity> behaviour)
        {
            Behaviour = behaviour;
            Owner = Behaviour.Owner;
        }

        /// <summary>
        /// Вызывается при пробуждении сущности, либо при первом пробуждении состояния, если сущность уже пробуждена.
        /// </summary>
        public virtual void Awake()
        {
        }

        public virtual void Update(in UpdateData u)
        {
        }

        public virtual void AwakeVirtual()
        {
        }

        public virtual void UpdateVirtual(in UpdateData u)
        {
        }
    }
}