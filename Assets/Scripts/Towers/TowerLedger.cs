using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Towers
{
    public class TowerLedger
    {
        private struct TowerObject
        {
            public Tower Tower;
            public TowerVisuals Visuals;
            public TowerGroup Group;
        }

        private List<TowerObject> m_towers = new List<TowerObject>();

        public void AddTower(Tower tower, TowerVisuals visuals, TowerGroup group)
        {
            m_towers.Add(new TowerObject()
            {
                Tower = tower,
                Visuals = visuals,
                Group = group
            });

            tower.Health.Subscribe((health) =>
            {
                if (health == tower.Health.Min.Value)
                {
                    DestroyTower(tower);
                }
            });
        }

        public void DestroyTower(Tower tower)
        {
            if(tower == null)
            {
                return;
            }

            if (m_towers.Count <= 0)
            {
                return;
            }

            TowerObject towerObject = m_towers.FirstOrDefault(obj => obj.Tower == tower);

            if (towerObject.Tower == null)
            {
                return;
            }

            m_towers.Remove(towerObject);

            towerObject.Group.RemoveTower(tower);

            GameObject.Destroy(towerObject.Visuals.gameObject);
        }
    }
}