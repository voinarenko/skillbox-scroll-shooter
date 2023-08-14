using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyPatrol : MonoBehaviour
    {
        // ИИ врага

        #region Переменные

        private Rigidbody2D rb;         // Объект
        private Animator anim;          // Аниматор
        private SpriteRenderer sr;      // Рендерер
        private Shooter shooterScript;  // Скрипт стрельбы
        private int shootDirection;     // Направление выстрела

        [SerializeField] private float speed, pause, reload;    // Скорость, время разворота, время перезарядки
        private float currentSpeed;                             // Текущая скорость

        private bool playerInArea, isEnemyReloading; // Игрок обнаружен, идёт перезарядка
        private Transform playerTransform;

        private const string DetectionTag = "Player";   // Метка поиска

        // Состояния
        private const float IdleState = 0;      // Покой
        private const float WalkState = 1;      // Ходьба
        private const float RevertState = 2;    // Разворот
        private const float AttackState = 3;    // Атака
        private const float ReloadState = 4;    // Перезарядка

        private float currentState, currentTimeToRevert, currentTimeToReload;   // Текущие состояние, время до разворота, время до перезарядки

        #endregion

        // Инициализация
        private void Start()
        {
            isEnemyReloading = false;
            currentSpeed = speed;
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
            shooterScript = GetComponent<Shooter>();
            currentState = WalkState;
            currentTimeToRevert = 0;
            sr.flipX = true;
        }

        private void Update()
        {
            // Завершение перезарядки
            if (currentTimeToReload >= reload)
            {
                isEnemyReloading = false;
                currentTimeToReload = 0;
                currentState = AttackState;
            }

            // Атака
            if (playerInArea && !isEnemyReloading)
            {
                currentState = AttackState;
            }

            // Разворот
            if (currentTimeToRevert >= pause)
            {
                currentTimeToRevert = 0;
                currentState = RevertState;
            }

            // Проверка состояния
            switch (currentState)
            {
                case IdleState:                                 // Покой
                    RestoreSpeed();
                    currentTimeToRevert += Time.deltaTime;
                    break;
                case WalkState:                                 // Ходьба
                    RestoreSpeed();
                    rb.velocity = Vector2.right * currentSpeed;
                    break;
                case RevertState:                               // Разворот
                    sr.flipX = !sr.flipX;
                    currentState = WalkState;
                    break;
                case AttackState:                               // Атака
                    Attack();
                    break;
                case ReloadState:                               // Перезарядка
                    currentTimeToReload += Time.deltaTime;
                    break;
            }
            anim.SetFloat("Velocity", rb.velocity.magnitude); // Изменение анимации движения
        }

        /// <summary>
        /// Метод обработки столкновений
        /// </summary>
        /// <param name="collision">объект, вошедший в триггер</param>
        private void OnTriggerEnter2D(Component collision)
        {
            // Столкновение с ограничителем движения
            if (collision.CompareTag("EnemyStopper"))
                currentState = IdleState;
            if (!collision.CompareTag(DetectionTag)) return;
            // Обнаружен игрок
            playerInArea = true;
            playerTransform = collision.gameObject.transform;
        }

        /// <summary>
        /// Метод обработки событий при выходе игрока из триггера
        /// </summary>
        /// <param name="collision">объект, вышедший из триггера</param>
        private void OnTriggerExit2D(Component collision)
        {
            if (!collision.CompareTag(DetectionTag)) return;
            playerInArea = false;
            isEnemyReloading = false;
            playerTransform = null;
            currentState = WalkState;
        }

        /// <summary>
        /// Метод восстановления скорости после прекращения атаки
        /// </summary>
        private void RestoreSpeed()
        {
            if (!sr.flipX) currentSpeed = speed;
            else currentSpeed = speed * -1;
        }

        /// <summary>
        /// Метод атаки
        /// </summary>
        private void Attack()
        {
            // Останавливаемся
            currentSpeed = 0;

            // Разворачиваемся в сторону игрока
            if (rb.transform.position.x < playerTransform.position.x)
            {
                sr.flipX = false;
                shootDirection = 1;
            }
            if (rb.transform.position.x > playerTransform.position.x)
            {
                sr.flipX = true;
                shootDirection = -1;
            }

            // Начинаем стрелять
            shooterScript.EnemyShoot(shootDirection);
            isEnemyReloading = true;
            currentState = ReloadState;
        }
    }
}
