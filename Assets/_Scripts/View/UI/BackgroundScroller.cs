using UnityEngine;
using UnityEngine.UI;

namespace View
{   
    namespace UI
    {
        public class BackgroundScroller : MonoBehaviour
        {
            [SerializeField] private RawImage _backgroundImage;
            [SerializeField] private float _xCoord, _yCoord;

            private void Update()
            {
                _backgroundImage.uvRect = new Rect(_backgroundImage.uvRect.position + new Vector2(_xCoord, _yCoord) * Time.deltaTime, _backgroundImage.uvRect.size);
            }
        }
    }
}