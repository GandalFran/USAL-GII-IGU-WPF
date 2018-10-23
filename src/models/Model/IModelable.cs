
using System;

namespace IGUWPF.src.models.model
{
    public interface IModelable : ICloneable
    {
        int GetID();
        void SetID(int ID);
    }
}
