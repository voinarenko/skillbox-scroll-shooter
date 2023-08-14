using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        // Управление меню настроек звука

        // Переменные
        [SerializeField] private AudioMixer mainMixer;  // Главный микшер звука
        [SerializeField] private Slider effectsSlider;  // Ползунок регулировки уровня громкости эффектов
        [SerializeField] private Slider musicSlider;    // Ползунок регулировки уровня громкости музыки
        public const string MixerEffects = "effects";   // Микшер эффектов
        public const string MixerMusic = "music";       // Микшер музыки

        private void Awake()
        {
            effectsSlider.onValueChanged.AddListener(EffectsVolume);    // Связывание ползунка громкости эффектов с микшером
            musicSlider.onValueChanged.AddListener(MusicVolume);        // Связывание ползунка громкости музыки с микшером
        }

        private void Start()
        {
            effectsSlider.value = PlayerPrefs.GetFloat(AudioManager.EffectsKey, 0); // Загрузка сохранённых параметров громкости эффектов
            musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MusicKey, 0);     // Загрузка сохранённых параметров громкости музыки
        }

        private void OnDisable()
        {
            PlayerPrefs.SetFloat(AudioManager.EffectsKey, effectsSlider.value); // Сохранение громкости эффектов
            PlayerPrefs.SetFloat(AudioManager.MusicKey, musicSlider.value);     // Сохранение громкости музыки
        }

        /// <summary>
        /// Метод установки громкости эффектов
        /// </summary>
        /// <param name="volume">значение громкости</param>
        private void EffectsVolume(float volume)
        {
            mainMixer.SetFloat(MixerEffects, volume);
        }

        /// <summary>
        /// Метод установки громкости музыки
        /// </summary>
        /// <param name="volume">значение громкости</param>
        private void MusicVolume(float volume)
        {
            mainMixer.SetFloat(MixerMusic, volume);
        }
    }
}