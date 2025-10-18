using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Button
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip _clickSound;
        private AudioSource _audioSource;


        public void OnPointerClick(PointerEventData eventData)
        {
            _audioSource ??= GameObject.FindWithTag("SFXSource").GetComponent<AudioSource>();
            _audioSource.PlayOneShot(_clickSound);
        }
    }
}