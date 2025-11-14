using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _loadDelay;
        private static readonly int ShowKey = Animator.StringToHash("Show");

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void LoadLoader()
        {
            SceneManager.LoadScene("LoaderLevel", LoadSceneMode.Additive);
            
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }


        public void LoadWithLoader(string levelName)
        {
            StartCoroutine(Load(levelName));
        }

        private IEnumerator Load(string levelName)
        {
            _animator.SetBool(ShowKey, true);
            yield return new WaitForSeconds(_loadDelay);
            SceneManager.LoadScene(levelName);
            _animator.SetBool(ShowKey, false);
        }
    }
}