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
    public class BehaviourComponent<TEntity> : EntityComponent<TEntity>
        where TEntity : BehaviourEntity<TEntity>
    {
        /// <summary>
        /// Все виды поведения сущности.
        /// </summary>
        private Dictionary<Type, Behaviour<TEntity>> Behaviours { get; set; } = new Dictionary<Type, Behaviour<TEntity>>();
        
        /// <summary>
        /// Текущее, вызываемоемое поведение.
        /// </summary>
        private Behaviour<TEntity> CurrentBehaviour { get; set; }

        protected override void OnEntitySetInternal()
        {
            Behaviours.Add(typeof(Behaviour<TEntity>.CommonBehaviour<TEntity>),
                new Behaviour<TEntity>.CommonBehaviour<TEntity>(Parent));
            Behaviours.Add(typeof(Behaviour<TEntity>.AnimationBehaviour<TEntity>),
                new Behaviour<TEntity>.AnimationBehaviour<TEntity>(Parent));
        }

        public void Awake()
        {
            SetDefaultBehaviour();
            CurrentBehaviour.Awake();
        }

        public void Update(in UpdateData u)
        {
            CurrentBehaviour.Update(in u);
        }

        protected void ChangeBehaviourTo<TBehaviourType>()
            where TBehaviourType : Behaviour<TEntity>
        {
            var type = typeof(TBehaviourType);

            Behaviour<TEntity> beh;
            if (Behaviours.TryGetValue(type, out beh))
            {
                CurrentBehaviour = beh;
            }
        }

        public void ChangeBehaviourTo(BehaviourType type)
        {
            switch (type)
            {
                case BehaviourType.Common: ChangeBehaviourTo<Behaviour<TEntity>.CommonBehaviour<TEntity>>();
                    return;
                case BehaviourType.Animation: ChangeBehaviourTo<Behaviour<TEntity>.AnimationBehaviour<TEntity>>();
                    return;
            }
        }

        protected void SetDefaultBehaviour() => ChangeBehaviourTo(BehaviourType.Common);
    }
}