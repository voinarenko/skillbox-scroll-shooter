using Assets.Scripts.Common;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class DamageDealerZone : MonoBehaviour
    {
        // ���� �� ������� ����

        #region ����������

        [SerializeField] private float tickTime;        // ����� �� ��������� ���������� ����� �� ����
        [SerializeField] private float damageFromFire;  // ���������� ����� �� ����

        private const float NormalState = 0;            // ����� ��������� �� ���������� �����������
        private const float BurningState = 1;           // ����� ����� � ������� ����

        private const string DetectionTag = "Player";   // ����� ������
        private GameObject player;                      // ������ ������

        private float currentState, currentTimeToTick;  // ���������� �������� ���������, ������� �� ��������� ����� �� ����

        #endregion

        // �������������
        void Start()
        {
            player = null;
        }
        private void Awake()
        {
            currentTimeToTick = 0; // ����� �� ��������� ����� �� ����
        }

        void Update()
        {
            if (currentTimeToTick >= tickTime)
            {
                currentTimeToTick = 0;
                player.GetComponent<Health>().TakeDamage(damageFromFire);  // ��������� ����� �� ����
                if (!player.GetComponent<Health>().CheckIsAlive()) player.GetComponent<PlayerHealth>().PlayerDead();  // ���� �������� ������ ���������� �� 0 � ����� ����

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
        /// ����� ��������� ����� ��� ����� � �������
        /// </summary>
        /// <param name="collision">������, �������� � �������</param>
        private void OnTriggerEnter2D(Component collision)
        {
            player = collision.gameObject;
            if (!collision.CompareTag(DetectionTag)) return;    // ���� ������ ������������� �����
            currentState = BurningState;                        // ����������� ���������
            collision.GetComponent<Health>().TakeDamage(damageFromFire);  // ������� ������� ����
        }

        /// <summary>
        /// ����� ���������� �������������� ����� ��� ������ �� ��������
        /// </summary>
        /// <param name="collision">������, �������� �� ��������</param>
        private void OnTriggerExit2D(Component collision)
        {
            player = null;
            if (!collision.CompareTag(DetectionTag)) return;    // ���� ������ ������������� �����
            currentState = NormalState;                         // ����������� ���������
        }
    }
}
