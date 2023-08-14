using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class MainMenuManager : MonoBehaviour
    {
        // Управление окном главного меню

        // Переменные экранов
        private GameObject screen;          // текущий экран
        [SerializeField] private GameObject startScreen;      // Начальный экран
        [SerializeField] private GameObject settingsScreen;   // Экран настроек
        [SerializeField] private GameObject quitScreen;       // Экран выхода из игры

        #region Управление экранами

        /// <summary>
        /// Метод вызова экрана настроек
        /// </summary>
        public void Settings()
        {
            screen = settingsScreen;
            Time.timeScale = 0;
            startScreen.SetActive(false);
            settingsScreen.SetActive(true);
        }

        /// <summary>
        /// Метод вызова экрана выхода из игры
        /// </summary>
        public void QuitMenu()
        {
            screen = quitScreen;
            Time.timeScale = 0;
            startScreen.SetActive(false);
            quitScreen.SetActive(true);
        }

        /// <summary>
        /// Метод закрытия приложения
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Метод возврата в главное меню
        /// </summary>
        public void Return()
        {
            Time.timeScale = 1;
            screen.SetActive(false);
            startScreen.SetActive(true);
        }

        /// <summary>
        /// Метод запуска игрового уровня
        /// </summary>
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        #endregion

        private void Awake()
        {
            Score.SetCurrentScoreToZero();  // Сброс счёта
        }
    }
}
