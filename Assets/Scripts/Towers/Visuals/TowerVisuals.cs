using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

namespace Towers
{
    public class TowerVisuals : MonoBehaviour
    {
        [HideInInspector]
        public Health m_health;
        private void Awake()
        {
            m_health = GetComponent<Health>();
        }
        public void SetUp(Tower tower)
        {
            tower.Position.Subscribe(SetPosition);
            //TODO: healthbar
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}