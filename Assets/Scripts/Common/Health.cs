using UnityEngine;

namespace Assets.Scripts.Common
{
    public abstract class Health : MonoBehaviour
    {
        // Здоровье

        #region Переменные

        [SerializeField] protected GameObject EventSystem;        // Обработка событий
        [SerializeField] protected float MaxHealth;             // Максимальное здоровье
        [SerializeField] protected float CurrentHealth;         // Текущее здоровье
        [SerializeField] protected bool IsAlive;                // Объект жив
        protected Rigidbody2D rb;         // Объект
        protected Animator anim;          // Аниматор

        #endregion


        /// <summary>
        /// Метод получения урона
        /// </summary>
        /// <param name="damage">количество урона</param>
        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;                        
            CheckMinHealth();
            CheckIsAlive();
            if (IsAlive) return;
            anim.SetTrigger("IsDead");
            SetAllCollidersStatus(false);
        }

        /// <summary>
        /// Метод лечения игрока
        /// </summary>
        /// <param name="healing">количество здоровья</param>
        public void Heal(float healing)
        {
            CurrentHealth += healing;
            CurrentHealth = CheckMaxHealth();
        }

        /// <summary>
        /// Метод проверки текущего здоровья — не должно опускаться ниже 0
        /// </summary>
        private void CheckMinHealth()
        {
            if (CurrentHealth <= 0) SetCurrentHealthToZero(); 
        }

        /// <summary>
        /// Метод проверки текущего здоровья — не должно превышать максимальное
        /// </summary>
        /// <returns>текущее здоровье</returns>
        private float CheckMaxHealth()
        {
            if (CurrentHealth >= MaxHealth) CurrentHealth = MaxHealth;
            return CurrentHealth;
        }

        /// <summary>
        /// Метод установки текущего здоровья на 0
        /// </summary>
        private void SetCurrentHealthToZero()
        {
            CurrentHealth = 0;
        }

        /// <summary>
        /// Метод проверки, жив ли игрок
        /// </summary>
        public bool CheckIsAlive()
        {
            return IsAlive = CurrentHealth > 0;
        }

        /// <summary>
        /// Метод переключения всех активных коллайдеров, после гибели
        /// </summary>
        /// <param name="active">состояние</param>
        public void SetAllCollidersStatus(bool active)
        {
            foreach (var c in GetComponents<Collider2D>())
            {
                c.enabled = active;
            }
        }
    }
}
