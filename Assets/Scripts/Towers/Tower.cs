using System;
using MoreMountains.TopDownEngine;
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
        public Topic<Vector3> Position { get; } = new Topic<Vector3>();

        public Action OnDestroyed;
        
        public Tower(TowerStats data)
        {
            Cost = data.Cost;

        }

        public override string ToString()
        {
            return $"Tower at ({Position.Value.x}, {Position.Value.y}, {Position.Value.z})";
        }
    }
}