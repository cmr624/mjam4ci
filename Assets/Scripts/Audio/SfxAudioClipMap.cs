using UnityEngine;
using System.Collections.Generic;
using System;

namespace MechJam.Audio
{
    [CreateAssetMenu(fileName = "SfxMap", menuName = "MechJam/SFX event map", order = 1)]
    public class SfxAudioClipMap : ScriptableObject
    {
        [Serializable]
        public struct KeyValue
        {
            [SerializeField]
            public string Action;

            [SerializeField]
            public AudioClip SFXClip;
        }

        [SerializeField]
        List<KeyValue> _keyValues;

        Dictionary<string, AudioClip> _sfxMap;
        public Dictionary<string, AudioClip> Map => _sfxMap;
        
        public void RefreshMap()
        {
            _sfxMap = new Dictionary<string, AudioClip>();

            foreach (var kvp in _keyValues) {
                _sfxMap[kvp.Action] = kvp.SFXClip;
            }
        }
    }
}
