using System;
using UnityEngine;

namespace Towers
{
    [Serializable]
    public class TowerStats
    {
        public float MaxHealth;
        public int Cost;
    }

    public class Tower
    {
        public int Cost { get; }
        public ClampedFloatTopic Health { get; } = new ClampedFloatTopic();
        public Topic<Vector3> Position { get; } = new Topic<Vector3>();

        public Action OnDestroyed;

        public Tower(TowerStats data)
        {
            Cost = data.Cost;
            Health.Max.Value = data.MaxHealth;
            Health.SetToMax();
        }

        public override string ToString()
        {
            return $"Tower at ({Position.Value.x}, {Position.Value.y}, {Position.Value.z})";
        }
    }
}