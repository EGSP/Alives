namespace Alive.Behaviour
{
    
    /// <summary>
    /// Состояние поведения.
    /// </summary>
    public abstract class BehaviourState<TEntity> where TEntity : BehaviourEntity<TEntity>
    {
        protected Behaviour<TEntity> Behaviour { get; private set; }

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
    
    /// <summary>
    /// Behaviour constraint.
    /// </summary>
    public abstract class BehaviourState<TEntity,TBehaviour> : BehaviourState<TEntity>
        where TEntity : BehaviourEntity<TEntity>
        where TBehaviour : Behaviour<TEntity>
    {
        protected new TBehaviour Behaviour { get; private set; }
    }
}