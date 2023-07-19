using System;
using UnityEngine;

namespace MechJam.Audio
{
    public class AnimationEventReceiver : MonoBehaviour
    {
        [Tooltip("The clip name to use if the animation event doesn't specify one.")]
        [SerializeField] private string _fallbackClipName = "Walk";

        [SerializeField] private bool _shouldAlwaysPlay = true;
        
        [Tooltip("No effect if `shouldAlwaysPlay` == true; percent chance of playback on trigger otherwise.")]
        [SerializeField] private float _playbackProbability = 0.33f;

        [SerializeField] private bool _shouldMuteIfFarFromListener = true;
        [SerializeField] private float _maxPlaybackDistance = 16f; 
        
        public void TriggerClipPlayback(string sfx = null)
        {
            if (_shouldMuteIfFarFromListener)
            {
                float distanceToListener =
                    Vector3.Distance(SfxAudioEventDriver.Listener.transform.position, transform.position);

                if (distanceToListener > _maxPlaybackDistance) return;
            }

            sfx = string.IsNullOrEmpty(sfx) ? _fallbackClipName : sfx; 
            
            if (_shouldAlwaysPlay)
            { 
                SfxAudioEventDriver.PlayClip(sfx);
            } else if (_playbackProbability > 0f) { 
                int draw = UnityEngine.Random.Range(0, Mathf.RoundToInt(1f / _playbackProbability));

                if (draw == 0)
                {
                    SfxAudioEventDriver.PlayClip(sfx);
                }
            }
        }
    }
}
