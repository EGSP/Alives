using System;

namespace Alive
{
    // Здесь определены базовые компоненты всех сущностей.
    
    public abstract partial class Entity
    {
        [Obsolete("Данный компонент лучше изменить на полноценный transform, имеющий данные о вращении." +
                  " Однако, если не учитывать вращение, то данного компонента достаточно.")]
        private PositionComponent _positionComponent;
        
        /// <summary>
        /// Компонент визульной оболочки сущности.
        /// </summary>
        private VisualShellComponent _visualShellComponent;
    }
}