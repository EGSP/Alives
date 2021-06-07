using Egsp.CSharp;

namespace Alive
{
    /// <summary>
    /// Данный компонент может быть добавлен в игровой цикл пробуждения.
    /// </summary>
    public interface IAwakeComponent : IComponent
    {
        void Awake();
    }
}