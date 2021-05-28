using System;
using System.Collections.Generic;

namespace Alive.Behaviour
{
    // Behaviour - это момент выполнения каких-либо действий. Обычное-игровое, в состоянии анимации и т.п..
    // Behaviour - это сама логика, выполняемая при обновлении поведения.
    
    /// <summary>
    /// <para>Компонент-носитель поведения.</para>
    /// <para>Содержит в себе базовый список типов поведения и логику их обновления.</para>
    /// </summary>
    public class BehaviourComponent<TEntity> : EntityComponent<TEntity>, IAwakeComponent, IUpdateComponent
        where TEntity : BehaviourEntity<TEntity>
    {
        /// <summary>
        /// Все виды поведения сущности.
        /// </summary>
        private Dictionary<Type, Behaviour> Behaviours { get; set; } = new Dictionary<Type, Behaviour>();
        
        private Behaviour CurrentBehaviour { get; set; }

        public BehaviourComponent()
        {
            Behaviours.Add(typeof(Behaviour.CommonBehaviour), new Behaviour.CommonBehaviour());
            Behaviours.Add(typeof(Behaviour.AnimationBehaviour), new Behaviour.AnimationBehaviour());
        }

        public void Awake()
        {
            CurrentBehaviour = Behaviours[typeof(Behaviour.CommonBehaviour)];
        }

        public void Update(in UpdateData u)
        {
            CurrentBehaviour.Update(in u);
        }
    }

    /// <summary>
    /// Состояние поведения.
    /// </summary>
    public abstract class BehaviourState
    {
        protected Behaviour Behaviour { get; private set; }
        
        public virtual void Awake(){}

        public virtual void Update(in UpdateData u) { }
    }

    /// <summary>
    /// Behaviour constraint.
    /// </summary>
    public abstract class BehaviourState<TBehaviour> : BehaviourState
        where TBehaviour : Behaviour
    {
        protected new TBehaviour Behaviour { get; private set; }
    }
}