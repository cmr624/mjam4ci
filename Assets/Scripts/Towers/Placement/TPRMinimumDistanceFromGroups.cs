using System.Collections.Generic;

namespace Towers
{
    //TPR = TowerPlacement Rule
    public class TPRMinimumDistanceFromGroups : ITowerPlacementRule
    {
        private float m_minDistance;
        private IEnumerable<TowerGroup> m_groups;

        public TPRMinimumDistanceFromGroups(float minDistance, IEnumerable<TowerGroup> groups)
        {
            m_minDistance = minDistance;
            m_groups = groups;
        }

        Result ITowerPlacementRule.CanPlace(Tower tower, TowerGroup _)
        {
            Result result = new Result()
            {
                Success = true,
            };

            foreach (TowerGroup group in m_groups)
            {
                foreach (Tower t in group.Towers.Value)
                {
                    //If this tower is already in another group, allow it to join
                    //another group. Otherwise it will always fail, as the tower will
                    //always be too close to itself.
                    if(t == tower)
                    {
                        continue;
                    }

                    float dist = (t.Position.Value - tower.Position.Value).magnitude;

                    if (dist < m_minDistance)
                    {
                        result.Success = false;
                        result.BriefReason = $"Too close to another tower";
                        result.FullReason += $"Tower {t} in group {group} and Tower {tower} are {dist}m apart. They need to be {m_minDistance}m at a minimum.\n";
                    }
                }
            }

            return result;
        }
    }
}
