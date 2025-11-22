using System.Collections.Generic;

namespace Creatures.Hero
{
    public class Lock
    {
        private List<object> _immunities = new List<object>();

        public void AddLock(object immunity)
        {
            if (!_immunities.Contains(immunity))
                _immunities.Add(immunity);
        }

        public void RemoveLock(object immunity) => _immunities.Remove(immunity);

        public bool IsImmune() => _immunities.Count > 0;
    }
}