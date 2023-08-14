using UnityEngine;

namespace Assets.Scripts.Common
{
    public class ParallaxController : MonoBehaviour
    {
        // Анимация фона

        [SerializeField] private Transform[] layers;    // Слои
        [SerializeField] private float[] coeff;         // Коэффициент

        private int layersCount;    // Переменная количества слоёв

        private void Start()
        {
            layersCount = layers.Length;    // Инициализация количества слоёв
        }

        private void Update()
        {
            for (var i = 0;i < layersCount; i++)
            {
                layers[i].position = transform.position * coeff[i]; // Смещаем слои, в зависимости от коэффициента
            }
        }
    }
}
