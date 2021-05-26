using UnityEngine;

namespace Alive
{
    public interface IWorldEntity
    {
        Vector3 Position { get; }
        bool CustomUpdateLoopUse { get; set; }
        
        void UpdateRaw();
        
        void ToReal();
        void ToVirtual();
    }
}