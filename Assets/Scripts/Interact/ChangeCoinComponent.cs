using Creatures;
using UnityEngine;

namespace Interact
{
    public class ChangeCoinComponent :MonoBehaviour
    {
        [SerializeField] private int _changeCoin;

        public void ChangeCoin(GameObject goWithWallet)
        {
            goWithWallet.GetComponent<IChangeCoinInWallet>().CoinChange(_changeCoin);
        }
    }
}