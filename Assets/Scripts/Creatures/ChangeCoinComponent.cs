using Scripts.Creatures.Hero;
using UnityEngine;

namespace Creatures
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