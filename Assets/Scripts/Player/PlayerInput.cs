using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(Shooter))]
    public class PlayerInput : MonoBehaviour
    {
        // Переменные
        private PlayerMovement playerMovement;  // Скрипт движения
        private Shooter shooter;                // Скрипт стрельбы

        // Инициализация
        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
            shooter = GetComponent<Shooter>();
        }

        private void Update()
        {
            // Получаем данные с устройств ввода
            var horizontalDirection = Input.GetAxis(GlobalStringVars.HorizontalAxis);   // Горизонтальное движение
            var isJumpButtonPressed = Input.GetButtonDown(GlobalStringVars.Jump);            // Прыжок
            
            // Стрельба
            if (Input.GetButtonDown(GlobalStringVars.Fire1))
                // Если не на перезарядке и мышь не над элементом интерфейса — стреляем
                if (!shooter.IsReloading && !EventSystem.current.IsPointerOverGameObject()) shooter.Shoot(playerMovement.Facing);
                // Закончились патроны
                else if (!EventSystem.current.IsPointerOverGameObject()) shooter.Empty();
                else return;
            // Движение игрока
            playerMovement.Move(horizontalDirection, isJumpButtonPressed);
        }

    }
}
