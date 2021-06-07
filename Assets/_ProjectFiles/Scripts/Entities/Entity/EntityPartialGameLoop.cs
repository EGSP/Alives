using System;
using Egsp.CSharp;
using UnityEngine;

namespace Alive
{
    // Здесь находится вся локига игрового цикла сущности. Update, Awake.
    // Компоненты всегда пробуждаются и обновляются первыми.
    
    public abstract partial class Entity
    {
        // Компоненты, которые хотят обновления в определенный момент игры.
        private ComponentGroup<IAwakeComponent> _awakeGroup;
        private ComponentGroup<IUpdateComponent> _updateGroup;

        // Стандартное действие для всех awakable компонентов. Кешированное значение. 
        private Action<IAwakeComponent> _awakeAction = new Action<IAwakeComponent>(x => x.Awake());
        
        
        // Вызывается извне.
        public void ExternalAwake()
        {
            // Применяем пробуждение для компонентов.
            _awakeGroup?.Invoke(_awakeAction);
            Awake();
        }
        
        // Вызывается извне.
        public void ExternalUpdate(in UpdateData updateData)
        {
            if (_updateGroup == null)
                return;
            
            // Если при пробуждении мы спокойно можем делегировать действия ComponentGroup,
            // то в данном случае логика меняется. Нужно вызвать компоненты руками.
            
            // Применены ручные вызовы для передачи UpdateData по ссылке. 
            // Группе бы пришлось передавать каждый раз новый экземпляр, т.к. группа ничего не знает об UpdateData.
            for (var i = 0; i < _updateGroup.Components.Count; i++)
            {
                var component = _updateGroup.Components[i];
                
                if(_updateGroup.CheckAlive(component,i) != ObjectLiveState.Alive)
                    continue;

                component.Update(in updateData);
            }

            // Вызываем обновление у себя.
            Update(in updateData);
        }

        /// <summary>
        /// Вызывается при появлении объекта в игре.
        /// </summary>
        protected virtual void Awake()
        {
        }

        /// <summary>
        /// Вызывается при обновлении состояния.
        /// </summary>
        protected virtual void Update(in UpdateData u)
        {
        }
        
        protected override void OnGroupComponent(IComponent component)
        {
            // Добавляем компоненты по группам.
            var groupAwake = 
                Group<IAwakeComponent>(component);

            var groupUpdate =
                Group<IUpdateComponent>(component);
            
            // Если группа была создана однажды, то она же и будет записана вновь.
            if (groupAwake.IsSome)
                _awakeGroup = groupAwake.Value;

            if (groupUpdate.IsSome)
                _updateGroup = groupUpdate.Value;
        }
    }
}