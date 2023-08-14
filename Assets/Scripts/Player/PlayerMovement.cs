using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))] // Необходимый компонент Rigidbody2D
    public class PlayerMovement : MonoBehaviour
    {
        // Движение игрока

        #region Переменные

        [Header("Movement vars")]
        [SerializeField] private float jumpForce;   // Сила прыжка
        [SerializeField] private float speed = 3;   // Скорость передвижения
        [SerializeField] private bool isGrounded;   // Игрок на земле

        [Header("Settings")]
        [SerializeField] private Transform groundColliderTransform; // Коллайдер для проверки соприкосновения с землёй
        [SerializeField] private AnimationCurve curve;              // Кривая анимации
        [SerializeField] private float jumpOffset;                  // Смещение прыжка
        [SerializeField] private LayerMask groundMask;              // Маска слоя земли

        private Rigidbody2D rb;         // Объект игрока
        private Animator anim;          // Аниматор
        private SpriteRenderer rend;    // Рендерер изображения
        public int Facing = 1;          // Направление взгляда
        private Vector2 moveVector;     // Вектор движения
        
        #endregion

        private void Start()
        {
            anim = GetComponent<Animator>();          // Инициализация аниматора
            if (anim == null) Debug.Log("Player Sprite is missing an Animator Component");
            rend = GetComponent<SpriteRenderer>();    // Инициализация рендерера
            if (anim == null) Debug.Log("Player Sprite is missing a Renderer Component");
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();   // Инициализация объекта
        }

        private void Update()
        {
            // Меняем направление взгляда
            if (Input.GetAxisRaw(GlobalStringVars.HorizontalAxis) > 0)
            {
                rend.flipX = false;
                Facing = 1;
            }
            else if (Input.GetAxisRaw(GlobalStringVars.HorizontalAxis) < 0)
            {
                rend.flipX = true;
                Facing = -1;
            }
        }

        private void FixedUpdate()
        {
            // Проверка нахождения на земле
            var overlapCirclePosition = groundColliderTransform.position;
            isGrounded = Physics2D.OverlapCircle(overlapCirclePosition, jumpOffset, groundMask);
        }

        /// <summary>
        /// Метод движения игрока
        /// </summary>
        /// <param name="direction">направление</param>
        /// <param name="isJumpButtonPressed">прыжок</param>
        public void Move(float direction, bool isJumpButtonPressed)
        {
            // Прыжок
            if (isJumpButtonPressed || !isGrounded)
            {
                Jump();
                anim.SetTrigger("IsJumping");
            }
            else
                anim.ResetTrigger("IsJumping");


            // Горизонтальное движение
            if (Mathf.Abs(direction) > 0.01f)
            {
                HorizontalMovement();
                anim.SetTrigger("IsRunning");
            }
            else
                anim.ResetTrigger("IsRunning");
        }

        /// <summary>
        /// Метод прыжка
        /// </summary>
        private void Jump()
        {
            if (isGrounded)
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        /// <summary>
        /// Метод горизонтального движения
        /// </summary>
        private void HorizontalMovement()
        {
            moveVector.x = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        }
    }
}
