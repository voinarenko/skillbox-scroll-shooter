using Assets.Scripts.Common;
using Assets.Scripts.Player;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class HudManager : MonoBehaviour
    {
        // Управление игровым экраном

        // Переменные вывода текстовой информации
        [SerializeField] private TMP_Text scoreText;    // Переменная вывода текущего счёта
        [SerializeField] private TMP_Text healthText;   // Переменная вывода текущего уровня здоровья игрока
        [SerializeField] private TMP_Text ammoText;     // Переменная вывода текущего количества патронов

        private void Update()
        {
            scoreText.text = Score.CurrentScore.ToString(CultureInfo.CurrentCulture);       // Вывод текущего счёта на экран
            healthText.text = PlayerHealth.CurrentPlayerHealth.ToString(CultureInfo.CurrentCulture);    // Вывод текущего уровня здоровья на экран
            ammoText.text = Shooter.CurrentAmmo.ToString(CultureInfo.CurrentCulture);       // Вывод текущего количества патронов на экран
        }
    }
}
