using UnityEngine;
using System.Collections.Generic;
using MoreMountains.Tools;

namespace MechJam.Audio
{
    public class SfxAudioEventDriver : MonoBehaviour
    {
        // (naive) singleton pattern
        private static SfxAudioEventDriver Instance { get; set; }
        
        [SerializeField]
        SfxAudioClipMap _sfxMapFile;
        
        private Dictionary<string, AudioClip> _sfxDict;
        private AudioListener _audioListener;
        private MMSoundManagerPlayOptions _playbackOptions; 
        
        public static AudioListener Listener => Instance._audioListener;

        private void Awake()
        {
            // prioritize the most recently-created instance
            // (i.e. the one corresponding to the most recently loaded-in scene)
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            
            Instance = this;

            _sfxMapFile.RefreshMap();
            _sfxDict = _sfxMapFile.Map;

            _playbackOptions = MMSoundManagerPlayOptions.Default;
            _playbackOptions.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Sfx; 
        }

        private void Start()
        {
            // TODO: generally bad practice to do a brute-force lookup; 
            // should this be inspector-assigned?
            _audioListener = FindObjectOfType<AudioListener>(); 
        }
        
        // users should trigger clip playback through the static wrapper, 
        // not an instance method
        public static void PlayClip(string action)
        {
            Instance.FireSfxEvent(action);
        }

        private void FireSfxEvent(string action)
        {
            if (_sfxDict == null)
            {
                _sfxMapFile.RefreshMap();
                _sfxDict = _sfxMapFile.Map; 
            }

            if (!_sfxDict.ContainsKey(action))
            {
                Debug.LogError("Clip playback failed: clip name not found");
                return; 
            }
            
            AudioClip clip = _sfxDict[action];

            // Playback via MoreMountains sound system
            MMSoundManager.Current.PlaySound(clip, _playbackOptions);

            
            // Non-MoreMountains playback implementation 

            // TODO: is PlayClipAtPoint performant enough? 
            // (it creates and destroys a new source per single call, which might be expensive) 
            // AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        }
    }
}
