using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers
{
    public class TPRTowerLimit : ITowerPlacementRule
    {
        private int m_maxTowers;

        public TPRTowerLimit(int maxTowers)
        {
            m_maxTowers = maxTowers;
        }

        Result ITowerPlacementRule.CanPlace(Tower tower, TowerGroup group)
        {
            if(group.Towers.Value.Count >= m_maxTowers)
            {
                return new Result()
                {
                    Success = false,
                    BriefReason = "Maximum number of towers placed",
                    FullReason = $"{group} can only have {m_maxTowers}"
                };
            }

            return new Result()
            {
                Success = true
            };
        }
    }
}