using Egsp.CSharp;

namespace Alive
{
    public abstract partial class Entity
    {
        /// <summary>
        /// Вызывается при появлении объекта в игре.
        /// </summary>
        public void ExternalAwake()
        {
            Awake();
        }

        /// <summary>
        /// Вызывается при обновлении состояния.
        /// </summary>
        public void ExternalUpdate(in UpdateData u)
        {
            Update(in u);
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
    }

    public readonly struct UpdateData
    {
        public readonly float deltaTime;
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