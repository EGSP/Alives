using Egsp.Core;

namespace Alive.Behaviour
{
    /// <summary>
    /// Определяет тип поведения. Игровое, анимационное и т.п..
    /// </summary>
    public abstract partial class Behaviour
    {
        /// <summary>
        /// Текущая логика поведения.
        /// </summary>
        protected Option<BehaviourState> State { get; private set; }

        public virtual void Awake() { }
        public virtual void Update(in UpdateData u) { }
    }
}