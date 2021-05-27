using System;
using Egsp.CSharp;
using UnityEngine;

namespace Alive
{
    public abstract partial class Entity
    {
        // Groups of components.
        private ComponentGroup<IAwakeComponent> _awakeGroup;
        private ComponentGroup<IUpdateComponent> _updateGroup;

        private Action<IAwakeComponent> _awakeAction = new Action<IAwakeComponent>(x => x.Awake());
        
        /// <summary>
        /// Вызывается при появлении объекта в игре.
        /// </summary>
        public void ExternalAwake()
        {
            _awakeGroup.Invoke(_awakeAction);
            Awake();
        }

        /// <summary>
        /// Вызывается при обновлении состояния.
        /// </summary>
        public void ExternalUpdate(in UpdateData updateData)
        {
            for (var i = 0; i < _updateGroup.Components.Count; i++)
            {
                var component = _updateGroup.Components[i];
                
                if(_updateGroup.CheckAlive(component,i) != ObjectLiveState.Alive)
                    continue;

                component.Update(in updateData);
            }

            Update(in updateData);
        }

        /// <summary>
        /// Вызывается при появлении объекта в игре. Base()-free
        /// </summary>
        protected virtual void Awake()
        {
            
        }

        /// <summary>
        /// Вызывается при обновлении состояния. Base()-free
        /// </summary>
        protected virtual void Update(in UpdateData u)
        {
            
        }
        
        protected override void OnGroupComponent(IComponent component)
        {
            var groupAwake = 
                Group<IAwakeComponent>(component);

            var groupUpdate =
                Group<IUpdateComponent>(component);
            
            if (groupAwake.IsSome)
                _awakeGroup = groupAwake.Value;

            if (groupUpdate.IsSome)
                _updateGroup = groupUpdate.Value;
        }
    }

    public readonly struct UpdateData
    {
        public readonly float deltaTime;

        public UpdateData(float deltaTime)
        {
            this.deltaTime = deltaTime;
        }
    }
    
    public interface IAwakeComponent : IComponent
    {
        void Awake();
    }

    public interface IUpdateComponent : IComponent
    {
        void Update(in UpdateData u);
    }
    
}