using UnityEngine;

namespace GameObjects.Extensions
{
    public class SpawnExtensions
    {
        private static Transform _spawnParticleContainer;
        private static Transform _spawnObjectContainer;
        private static string _spawnParticleContainerName = "###PARTICLE CONTAINER###";
        private static string _spawnObjectContainerName = "###OBJECT CONTAINER###";

        public static GameObject SpawnInParticleContainer(GameObject _prefab, Transform _spawnPoint)
        {
            if (!_spawnParticleContainer  || !_spawnParticleContainer.gameObject) GetContainer(_spawnParticleContainerName);
            return GameObject.Instantiate(_prefab, _spawnPoint.position, Quaternion.identity, _spawnParticleContainer);
        }
        
        public static GameObject SpawnInObjectContainer(GameObject _prefab, Transform _spawnPoint)
        {
            if (!_spawnObjectContainer  || !_spawnObjectContainer.gameObject) GetContainer(_spawnObjectContainerName);
            return GameObject.Instantiate(_prefab, _spawnPoint.position, Quaternion.identity, _spawnObjectContainer);
        }
        

        private static void GetContainer(string containerName)
        {
            var containerGO = GameObject.Find(containerName);
            if (!containerGO) containerGO = new GameObject(containerName);
            switch (containerName)
            { 
                 case "###OBJECT CONTAINER###":
                     _spawnObjectContainer = containerGO.transform;
                     break;
                 case "###PARTICLE CONTAINER###":
                     _spawnParticleContainer = containerGO.transform;
                        break;
            }
        }
    }
}