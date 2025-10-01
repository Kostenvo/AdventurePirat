using Scripts.Creatures.Hero;
using UnityEngine;

namespace Scriptes.Creatures
{
    public class ChangeCoinComponent :MonoBehaviour
    {
        [SerializeField] private int _changeCoin;

        public void ChangeCoin(GameObject GOWithWallet)
        {
            GOWithWallet.GetComponent<IChangeCoinInWallet>().CoinChange(_changeCoin);
        }
    }
}