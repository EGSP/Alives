namespace Alive.Behaviours
{
    /// <summary>
    /// Автоматически добавляет компонент поведения.
    /// </summary>
    public abstract class BehaviourEntity<TEntity> : Entity
        where TEntity : BehaviourEntity<TEntity>
    {
        // Generic нужен, чтобы указать тип будущего дочернего класса при создании компонента.
        // Он указывает на самого себя, но конкретного типа TEntity.
        
        protected BehaviourComponent<TEntity> BehaviourComponent { get; private set; }
        
        protected BehaviourEntity() : base()
        {
            AddComponent(new BehaviourComponent<TEntity>());
            BehaviourComponent = GetComponent<BehaviourComponent<TEntity>>().Value;
        }
    }
}