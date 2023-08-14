using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class GameManager : MonoBehaviour
    {
        // Управление экранами игрового уровня

        // Переменные экранов
        public GameObject PauseScreen;  // Экран паузы
        public GameObject GameScreen;   // Экран игры

        #region Управление экранами

        /// <summary>
        /// Метод вызова экрана паузы
        /// </summary>
        public void PauseGame()
        {
            Time.timeScale = 0;
            GameScreen.SetActive(false);
            PauseScreen.SetActive(true);
        }

        /// <summary>
        /// Метод вызова сцены победы
        /// </summary>
        public void WonGame()
        {
            SceneManager.LoadScene(2);
        }

        /// <summary>
        /// Метод вызова сцены поражения
        /// </summary>
        public void LostGame()
        {
            SceneManager.LoadScene(3);
        }

        /// <summary>
        /// Метод возврата в игру из экрана паузы
        /// </summary>
        public void ResumeGame()
        {
            Time.timeScale = 1;
            PauseScreen.SetActive(false);
            GameScreen.SetActive(true);
        }

        /// <summary>
        /// Метод перезагрузки игрового уровня
        /// </summary>
        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Score.SetCurrentScoreToZero();
        }

        /// <summary>
        /// Метод выхода в стартовую сцену
        /// </summary>
        public void MenuExit()
        {
            SceneManager.LoadScene(0);
        }

        #endregion
    }
}
