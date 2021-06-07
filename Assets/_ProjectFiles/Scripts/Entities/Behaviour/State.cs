namespace Alive.Behaviours
{
    /// <summary>
    /// Состояние поведения. Другими словами вся логика.
    /// </summary>
    public abstract class BehaviourState<TEntity> where TEntity : BehaviourEntity<TEntity>
    {
        /// <summary>
        /// Текущее поведение данного состояния.
        /// </summary>
        protected readonly Behaviour<TEntity> Behaviour;

        protected BehaviourState(Behaviour<TEntity> behaviour)
        {
            Behaviour = behaviour;
        }

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