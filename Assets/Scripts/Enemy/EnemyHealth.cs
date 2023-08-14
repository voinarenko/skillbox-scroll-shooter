using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyHealth : Health
    {
        // Здоровье врага

        // Переменные
        [SerializeField] private float killBounty = 50; // Награда за убийство
        private EnemyPatrol deadEnemy;                  // Скрипт ИИ

        private void Start()
        {
            deadEnemy = GetComponent<EnemyPatrol>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        // Инициализация
        private void Awake()
        {
            MaxHealth = 100;            // Стартовое здоровье
            CurrentHealth = MaxHealth;  // Инициализация текущего здоровья
            IsAlive = true;             // Враг жив
        }

        /// <summary>
        /// Метод гибели врага
        /// </summary>
        public void EnemyDead()
        {
            Score.CurrentScore += killBounty;
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            deadEnemy.enabled = false;
        }
    }
}
