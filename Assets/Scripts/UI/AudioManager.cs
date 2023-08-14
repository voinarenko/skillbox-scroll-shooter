using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.UI
{
    public class AudioManager : MonoBehaviour
    {
        // Управление регулировкой звука

        // Переменные
        [SerializeField] AudioMixer audioMixer; //  Микшер звука
        private static AudioManager Instance;   // Экземпляр объекта
        public const string EffectsKey = "effectsVolume";   // Громкость звука
        public const string MusicKey = "musicVolume";       // Громкость музыки

        private void Awake()
        {
            // Присваиваем экземпляр объекта и загружаем настройки
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            LoadVolume();   // Вызов загрузки настроек
        }

        /// <summary>
        /// Метод передачи сохранённых настроек в меню регулировки звука
        /// </summary>
        private void LoadVolume()
        {
            var effectsVolume = PlayerPrefs.GetFloat(EffectsKey, 0);    // Получение уровня эффектов
            var musicVolume = PlayerPrefs.GetFloat(MusicKey, 0);        // Получение уровня музыки

            audioMixer.SetFloat( SettingsMenu.MixerEffects, effectsVolume);         // Передача в меню настроек уровня эффектов
            audioMixer.SetFloat(SettingsMenu.MixerMusic, musicVolume);              // Передача в меню настроек уровня музыки
        }
    }
}
