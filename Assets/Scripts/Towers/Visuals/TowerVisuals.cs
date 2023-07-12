using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers
{
    public class TowerVisuals : MonoBehaviour
    {
        public void SetUp(Tower tower)
        {
            tower.Position.Subscribe(SetPosition);
            //TODO: healthbar?
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}