using UnityEngine;

namespace Assets.Scripts.Common
{
    public class Score : MonoBehaviour
    {
        // Подсчёт наград

        public static float CurrentScore;   // Переменная текущего счёта

        /// <summary>
        /// Метод сбора наград
        /// </summary>
        /// <param name="score">количество очков</param>
        public void PickBonus(float score)
        {
            CurrentScore += score;  // Увеличение очков
        }

        /// <summary>
        /// Метод сброса очков
        /// </summary>
        public static void SetCurrentScoreToZero()
        {
            CurrentScore = 0;
        }
    }
}
