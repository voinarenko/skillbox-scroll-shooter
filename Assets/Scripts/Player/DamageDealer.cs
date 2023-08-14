using Assets.Scripts.Common;
using Assets.Scripts.Enemy;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class DamageDealer : MonoBehaviour
    {
        // Нанесение урона игроком

        [SerializeField] private float damage;  // Переменная количества урона

        /// <summary>
        /// Метод нанесения урона при входе в триггер
        /// </summary>
        /// <param name="collision">объект, вошедший в триггер</param>
        private void OnTriggerEnter2D(Component collision)
        {
            // Если столкнулись с врагом — наносим урон
            if (collision.CompareTag("EnemyShooter"))
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                if (!collision.GetComponent<Health>().CheckIsAlive()) collision.GetComponent<EnemyHealth>().EnemyDead();
            }

            Destroy(gameObject);    // Уничтожаем объект после столкновения
        }
    }
}
