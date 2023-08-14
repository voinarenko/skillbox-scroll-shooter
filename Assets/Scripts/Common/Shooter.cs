using UnityEngine;

namespace Assets.Scripts.Common
{
    public class Shooter : MonoBehaviour
    {
        // Стрельба

        #region Переменные

        // Аудио
        [SerializeField] private AudioSource audioSource;           // Источник звука
        [SerializeField] private AudioClip reloadAudioClip;         // Звук перезарядки
        [SerializeField] private AudioClip playerShootAudioClip;    // Выстрел игрока
        [SerializeField] private AudioClip enemyShootAudioClip;     // Выстрел врага
        [SerializeField] private AudioClip emptyAudioClip;          // Закончились патроны

        [SerializeField] private GameObject bullet;     // Пуля
        [SerializeField] private float fireSpeed;       // Скорость стрельбы
        [SerializeField] private Transform firePoint;   // Точка выстрела
        [SerializeField] private float reloadTime;      // Время перезарядки
        [SerializeField] private int startAmmo;         // Полный магазин патронов
        public static int CurrentAmmo;                  // Текущее количество патронов 
        public bool IsReloading;              // Перезарядка
        [SerializeField] private float timer;                            // Таймер перезарядки

        #endregion

        // Инициализация
        private void Start()
        {
            IsReloading = false;
            CurrentAmmo = startAmmo;
        }


        private void Update()
        {
            // Пауза для перезарядки
            if (!IsReloading) return;
            timer += Time.deltaTime;
            if (timer < reloadTime) return;
            Reload();
            timer = 0;
        }

        /// <summary>
        /// Метод действия при пустом магазине
        /// </summary>
        public void Empty()
        {
            audioSource.GetComponent<AudioSource>().PlayOneShot(emptyAudioClip);
        }

        /// <summary>
        /// Метод выстрела игрока
        /// </summary>
        /// <param name="direction">направление</param>
        public void Shoot(int direction)
        {
            if (IsReloading) return;
            audioSource.GetComponent<AudioSource>().PlayOneShot(playerShootAudioClip);
            var currentBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
            var currentBulletVelocity = currentBullet.GetComponent<Rigidbody2D>();
            currentBulletVelocity.velocity = new Vector2(fireSpeed * direction, currentBulletVelocity.velocity.y);
            CurrentAmmo -= 1;
            if (CurrentAmmo <= 0)
            {
                IsReloading = true;
            }
        }

        /// <summary>
        /// Метод выстрела врага
        /// </summary>
        /// <param name="direction"></param>
        public void EnemyShoot(int direction)
        {
            audioSource.GetComponent<AudioSource>().PlayOneShot(enemyShootAudioClip);
            var currentBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
            var currentBulletVelocity = currentBullet.GetComponent<Rigidbody2D>();
            currentBulletVelocity.velocity = new Vector2(fireSpeed * direction, currentBulletVelocity.velocity.y);
        }

        /// <summary>
        /// Метод перезарядки
        /// </summary>
        private void Reload()
        {
            CurrentAmmo = startAmmo;
            IsReloading = false;
            audioSource.GetComponent<AudioSource>().PlayOneShot(reloadAudioClip);
        }
    }
}
