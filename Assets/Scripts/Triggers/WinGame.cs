using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Triggers
{
    public class WinGame : MonoBehaviour
    {
        // Сценарий, исполняемый в случае выигрыша
        [SerializeField] private GameObject eventSystem;  // Система обработки событий

        /// <summary>
        /// Метод на входе в триггер
        /// </summary>
        /// <param name="other">объект, вошедший в триггер</param>
        private void OnTriggerEnter2D(Object other)
        {
            if(other.name == "Player")
            {
                eventSystem.GetComponent<GameManager>().WonGame();
            }
        }
    }
}
