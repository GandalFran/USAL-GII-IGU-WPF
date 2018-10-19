
namespace IGUWPF.src.models.Model
{
    public interface IModelable<T>
    {
        int GetID();
        void SetID(int ID);
        T Clone();
    }
}
