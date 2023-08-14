using System.Globalization;
using Assets.Scripts.Common;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class FinalScreenManager : MonoBehaviour
    {
        // Управление экранами победы и поражения

        /// <summary>
        /// Переменная вывода счёта
        /// </summary>
        [SerializeField] private TMP_Text scoreText;

        private void Update()
        {
            scoreText.text = Score.CurrentScore.ToString(CultureInfo.CurrentCulture);   // Вывод финального счёта на экран
        }

        /// <summary>
        /// Метод возврата в начальную сцену
        /// </summary>
        public void NewGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}
