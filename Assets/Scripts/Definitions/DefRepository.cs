using System.Linq;
using UnityEngine;

namespace Definitions
{
    public class DefRepository<TDefType> where TDefType : IHaveId
    {
        [SerializeField] private TDefType[] _collections;
        
        public TDefType GetItem(string itemName) =>
            _collections.FirstOrDefault(x => x.Name.Contains(itemName));

        public TDefType[] Items => _collections;

    }


    public interface IHaveId
    {
        string Name { get; }
    }
}