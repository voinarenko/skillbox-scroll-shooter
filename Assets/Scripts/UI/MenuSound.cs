using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MenuSound : MonoBehaviour
    {
        // Звуковые эффекты меню

        // Переменные
        [SerializeField] private AudioSource audioSource;           // Источник звука
        [SerializeField] private AudioClip buttonClickAudioClip;    // Звуковой клип нажатия на кнопку

        /// <summary>
        /// Метод воспроизведения звука при нажатии на кнопку
        /// </summary>
        public void PlayButtonClick()
        {
            audioSource.GetComponent<AudioSource>().PlayOneShot(buttonClickAudioClip);
        }
    }
}
