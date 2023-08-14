using Assets.Scripts.Common;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class DamageDealerEnemyBullet : MonoBehaviour
    {
        // Нанесение урона врагом

        [SerializeField] private float damage;  // Переменная количества урона

        /// <summary>
        /// Метод нанесения урона при входе в триггер
        /// </summary>
        /// <param name="collision">объект, вошедший в триггер</param>
        private void OnTriggerEnter2D(Component collision)
        {
            // Если попали в игрока — наносим урон
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Health>().TakeDamage(damage);
                if (!collision.GetComponent<Health>().CheckIsAlive()) collision.GetComponent<PlayerHealth>().PlayerDead();
            }
            if (!collision.CompareTag("EnemyShooter")) Destroy(gameObject);    // Уничтожаем объект после столкновения
        }
    }
}
