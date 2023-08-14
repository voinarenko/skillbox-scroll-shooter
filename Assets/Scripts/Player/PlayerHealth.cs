using Assets.Scripts.Common;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : Health
    {
        // Здоровье игрока

        #region Переменные

        public static float CurrentPlayerHealth;               // Текущее здоровье игрока

        #endregion

        // Инициализация
        private void Awake()
        {
            MaxHealth = 150;            // Стартовое здоровье
            CurrentHealth = MaxHealth;  // Текущее здоровье
            CurrentPlayerHealth = CurrentHealth;
            IsAlive = true;             // Игрок жив
        }
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            CurrentPlayerHealth = CurrentHealth;
        }
        
        /// <summary>
        /// Метод гибели игрока
        /// </summary>
        public void PlayerDead()
        {
            EventSystem.GetComponent<GameManager>().LostGame();
        }
    }
}
