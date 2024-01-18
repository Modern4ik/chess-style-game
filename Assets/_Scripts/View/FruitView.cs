using System.Threading.Tasks;
using UnityEngine;

namespace View
{
    public class FruitView : MonoBehaviour, IFruitView
    {
        private const string isDestroying = "isDestroying";

        [SerializeField] private Animator _fruitAnimator;

        public async Task StartDestroyingAnimation()
        {
            _fruitAnimator.SetBool(isDestroying, true);

            while (_fruitAnimator.GetBool(isDestroying)) await Task.Delay(25);
        }

        public void StopDestroyAnimation() => _fruitAnimator.SetBool(isDestroying, false);

        public void DestroyObject() => Destroy(gameObject);
    }
}