using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace View
{   
    namespace UI
    {
        public class PlayButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
        {
            [SerializeField] private Image _buttonImage;
            [SerializeField] private Sprite _pressedSprite, _unpressedSprite;
            [SerializeField] private AudioClip _pressedClip, _unpressedClip;
            [SerializeField] private AudioSource _audioSource;

            public void OnPointerDown(PointerEventData eventData)
            {
                _buttonImage.sprite = _pressedSprite;
                _audioSource.PlayOneShot(_pressedClip);
            }

            public async void OnPointerUp(PointerEventData eventData)
            {
                _buttonImage.sprite = _unpressedSprite;
                _audioSource.PlayOneShot(_unpressedClip);
                await Task.Delay(1000);

                PlayButtonOnClick();
            }

            public void PlayButtonOnClick()
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    } 
}