using UnityEngine;
using UnityEngine.Rendering;

namespace Creatures.Hero
{
    public class ChangeGlobalVolume : MonoBehaviour
    {
        private Volume _volume;
        
        public void ChangeVolume(VolumeProfile profile)
        {
            if (profile == null) return;
            if (_volume == null) _volume = GetVolume();
            if (_volume == null) return;
            _volume.profile = profile;
        }

        private Volume GetVolume()
        {
            var volumes = FindObjectsByType<Volume>(FindObjectsSortMode.None);
            foreach (var volume in volumes)
            {
                if(volume.isGlobal) return volume;
            }
            return null;
        }
    }
}