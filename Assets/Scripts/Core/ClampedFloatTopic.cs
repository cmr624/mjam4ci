using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampedFloatTopic : Topic<float>
{
    public Topic<float> Min { get; } 
    public Topic<float> Max { get; }

    public override float Value 
    { 
        set => base.Value = Mathf.Clamp(value, Min.Value, Max.Value); 
    }

    public ClampedFloatTopic(float min = 0f, float max = 1f)
    {
        Min = new Topic<float>(min);
        Max = new Topic<float>(max);
        Min.Subscribe(Refresh);
        Max.Subscribe(Refresh);
    }
}
