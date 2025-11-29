using System;
using System.Collections;
using Animation;
using Creatures;
using GameObjects.Extensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Particles
{
    public class CircularSpawner : MonoBehaviour
    {
        [SerializeField] private CircularProjectTile[] _circularLevels;
        [SerializeField] private UnityEvent _levelUpEvent;

        private int _currentLevel;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            StartCoroutine(SpawnCoroutine());
        }

        public void LevelUp()
        {
            _currentLevel = Mathf.Min(++_currentLevel, _circularLevels.Length - 1);
            _levelUpEvent?.Invoke();
        }

        private IEnumerator SpawnCoroutine()
        {
            var currentLevelData = _circularLevels[_currentLevel];
            var deltaAngle = Mathf.PI * 2 / currentLevelData.Count;
            for (int i = 0, group = 1; i < currentLevelData.Count; i++ , group++)
            {
                var currentAngle = i * deltaAngle;
                var dirX = Mathf.Sin(currentAngle);
                var dirY = Mathf.Cos(currentAngle);
                var direction = new Vector3(dirX, dirY, 0);
                var spawnObject =
                    SpawnExtensions.SpawnInObjectContainer(currentLevelData.SpawnPrefab.GameObject(),
                        transform).GetComponent<ThrowDirectional>();
                spawnObject.SpawnTowards(direction);
                if (group < currentLevelData.BatchSize) continue;
                group = 0;
                yield return new WaitForSeconds(currentLevelData.SpawnDelay);
            }
        }
    }

    [Serializable]
    public struct CircularProjectTile
    {
        [SerializeField] private ThrowDirectional _spawnPrefab;
        [SerializeField] private float _batchSize;
        [SerializeField] private float _count;
        [SerializeField] private float _spawnDelay;

        public float BatchSize => _batchSize;

        public float SpawnDelay => _spawnDelay;
        public float Count => _count;
        public ThrowDirectional SpawnPrefab => _spawnPrefab;
    }
}