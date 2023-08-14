using Assets.Scripts.Common;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class DamageDealerZone : MonoBehaviour
    {
        // Урон от опасной зоны

        #region Переменные

        [SerializeField] private float tickTime;        // Время до нанесения следующего урона от огня
        [SerializeField] private float damageFromFire;  // Количество урона от огня

        private const float NormalState = 0;            // Игрок находится на безопасной поверхности
        private const float BurningState = 1;           // Игрок стоит в опасной зоне

        private const string DetectionTag = "Player";   // Метка игрока
        private GameObject player;                      // Объект игрока

        private float currentState, currentTimeToTick;  // Переменные текущего состояния, времени до нанесения урона от огня

        #endregion

        // Инициализация
        void Start()
        {
            player = null;
        }
        private void Awake()
        {
            currentTimeToTick = 0; // Время до нанесения урона от огня
        }

        void Update()
        {
            if (currentTimeToTick >= tickTime)
            {
                currentTimeToTick = 0;
                player.GetComponent<Health>().TakeDamage(damageFromFire);  // Нанесение урона от огня
                if (!player.GetComponent<Health>().CheckIsAlive()) player.GetComponent<PlayerHealth>().PlayerDead();  // Если здоровье игрока опускается до 0 — конец игры

            }

            switch (currentState)
            {
                case NormalState:
                    break;
                case BurningState:
                    currentTimeToTick += Time.deltaTime;
                    break;
            }
        }

        /// <summary>
        /// Метод нанесения урона при входе в триггер
        /// </summary>
        /// <param name="collision">объект, вошедший в триггер</param>
        private void OnTriggerEnter2D(Component collision)
        {
            player = collision.gameObject;
            if (!collision.CompareTag(DetectionTag)) return;    // Если объект соответствует метке
            currentState = BurningState;                        // Переключаем состояние
            collision.GetComponent<Health>().TakeDamage(damageFromFire);  // Наносим разовый урон
        }

        /// <summary>
        /// Метод отключения периодического урона при выходе из триггера
        /// </summary>
        /// <param name="collision">объект, вышедший из триггера</param>
        private void OnTriggerExit2D(Component collision)
        {
            player = null;
            if (!collision.CompareTag(DetectionTag)) return;    // Если объект соответствует метке
            currentState = NormalState;                         // Переключаем состояние
        }
    }
}
