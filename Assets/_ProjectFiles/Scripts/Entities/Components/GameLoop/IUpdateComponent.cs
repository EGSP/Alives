using Egsp.CSharp;

namespace Alive
{
    /// <summary>
    /// Данный компонент может быть добавлен в игровой цикл обновления.
    /// </summary>
    public interface IUpdateComponent : IComponent
    {
        void Update(in UpdateData u);
    }
}