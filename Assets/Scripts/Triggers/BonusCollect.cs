using Assets.Scripts.Common;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Triggers
{
    public class BonusCollect : MonoBehaviour
    {
        // Сбор наград

        #region Переменные

        private GameObject bonus;                               // Источник награды
        private Animator anim;                                  // Аниматор
        private Score collectScore;                             // Сбор очков
        private Health collectHealth;                           // Сбор здоровья
        [SerializeField] private float boxValue = 10;           // Количество очков в ящике
        [SerializeField] private float chestValue = 100;        // Количество здоровья в ящике
        [SerializeField] private float boxHealthValue = 10;     // Количество очков в сундуке
        [SerializeField] private float chestHealthValue = 100;  //Количество здоровья в сундуке

        #endregion

        private void Start()
        {
            // Инициализация
            bonus = gameObject;
            collectScore = GameObject.Find("Player").GetComponent<Score>();
            collectHealth = GameObject.Find("Player").GetComponent<Health>();
            anim = GetComponent<Animator>();
        }

        /// <summary>
        /// Метод сбора наград по триггеру
        /// </summary>
        /// <param name="other">объект, вошедший в триггер</param>
        private void OnTriggerEnter2D(Object other)
        {
            if (!other.GameObject().CompareTag("Player")) return;   // Проверяем наличие игрока
            bonus.GetComponent<Collider2D>().enabled = false;       // Отключаем коллайдер объекта
            anim.SetTrigger("IsOpen");                          // Запускаем анимацию открытия
            // Проверяем тип награды
            if (bonus.CompareTag("Box"))
            {
                collectScore.PickBonus(boxValue);   // Очки из ящика
                collectHealth.Heal(boxHealthValue); // Здоровье из ящика
            }
            else if (bonus.CompareTag("Chest"))
            {
                collectScore.PickBonus(chestValue);     // Очки из сундука
                collectHealth.Heal(chestHealthValue);   // Здоровье из сундука
            }
        }
    }
}
