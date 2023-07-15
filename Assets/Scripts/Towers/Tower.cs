using System;
using UnityEngine;

namespace Towers
{
    public class Tower 
    {
        public int Cost { get; set; }
        public float MaxHealth { get; set; }
        public Topic<Vector3> Position { get; } = new Topic<Vector3>();

        public Action OnDestroyed;

        public void Destroy()
        {
            OnDestroyed?.Invoke();
        }

        public override string ToString()
        {
            return $"Tower at ({Position.Value.x}, {Position.Value.y}, {Position.Value.z})";
        }
    }
}