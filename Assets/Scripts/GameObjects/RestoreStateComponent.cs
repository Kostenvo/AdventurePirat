using System;
using GameData;
using UnityEngine;

namespace GameObjects
{
    public class RestoreStateComponent :MonoBehaviour
    {
        [SerializeField] private string _id;

        private void Start()
        {

            if (GameSession.Instance.IsStoredGo(_id)) Destroy(gameObject);
        }

        public void StoreInSession()
        {
            GameSession.Instance.StoreGo(_id);
        }
    }
}