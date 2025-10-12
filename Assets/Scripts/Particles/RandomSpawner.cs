using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Particles
{
    /// <summary>
    /// Компонент для спавна обьекта в определенном радиусе с заданной силой
    /// </summary>
    public class RandomSpawner : MonoBehaviour
    {
        /// <summary>
        /// угол спавна обьектов
        /// </summary>
        [Header("Spawn bound:")] [SerializeField]
        private float _sectorAngle = 60;

        /// <summary>
        /// Угол смещения спавнера
        /// </summary>
        [SerializeField] private float _sectorRotation;

        /// <summary>
        /// Время между спавнами
        /// </summary>
        [Header("Spawn params:")]
        [Space]

        // [SerializeField] private ContainerForGO _container;
        [SerializeField]
        private float _waitTime = 0.1f;

        /// <summary>
        /// сила спавна
        /// </summary>
        [SerializeField] private float _speed = 6;

        /// <summary>
        /// Сколько спавнится за раз
        /// </summary>
        [SerializeField] private float _itemPerBurst = 2;

        /// <summary>
        /// ивент после спавна если нужен
        /// </summary>
        [SerializeField] UnityEvent _eventAfterSpawn;

        private Coroutine _routine;

        /// <summary>
        /// запуск спавна массива
        /// </summary>
        /// <param name="spawnObjects"></param>
        public void Spawn(GameObject[] spawnObjects)
        {
            TryStopRoutine();
            _routine = StartCoroutine(StartSpawn(spawnObjects));
        }

        private IEnumerator StartSpawn(GameObject[] spawnObjects)
        {
            var countObjects = spawnObjects.Length;
            while (countObjects > 0)
            {
                for (int j = 0; j < _itemPerBurst && countObjects > 0; j++)
                {
                    Spawn(spawnObjects[countObjects - 1]);
                    countObjects--;
                }

                yield return new WaitForSeconds(_waitTime);
            }

            _eventAfterSpawn?.Invoke();
        }

        /// <summary>
        /// Спавним все элементы разом без задержки
        /// </summary>
        /// <param name="spawnObjects"></param>
        public void SpawnAll(GameObject[] spawnObjects)
        {
            var countObjects = spawnObjects.Length;
            while (countObjects > 0)
            {
                for (int j = 0; countObjects > 0; j++)
                {
                    Spawn(spawnObjects[countObjects - 1]);
                    countObjects--;
                }
            }

            _eventAfterSpawn?.Invoke();
        }

        /// <summary>
        /// спавн одного элемента в заданном радиусе
        /// </summary>
        /// <param name="spawnObject"></param>
        private void Spawn(GameObject spawnObject)
        {
            //var instance = SpawnUtilites.Spawn(spawnObject, transform.position);
            var instance = Instantiate(spawnObject, transform.position, Quaternion.identity);
            // var instance = spawnObject.SpawnObjectINConteiner(transform.position, _container);
            var rigidBody = instance.GetComponent<Rigidbody2D>();

            var randomAngle = Random.Range(0, _sectorAngle);
            var forceVector = AngleToVectorInSector(randomAngle);
            rigidBody.AddForce(forceVector * _speed, ForceMode2D.Impulse);
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var position = transform.position;
            var startAnglePosition = (180 - _sectorRotation - _sectorAngle) / 2;
            var rightBound = GetUnitOnCircle(startAnglePosition);
            Handles.DrawLine(position, position + rightBound * _speed * 0.3f);

            var leftBound = GetUnitOnCircle(startAnglePosition + _sectorAngle);
            Handles.DrawLine(position, position + leftBound * _speed * 0.3f);
            Handles.DrawWireArc(position, Vector3.forward, rightBound, _sectorAngle, _speed * 0.3f);

            Handles.color = new Color(1, 1, 1, 0.1f);
            Handles.DrawSolidArc(position, Vector3.forward, rightBound, _sectorAngle, _speed * 0.3f);
        }
#endif
        private Vector2 AngleToVectorInSector(float angle)
        {
            var angleMiddleDelta = (180 - _sectorRotation - _sectorAngle) / 2;
            return GetUnitOnCircle(angle + angleMiddleDelta);
        }

        /// <summary>
        /// Полусение координат на единичной окружности
        /// </summary>
        /// <param name="angleDegress"></param>
        /// <returns></returns>
        private Vector3 GetUnitOnCircle(float angleDegress)
        {
            var angleRadians = angleDegress * Mathf.PI / 180f; //пеевод в радианы

            var x = Mathf.Cos(angleRadians); 
            var y = Mathf.Sin(angleRadians);

            return new Vector3(x, y, 0);
        }

        private void OnDisable()
        {
            TryStopRoutine();
        }

        private void OnDestroy()
        {
            TryStopRoutine();
        }

        private void TryStopRoutine()
        {
            if (_routine != null)
            {
                StopCoroutine(_routine);
            }
        }
    }
}