using Alive.Behaviour;

namespace Alive
{
    /// <summary>
    /// Автоматически добавляет компонент поведение.
    /// </summary>
    public abstract class BehaviourEntity<TEntity> : Entity
        where TEntity : BehaviourEntity<TEntity>
    {
        // Generic нужен, чтобы указать тип будущего дочернего класса при создании компонента.
        // Он указывает на самого себе, но конкретного типа TEntity.
        
        protected BehaviourComponent<TEntity> BehaviourComponent { get; private set; }
        
        protected BehaviourEntity(UInt32Id id) : base(id)
        {
            AddComponent(new BehaviourComponent<TEntity>());
            BehaviourComponent = GetComponent<BehaviourComponent<TEntity>>().Value;
        }
    }
}