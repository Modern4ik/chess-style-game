using System.Threading.Tasks;

namespace View
{
    public interface IFruitView
    {
        Task StartDestroyingAnimation();
        void DestroyObject();
    }
}