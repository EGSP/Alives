using System;
using System.Collections.Generic;

namespace Alive.Behaviours
{
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

        // При установке компонента на сущность, создаем стандартные поведения.
        protected override void OnEntitySetInternal()
        {
            Behaviours.Add(typeof(Behaviour<TEntity>.CommonBehaviour),
                new Behaviour<TEntity>.CommonBehaviour(Parent));
            Behaviours.Add(typeof(Behaviour<TEntity>.AnimationBehaviour),
                new Behaviour<TEntity>.AnimationBehaviour(Parent));
        }

        public void Awake()
        {
            SetupDefaultBehaviour();
            CurrentBehaviour.Awake();
        }

        public void Update(in UpdateData u)
        {
            CurrentBehaviour.Update(in u);
        }

        /// <summary>
        /// Меняет поведение на переданный тип поведения.
        /// </summary>
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

        /// <summary>
        /// Меняет поведение на переданный тип поведения.
        /// </summary>
        public void ChangeBehaviourTo(BehaviourType type)
        {
            switch (type)
            {
                case BehaviourType.Common: ChangeBehaviourTo<Behaviour<TEntity>.CommonBehaviour>();
                    return;
                case BehaviourType.Animation: ChangeBehaviourTo<Behaviour<TEntity>.AnimationBehaviour>();
                    return;
            }
        }
        
        /// <summary>
        /// Устанавливает текущее поведение на одно из стандартных.
        /// </summary>
        protected void SetupDefaultBehaviour() => ChangeBehaviourTo(BehaviourType.Common);
    }
}