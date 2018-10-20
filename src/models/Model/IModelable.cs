
namespace IGUWPF.src.models.model
{
    public interface IModelable<T>
    {
        int GetID();
        void SetID(int ID);
        T Clone();
    }
}
